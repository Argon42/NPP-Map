using System.Collections.Generic;
using UnityEngine;


namespace NPPMap.MapCreating
{
    public interface IRoomVisualizer
    {
        void Visualize(IReadOnlyCollection<Vector2> pointsChain);
        void Clear();
    }
}