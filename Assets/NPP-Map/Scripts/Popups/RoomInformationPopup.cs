using TMPro;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class RoomInformationPopup : Popup<RoomInformation>
    {
        [SerializeField] private TextMeshProUGUI roomTitle;
        [SerializeField] private TextMeshProUGUI field1;
        [SerializeField] private TextMeshProUGUI field2;
        [SerializeField] private TextMeshProUGUI field3;
        [SerializeField] private TextMeshProUGUI field4;

        private MachineInformation _machineInformation;

        [Inject] private MachineInformationPopup _machineInformationPopup;

        protected override void SetData(RoomInformation data)
        {
            roomTitle.text = data.RoomTitle;
            field1.text = data.Field1;
            field2.text = data.Field2;
            field3.text = data.Field3;
            field4.text = data.MachineButtonText;
            _machineInformation = data.MachineInformation;
        }

        public void OpenMachineInformation()
        {
            _machineInformationPopup.Open(_machineInformation, field4.transform.position);
        }
    }
}