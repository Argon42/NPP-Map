using System.Collections.Generic;
using NPPMap.MapCreating;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NPPMap
{
    public class EditableRoom : Room, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI roomName;
        [SerializeField] private EditableRoomSettings settingsPrefab;
        [SerializeField] private GameObject pointPrefab;

        private EditableRoomSettings _settings;

        private readonly List<GameObject> _points = new List<GameObject>();

        public void OnPointerClick(PointerEventData eventData)
        {
            CreateSettingWindow(eventData.pointerCurrentRaycast.worldPosition);
        }

        public void UpdateVisualize()
        {
            roomName.text = Data.Name;
            for (var i = 0; i < _points.Count || i < Data.RoomObjects.Count; i++)
            {
                if (i >= _points.Count)
                    _points.Add(CreatePoint());

                var hasData = i < Data.RoomObjects.Count;
                _points[i].SetActive(hasData);

                if (hasData)
                {
                    // _points[i].SetData(Data.RoomObjects[i]);
                    _points[i].transform.localPosition = Data.RoomObjects[i].Position;
                }
            }
        }

        private GameObject CreatePoint() => Instantiate(pointPrefab, transform);

        protected override void OnInit() => UpdateVisualize();

        private void CreateSettingWindow(Vector3 openPosition)
        {
            if (_settings == null)
                _settings = Instantiate(settingsPrefab, transform);
            
            _settings.Open(this, openPosition - Vector3.forward * 0.1f);
        }
    }
}