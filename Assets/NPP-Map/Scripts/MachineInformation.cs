using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = nameof(MachineInformation),
        menuName = Constants.ProjectName + "/" + nameof(MachineInformation))]
    public class MachineInformation : ScriptableObject
    {
        [SerializeField] private string machineName;
        [SerializeField] private string description;
        [SerializeField] private Vector2 positionOnMap;

        public string MachineName => machineName;
        public string Description => description;
        public Vector2 PositionOnMap => positionOnMap;
    }
}