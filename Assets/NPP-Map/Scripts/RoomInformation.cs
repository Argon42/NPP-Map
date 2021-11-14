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
        [SerializeField] private Sprite map;

        public string Title => title;
        public string Description => description;
        public IReadOnlyList<MachineInformation> MachinesInformation => machinesInformation;

        public Sprite LoadMap()
        {
            return map;
        }
    }
}