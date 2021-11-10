using System.Collections.Generic;
using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = "RoomInformation", menuName = Constants.ProjectName + "/" + nameof(RoomInformation))]
    public class RoomInformation : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private List<MachineInformation> machinesInformation;
        public string Title => title;
        public string Description => description;
        public IReadOnlyList<MachineInformation> MachinesInformation => machinesInformation;

        public void Init(string title, string description, List<MachineInformation> machinesInformation)
        {
            this.title = title;
            this.description = description;
            this.machinesInformation = machinesInformation;
        }
    }
}