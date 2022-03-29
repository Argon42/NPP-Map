using System.Linq;
using mattatz.Triangulation2DSystem;
using UnityEngine;

namespace NPPMap.MapCreating
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private LineRenderer lineRenderer;

        private Vector2[] _points;

        public void Init(Vector2[] roomSketchPoints)
        {
            _points = roomSketchPoints;

            var triangulation = new Triangulation2D(Polygon2D.Contour(roomSketchPoints));
            meshFilter.mesh = triangulation.Build();

            lineRenderer.loop = true;
            lineRenderer.positionCount = roomSketchPoints.Length;
            lineRenderer.SetPositions(roomSketchPoints.Select(point => (Vector3)point).ToArray());
        }
    }
}