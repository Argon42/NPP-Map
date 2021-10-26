using System.Collections.Generic;
using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = nameof(Map), menuName = Constants.ProjectName + "/" + nameof(Map))]
    public class Map : ScriptableObject
    {
        [SerializeField] private Sprite map;
        [SerializeField] private List<RoomInformation> rooms;
    }
}