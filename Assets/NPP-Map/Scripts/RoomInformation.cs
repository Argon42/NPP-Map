using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = "RoomInformation", menuName = Constants.ProjectName + "/" + nameof(RoomInformation))]
    public class RoomInformation : ScriptableObject
    {
        [SerializeField] private string roomTitle;
        [SerializeField] private string field1;
        [SerializeField] private string field2;
        [SerializeField] private string field3;
        [SerializeField] private string machineButtonText;
        [SerializeField] private MachineInformation machineInformation;

        public string RoomTitle => roomTitle;
        public string Field1 => field1;
        public string Field2 => field2;
        public string Field3 => field3;
        public string MachineButtonText => machineButtonText;
        public MachineInformation MachineInformation => machineInformation;
    }
}