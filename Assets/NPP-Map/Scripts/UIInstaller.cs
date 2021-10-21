using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace NPPMap
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private RoomInformationPopup roomInformationPopup;
        [SerializeField] private ListMachinePopup listMachinePopup;
        [SerializeField] private MachinePopup machinePopup;

        public override void InstallBindings()
        {
            Container.Bind<RoomInformationPopup>().FromInstance(roomInformationPopup).AsSingle();
            Container.Bind<ListMachinePopup>().FromInstance(listMachinePopup).AsSingle();
            Container.Bind<MachinePopup>().FromInstance(machinePopup).AsSingle();
        }
    }
}