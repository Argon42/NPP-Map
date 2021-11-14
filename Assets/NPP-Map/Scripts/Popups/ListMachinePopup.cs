using System.Collections.Generic;
using NPPMap.Utility;
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
        [SerializeField] private Pool<MachineButton, MachineInformation> pool;

        [Inject] private MachinePopup _machinePopup;

        private void TryInit()
        {
            if(pool.Initialized)
                return;
            pool.Init(
                button => MachineButton.Create(button, parent, _machinePopup),
                (button, information) => button.SetData(information)
            );
        }

        protected override void SetData(Data data)
        {
            TryInit();
            roomTitle.text = $"{RoomTitle} {data.RoomName}";
            pool.SetupData(data.Machines);
        }

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