using UnityEngine;

namespace NPPMap.MapCreating.MapTemplate
{
    public class MapTemplateCreator : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera;
        [SerializeField] private MapTemplate prefab;
        [SerializeField] private Transform parent;

        public void AddMapTemplate() => Instantiate(prefab, parent).Init(gameCamera);
    }
}