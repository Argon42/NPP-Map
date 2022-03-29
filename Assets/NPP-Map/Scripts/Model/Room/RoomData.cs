using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPPMap.MapCreating
{
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
}