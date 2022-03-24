using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NPPMap.MapCreating.MapTemplate
{
    public class MapTemplateInput : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MapTemplate mapTemplate;


        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.pointerId != -2)
                return;
            
            mapTemplate.OpenSettings(eventData.position);
        }
    }
}