using System;
using UnityEngine;


namespace NPPMap.MapCreating
{
    internal class MenuState : MonoBehaviour, IState
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