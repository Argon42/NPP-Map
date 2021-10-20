using TMPro;
using UnityEngine;

namespace NPPMap
{
    public class MachineInformationPopup : Popup<MachineInformation>
    {
        [SerializeField] private TextMeshProUGUI roomTitle;
        [SerializeField] private TextMeshProUGUI field1;
        [SerializeField] private TextMeshProUGUI field2;
        [SerializeField] private TextMeshProUGUI field3;
        [SerializeField] private TextMeshProUGUI field4;

        protected override void SetData(MachineInformation data)
        {
            roomTitle.text = data.MachineTitle;
            field1.text = data.Field1;
            field2.text = data.Field2;
            field3.text = data.Field3;
            field4.text = data.Field4;
        }
    }
}