using System;
using System.Globalization;
using NPPMap.MapCreating;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NPPMap
{
    public class RoomObjectSettings : MonoBehaviour
    {
        public event Action DataUpdated;
        public event Action OnElementDelete;

        [SerializeField] private TMP_InputField roomName;
        [SerializeField] private TMP_InputField description;
        [SerializeField] private TMP_InputField positionX;
        [SerializeField] private TMP_InputField positionY;
        [SerializeField] private Button deleteElement;

        private RoomObjectData _roomObjectData;

        public void Init(RoomObjectData roomObjectData)
        {
            _roomObjectData = roomObjectData;

            roomName.text = _roomObjectData.Name;
            description.text = _roomObjectData.Description;
            positionX.text = _roomObjectData.Position.x.ToString(CultureInfo.InvariantCulture);
            positionY.text = _roomObjectData.Position.y.ToString(CultureInfo.InvariantCulture);

            roomName.onDeselect.AddListener(OnRoomNameChanged);
            description.onDeselect.AddListener(OnDescriptionChanged);
            positionX.onDeselect.AddListener(OnPositionXChanged);
            positionY.onDeselect.AddListener(OnPositionYChanged);

            deleteElement.onClick.AddListener(() => OnElementDelete?.Invoke());
        }

        private void OnDescriptionChanged(string arg0)
        {
            _roomObjectData.Description = arg0;
            DataUpdated?.Invoke();
        }

        private void OnPositionXChanged(string arg0)
        {
            Vector2 position = _roomObjectData.Position;
            position.x = float.Parse(arg0);
            _roomObjectData.Position = position;
            DataUpdated?.Invoke();
        }

        private void OnPositionYChanged(string arg0)
        {
            Vector2 position = _roomObjectData.Position;
            position.y = float.Parse(arg0);
            _roomObjectData.Position = position;
            DataUpdated?.Invoke();
        }

        private void OnRoomNameChanged(string arg0)
        {
            _roomObjectData.Name = arg0;
            DataUpdated?.Invoke();
        }
    }
}