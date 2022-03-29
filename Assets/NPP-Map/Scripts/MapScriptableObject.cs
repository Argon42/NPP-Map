using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = nameof(MapScriptableObject), menuName = Constants.ProjectName + "/" + nameof(MapScriptableObject))]
    public class MapScriptableObject : ScriptableObject
    {
        [SerializeField] private Sprite map;
        [SerializeField] private List<RoomInformation> rooms;

        public IReadOnlyList<RoomInformation> GetRooms() => rooms;

        public IReadOnlyList<MachineInformation> GetMachines() =>
            GetRooms().SelectMany(information => information.MachinesInformation).ToList();
    }
}