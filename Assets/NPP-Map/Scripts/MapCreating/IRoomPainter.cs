using System;
using UnityEngine;


namespace NPPMap.MapCreating
{
    internal interface IRoomPainter
    {
        public event Action<Vector2> OnSetPoint;
        public event Action<Vector2> OnStartPaintWall;
        public event Action<Vector2> OnChangeEndWallPosition;
        public event Action OnPaintCanceled;
    }
}