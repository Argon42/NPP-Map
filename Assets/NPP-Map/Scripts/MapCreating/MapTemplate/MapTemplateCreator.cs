using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPPMap.MapCreating.MapTemplate
{
    public class MapTemplateCreator : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera;
        [SerializeField] private MapTemplate prefab;
        [SerializeField] private Transform parent;
        
        private readonly List<MapTemplate> _templates = new List<MapTemplate>();

        public void CloseAllTemplates()
        {
            foreach (MapTemplate mapTemplate in _templates.Where(template => template).ToList())
            {
                mapTemplate.DisableTemplate();
            }
        }

        public void AddMapTemplate()
        {
            MapTemplate mapTemplate = Instantiate(prefab, parent);
            mapTemplate.Init(gameCamera);
            mapTemplate.OnDestroyTemplate += () => _templates.Remove(mapTemplate);
            _templates.Add(mapTemplate);
        }
    }
}