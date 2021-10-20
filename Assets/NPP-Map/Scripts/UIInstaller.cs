using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private RoomInformationPopup roomInformationPopup;
        [SerializeField] private MachineInformationPopup machineInformationPopup;

        public override void InstallBindings()
        {
            Container.Bind<RoomInformationPopup>().FromInstance(roomInformationPopup).AsSingle();
            Container.Bind<MachineInformationPopup>().FromInstance(machineInformationPopup).AsSingle();
        }
    }
}