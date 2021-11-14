using TMPro;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class MapMachineButton : MonoBehaviour
    {
        private const int Offset = 100;

        [SerializeField] private TextMeshProUGUI machineName;

        private MachineInformation _machineData;
        private MachinePopup _machinePopup;

        public void SetPosition(Vector2 position)
        {
            var rectTransform = (RectTransform) transform;
            rectTransform.anchorMin = position;
            rectTransform.anchorMax = position;
            rectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetData(MachineInformation machineInformation)
        {
            _machineData = machineInformation;
            machineName.text = machineInformation.MachineName;
        }

        public void Open()
        {
            _machinePopup.Open(_machineData, transform.position + Vector3.right * Offset);
        }

        public static MapMachineButton Create(MapMachineButton prefab, Transform parent, MachinePopup popup)
        {
            MapMachineButton instance = Instantiate(prefab, parent);
            instance._machinePopup = popup;
            return instance;
        }
    }
}