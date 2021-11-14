using TMPro;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class MachineButton : MonoBehaviour
    {
        private const int Offset = 100;

        [SerializeField] private TextMeshProUGUI machineName;

        private MachineInformation _machineData;
        private MachinePopup _machinePopup;

        public void SetData(MachineInformation machineInformation)
        {
            _machineData = machineInformation;
            machineName.text = machineInformation.MachineName;
        }

        public void Open()
        {
            _machinePopup.Open(_machineData, transform.position + Vector3.right * Offset);
        }

        public static MachineButton Create(MachineButton prefab, Transform parent, MachinePopup popup)
        {
            MachineButton instance = Instantiate(prefab, parent);
            instance._machinePopup = popup;
            return instance;
        }
    }
}