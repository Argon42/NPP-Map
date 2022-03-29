using System.Linq;
using mattatz.Triangulation2DSystem;
using UnityEngine;

namespace NPPMap.MapCreating
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private LineRenderer lineRenderer;

        public RoomData Data { get; private set; }

        public void Init(RoomData data)
        {
            Data = data;
            Vector2[] roomPoints = data.Points.ToArray();

            var triangulation = new Triangulation2D(Polygon2D.Contour(roomPoints));
            Mesh mesh = triangulation.Build();
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            lineRenderer.loop = true;
            lineRenderer.positionCount = roomPoints.Length;

            Vector3[] positions = roomPoints.Select(PointsForLine).ToArray();
            lineRenderer.SetPositions(positions);

            Vector3 PointsForLine(Vector2 point) => (Vector3)point + Vector3.forward * -0.01f;
            OnInit();
        }

        protected virtual void OnInit()
        {
        }
    }
}