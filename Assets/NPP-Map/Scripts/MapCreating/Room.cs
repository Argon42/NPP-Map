using System;
using System.Collections.Generic;
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
        }
    }

    public class RoomData
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public List<Vector2> Points { get; set; } = new List<Vector2>();

        public List<RoomObjectData> RoomObjects { get; set; } = new List<RoomObjectData>();

        public Vector3 RoomPosition { get; set; }

        public RoomData()
        {
        }

        public RoomData(List<Vector2> roomPoints, Vector3 roomPosition)
        {
            if (roomPoints == null || roomPoints.Count < 3)
                throw new ArgumentException("Incorrect room, minimum 3 points");
            Points = roomPoints;
            RoomPosition = roomPosition;
        }
    }

    public class RoomObjectData
    {
        public string Description { get; set; }
        public string Name { get; set; }
    }
}