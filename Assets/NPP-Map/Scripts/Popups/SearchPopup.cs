using System;
using System.Collections.Generic;
using System.Linq;
using NPPMap.Utility;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class SearchPopup : Popup<string>
    {
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform parent;

        [Space, SerializeField] private Pool<MachineButton, MachineInformation> machinePool;
        [Space, SerializeField] private Pool<RoomButton, RoomInformation> roomPool;

        private readonly char[] _separators = {',', ';'};

        [Inject] private MachinePopup _machinePopup;
        [Inject] private MapObject _map;
        [Inject] private RoomInformationPopup _roomPopup;
        [Inject] private RoomMapDrawer _mapDrawer;

        private void Awake()
        {
            machinePool.Init(
                button => MachineButton.Create(button, parent, _machinePopup),
                (button, information) => button.SetData(information)
            );

            roomPool.Init(
                button => RoomButton.Create(button, parent, _roomPopup, _mapDrawer),
                (button, information) => button.SetData(information)
            );
        }

        protected override void SetData(string listOfNames)
        {
            machinePool.SetupData(FindMachines(listOfNames));
            roomPool.SetupData(FindRooms(listOfNames));
        }

        public void Search(string listOfNames) => SetData(listOfNames);

        public void Open() => Open("", spawnPosition.position);

        private List<MachineInformation> FindMachines(string listOfNames)
        {
            string[] names = listOfNames
                .Split(_separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(machine => machine.Trim())
                .ToArray();

            return _map.GetMachines().Where(machine => names.Contains(machine.MachineName)).ToList();
        }

        private List<RoomInformation> FindRooms(string listOfNames)
        {
            string[] names = listOfNames
                .Split(_separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(machine => machine.Trim())
                .ToArray();

            return _map.GetRooms().Where(room => names.Contains(room.Title)).ToList();
        }
    }
}