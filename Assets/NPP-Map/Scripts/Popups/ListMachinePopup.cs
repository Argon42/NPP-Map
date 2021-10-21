using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class ListMachinePopup : Popup<ListMachinePopup.Data>
    {
        private const string RoomTitle = "Задвижки";

        [SerializeField] private TextMeshProUGUI roomTitle;
        [SerializeField] private Transform parent;
        [SerializeField] private MachineButton buttonPrefab;
        [SerializeField] private List<MachineButton> buttonsInstances = new List<MachineButton>(20);

        [Inject] private MachinePopup _machinePopup;

        protected override void SetData(Data data)
        {
            roomTitle.text = $"{RoomTitle} {data.RoomName}";
            SetupButtons(data);
        }

        private void SetupButtons(Data data)
        {
            for (var i = 0; i < data.Machines.Count || i < buttonsInstances.Count; i++)
            {
                if (i >= buttonsInstances.Count)
                    buttonsInstances.Add(CreateButton());

                bool hasData = i < data.Machines.Count;
                buttonsInstances[i].gameObject.SetActive(hasData);

                if (hasData)
                    buttonsInstances[i].SetData(data.Machines[i], _machinePopup);
            }
        }

        private MachineButton CreateButton() => Instantiate(buttonPrefab, parent);

        public class Data
        {
            public Data(RoomInformation roomInformation)
            {
                RoomName = roomInformation.Title;
                Machines = roomInformation.MachinesInformation;
            }

            public IReadOnlyList<MachineInformation> Machines { get; }

            public string RoomName { get; }
        }
    }
}