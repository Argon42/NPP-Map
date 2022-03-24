using System;
using UnityEngine;


namespace NPPMap.MapCreating
{
    internal class ViewState : MonoBehaviour, IState
    {
        [SerializeField] private GameObject buttons;
        
        public void Disable()
        {
            buttons.SetActive(false);
        }

        public void Enable()
        {
            buttons.SetActive(true);
        }
    }
}