using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = nameof(MachineInformation),
        menuName = Constants.ProjectName + "/" + nameof(MachineInformation))]
    public class MachineInformation : ScriptableObject
    {
        [SerializeField] private string machineTitle;
        [SerializeField] private string field1;
        [SerializeField] private string field2;
        [SerializeField] private string field3;
        [SerializeField] private string field4;

        public string MachineTitle => machineTitle;
        public string Field1 => field1;
        public string Field2 => field2;
        public string Field3 => field3;
        public string Field4 => field4;
    }
}