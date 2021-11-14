using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPPMap.Utility
{
    [Serializable]
    public class Pool<T, TData> where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] private List<T> instances = new List<T>(20);

        private Func<T, T> _createElement;
        private Action<T, TData> _onSetData;
        
        public bool Initialized { get; private set; }

        public void Init(Func<T, T> createElement, Action<T, TData> onSetData)
        {
            _createElement = createElement;
            _onSetData = onSetData;
            Initialized = true;
        }

        public void SetupData(IReadOnlyList<TData> data)
        {
            for (var i = 0; i < data.Count || i < instances.Count; i++)
            {
                if (i >= instances.Count)
                    instances.Add(_createElement(prefab));

                bool hasData = i < data.Count;
                instances[i].gameObject.SetActive(hasData);

                if (hasData)
                    _onSetData(instances[i], data[i]);
            }
        }
    }
}