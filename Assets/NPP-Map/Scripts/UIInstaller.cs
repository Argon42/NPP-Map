using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace NPPMap
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MapObject map;
        [SerializeField] private RoomInformationPopup roomInformationPopup;
        [SerializeField] private ListMachinePopup listMachinePopup;
        [SerializeField] private MachinePopup machinePopup;
        [SerializeField] private SearchPopup searchPopup;

        public override void InstallBindings()
        {
            Container.Bind<MapObject>().FromInstance(map).AsSingle();
            Container.Bind<RoomInformationPopup>().FromInstance(roomInformationPopup).AsSingle();
            Container.Bind<ListMachinePopup>().FromInstance(listMachinePopup).AsSingle();
            Container.Bind<MachinePopup>().FromInstance(machinePopup).AsSingle();
            Container.Bind<SearchPopup>().FromInstance(searchPopup).AsSingle();
        }
    }
}