using TMPro;
using UnityEngine;

namespace NPPMap
{
    public class MachineButton : MonoBehaviour
    {
        private const int Offset = 100;

        [SerializeField] private TextMeshProUGUI machineName;

        private MachineInformation _machineData;
        private MachinePopup _machinePopup;

        public void SetData(MachineInformation machineInformation, MachinePopup machinePopup)
        {
            _machinePopup = machinePopup;
            _machineData = machineInformation;
            machineName.text = machineInformation.MachineTitle;
        }

        public void Open()
        {
            _machinePopup.Open(_machineData, transform.position + Vector3.right * Offset);
        }
    }
}