using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class SearchPopup : Popup<string>
    {
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform parent;

        [Space, SerializeField] private MachineButton machineButtonPrefab;
        [SerializeField] private List<MachineButton> machineButtonsInstances = new List<MachineButton>(20);

        [Space, SerializeField] private RoomButton roomButtonPrefab;
        [SerializeField] private List<RoomButton> roomButtonsInstances = new List<RoomButton>(20);

        private readonly char[] _separators = {',', ';'};

        [Inject] private MachinePopup _machinePopup;
        [Inject] private MapObject _map;
        [Inject] private RoomInformationPopup _roomPopup;

        protected override void SetData(string listOfNames)
        {
            SetupMachineButtons(FindMachines(listOfNames));
            SetupRoomButtons(FindRooms(listOfNames));
        }

        public void Search(string listOfNames) => SetData(listOfNames);

        public void Open() => Open("", spawnPosition.position);

        private List<MachineInformation> FindMachines(string listOfNames)
        {
            string[] names = listOfNames
                .Split(_separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(machine => machine.Trim())
                .ToArray();
            List<MachineInformation> machines = _map.GetMachines();

            return machines.Where(machine => names.Contains(machine.MachineName)).ToList();
        }

        private List<RoomInformation> FindRooms(string listOfNames)
        {
            string[] names = listOfNames
                .Split(_separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(machine => machine.Trim())
                .ToArray();
            List<RoomInformation> rooms = _map.GetRooms();

            return rooms.Where(room => names.Contains(room.Title)).ToList();
        }

        private void SetupMachineButtons(List<MachineInformation> data)
        {
            for (var i = 0; i < data.Count || i < machineButtonsInstances.Count; i++)
            {
                if (i >= machineButtonsInstances.Count)
                    machineButtonsInstances.Add(CreateMachineButton());

                bool hasData = i < data.Count;
                machineButtonsInstances[i].gameObject.SetActive(hasData);

                if (hasData)
                    machineButtonsInstances[i].SetData(data[i], _machinePopup);
            }
        }

        private void SetupRoomButtons(List<RoomInformation> data)
        {
            for (var i = 0; i < data.Count || i < roomButtonsInstances.Count; i++)
            {
                if (i >= roomButtonsInstances.Count)
                    roomButtonsInstances.Add(CreateRoomButton());

                bool hasData = i < data.Count;
                roomButtonsInstances[i].gameObject.SetActive(hasData);

                if (hasData)
                    roomButtonsInstances[i].SetData(data[i], _roomPopup);
            }
        }

        private MachineButton CreateMachineButton() => Instantiate(machineButtonPrefab, parent);

        private RoomButton CreateRoomButton() => Instantiate(roomButtonPrefab, parent);
    }
}