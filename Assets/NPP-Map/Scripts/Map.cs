using UnityEngine;

namespace NPPMap
{
    [CreateAssetMenu(fileName = nameof(Map), menuName = Constants.ProjectName + "/" + nameof(Map))]
    public class Map : ScriptableObject
    {
        [SerializeField] private Sprite map;
    }
}