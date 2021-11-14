using TMPro;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class RoomButton : MonoBehaviour
    {
        private const int Offset = 100;

        [SerializeField] private TextMeshProUGUI machineName;

        private RoomInformation _roomData;

        private RoomInformationPopup _roomInformationPopup;
        private RoomMapDrawer _map;

        public void SetData(RoomInformation machineInformation)
        {
            _roomData = machineInformation;
            machineName.text = machineInformation.Title;
        }

        public void Open()
        {
            _roomInformationPopup.Open(_roomData);
            _map.Open(_roomData);
        }

        public static RoomButton Create(RoomButton prefab, Transform parent, RoomInformationPopup popup, RoomMapDrawer roomMapDrawer)
        {
            RoomButton instance = Instantiate(prefab, parent);
            instance._map = roomMapDrawer;
            instance._roomInformationPopup = popup;
            return instance;
        }
    }
}