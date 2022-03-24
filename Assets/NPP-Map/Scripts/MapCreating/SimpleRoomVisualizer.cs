using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions;


namespace NPPMap.MapCreating
{
    internal class SimpleRoomVisualizer : MonoBehaviour, IRoomVisualizer
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float minimalDistance = 0.3f;

        private float _distance = 0;

        public void Visualize(IReadOnlyCollection<Vector2> pointsChain)
        {
            if (pointsChain == null || pointsChain.Count == 0)
                throw new ArgumentException();

            lineRenderer.positionCount = pointsChain.Count;
            lineRenderer.SetPositions(pointsChain.Select(ConvertToPosition).ToArray());
            lineRenderer.loop = Vector2.Distance(pointsChain.First(), pointsChain.Last()) < minimalDistance;
            
            if (pointsChain.Count == 1)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(1, ConvertToPosition(pointsChain.First()));
                lineRenderer.loop = false;
            }
            
            Vector3 ConvertToPosition(Vector2 position) => (Vector3)position + Vector3.forward * _distance;
        }

        public void Clear()
        {
            lineRenderer.positionCount = 0;
            lineRenderer.SetPositions(Array.Empty<Vector3>());
        }
    }
}