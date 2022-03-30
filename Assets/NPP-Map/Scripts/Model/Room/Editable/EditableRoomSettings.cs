using System.Collections.Generic;
using NPPMap.MapCreating;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NPPMap
{
    public class EditableRoomSettings : Popup<EditableRoom>
    {
        private const float DefaultScale = 0.01f;
        
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TMP_InputField descriptionField;
        [SerializeField] private Button addRoomObject;
        [SerializeField] private RoomObjectSettings prefab;
        [SerializeField] private Transform parent;

        private EditableRoom _room;

        protected override void SetData(EditableRoom data)
        {
            _room = data;

            nameField.text = _room.Data.Name;
            descriptionField.text = _room.Data.Description;

            nameField.onDeselect.AddListener(OnNameChanged);
            descriptionField.onDeselect.AddListener(OnDescriptionChanged);
            addRoomObject.onClick.AddListener(OnAddRoomObject);

            _room.Data.RoomObjects.ForEach(CreateRoomObjectSettings);
        }

        public override void SetPopupPosition(Vector3 position)
        {
            transform.position = position;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 1f);
            Vector3 screenTouch = screenCenter + new Vector3(eventData.delta.x, eventData.delta.y, 0f);

            Vector3 worldCenterPosition = Camera.main.ScreenToWorldPoint(screenCenter);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(screenTouch);

            Vector3 delta = worldTouchPosition - worldCenterPosition;
            SetPopupPosition(transform.position + delta);
        }

        protected override Vector3 GetDefaultScale()
        {
            return Vector3.one * DefaultScale;
        }

        private void CreateRoomObjectSettings(RoomObjectData data)
        {
            RoomObjectSettings roomObjectSettings = Instantiate(prefab, parent);

            roomObjectSettings.Init(data);

            roomObjectSettings.OnElementDelete += OnDelete;
            roomObjectSettings.DataUpdated += _room.UpdateVisualize;

            void OnDelete()
            {
                roomObjectSettings.OnElementDelete -= OnDelete;
                roomObjectSettings.DataUpdated -= _room.UpdateVisualize;

                _room.Data.RoomObjects.Remove(data);

                Destroy(roomObjectSettings.gameObject);
            }
        }

        private void OnAddRoomObject()
        {
            var roomObjectData = new RoomObjectData();
            _room.Data.RoomObjects.Add(roomObjectData);
            CreateRoomObjectSettings(roomObjectData);
        }

        private void OnDescriptionChanged(string description)
        {
            _room.Data.Description = description;
        }

        private void OnNameChanged(string roomName)
        {
            _room.Data.Name = roomName;
            _room.UpdateVisualize();
        }
    }
}