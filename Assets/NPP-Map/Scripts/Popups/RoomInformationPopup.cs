using TMPro;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class RoomInformationPopup : Popup<RoomInformation>
    {
        private const float Offset = 300;

        [SerializeField] private TextMeshProUGUI roomTitle;
        [SerializeField] private TextMeshProUGUI description;

        [SerializeField] private Transform positionForOpen;

        [Inject] private ListMachinePopup _listMachinePopup;

        private RoomInformation _roomInformation;

        public void Open(RoomInformation data) => Open(data, positionForOpen.position);

        protected override void SetData(RoomInformation data)
        {
            roomTitle.text = data.Title;
            description.text = data.Description;
            _roomInformation = data;
        }

        public void OpenListMachines()
        {
            Vector3 popupPosition = transform.position + Vector3.right * Offset;
            _listMachinePopup.Open(new ListMachinePopup.Data(_roomInformation), popupPosition);
        }
    }
}