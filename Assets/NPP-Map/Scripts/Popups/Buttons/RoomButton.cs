using TMPro;
using UnityEngine;

namespace NPPMap
{
    public class RoomButton : MonoBehaviour
    {
        private const int Offset = 100;

        [SerializeField] private TextMeshProUGUI machineName;

        private RoomInformation _roomData;
        private RoomInformationPopup _roomInformationPopup;

        public void SetData(RoomInformation machineInformation, RoomInformationPopup machinePopup)
        {
            _roomInformationPopup = machinePopup;
            _roomData = machineInformation;
            machineName.text = machineInformation.Title;
        }

        public void Open()
        {
            _roomInformationPopup.Open(_roomData, transform.position + Vector3.right * Offset);
        }
    }
}