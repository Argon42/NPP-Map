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
        [SerializeField] private MachineButton buttonPrefab;
        [SerializeField] private List<MachineButton> buttonsInstances = new List<MachineButton>(20);

        private readonly char[] _separators = {',', ';'};

        [Inject] private MachinePopup _machinePopup;
        [Inject] private MapObject _map;

        protected override void SetData(string listOfNames) => SetupButtons(FindMachines(listOfNames));

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

        private void SetupButtons(List<MachineInformation> data)
        {
            for (var i = 0; i < data.Count || i < buttonsInstances.Count; i++)
            {
                if (i >= buttonsInstances.Count)
                    buttonsInstances.Add(CreateButton());

                bool hasData = i < data.Count;
                buttonsInstances[i].gameObject.SetActive(hasData);

                if (hasData)
                    buttonsInstances[i].SetData(data[i], _machinePopup);
            }
        }

        private MachineButton CreateButton() => Instantiate(buttonPrefab, parent);
    }
}