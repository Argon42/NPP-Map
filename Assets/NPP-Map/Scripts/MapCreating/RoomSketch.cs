using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPPMap.MapCreating
{
    public class RoomSketch
    {
        private readonly LinkedList<Vector2> _points = new LinkedList<Vector2>();
        private readonly IRoomVisualizer _roomVisualizer;

        public Vector2[] Points => _points.ToArray();

        public RoomSketch(IRoomVisualizer roomVisualizer)
        {
            _roomVisualizer = roomVisualizer;
        }

        public void AddPoint(Vector2 position)
        {
            _points.AddLast(position);
            _roomVisualizer.Visualize(_points.ToList());
        }

        public void Clear()
        {
            _points.Clear();
            _roomVisualizer.Clear();
        }

        public void RemoveLastPoint()
        {
            _points.RemoveLast();
            _roomVisualizer.Visualize(_points.ToList());
        }

        public (bool successful, Vector2 point) TryGetLastPoint()
        {
            if (_points.Count == 0)
                return (false, default);

            return (true, _points.Last());
        }
    }
}