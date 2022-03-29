using UnityEngine;
using UnityEngine.EventSystems;

namespace NPPMap
{
    public class InputModule : StandaloneInputModule
    {
        public GameObject GameObjectUnderPointer(int pointerId = kMouseLeftId)
        {
            PointerEventData lastPointer = GetLastPointerEventData(pointerId);
            if (lastPointer != null)
                return lastPointer.pointerCurrentRaycast.gameObject;

            return null;
        }
    }
}