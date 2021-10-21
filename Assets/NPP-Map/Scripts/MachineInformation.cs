using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = nameof(MachineInformation),
        menuName = Constants.ProjectName + "/" + nameof(MachineInformation))]
    public class MachineInformation : ScriptableObject
    {
        [SerializeField] private string machineTitle;
        [SerializeField] private string description;

        public string MachineTitle => machineTitle;
        public string Description => description;
    }
}