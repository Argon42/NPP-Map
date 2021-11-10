using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPPMap
{
    public class MapObject : MonoBehaviour
    {
        public List<RoomInformation> GetRooms() =>
            GetComponentsInChildren<RoomMapButton>().Select(button => button.Information).ToList();

        public List<MachineInformation> GetMachines() =>
            GetRooms().SelectMany(information => information.MachinesInformation).ToList();
    }
}