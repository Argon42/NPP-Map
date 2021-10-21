using TMPro;
using UnityEngine;

namespace NPPMap
{
    public class MachinePopup : Popup<MachineInformation>
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;

        protected override void SetData(MachineInformation data)
        {
            title.text = data.MachineTitle;
            description.text = data.Description;
        }
    }
}