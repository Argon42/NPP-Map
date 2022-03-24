using System;
using UnityEngine;


namespace NPPMap.MapCreating
{
    internal class SimpleWallVisualizer : MonoBehaviour, IWallVisualizer
    {
        [SerializeField] private LineRenderer lineRenderer;
        private float _distance = 0;

        private IRoomPainter _roomPainter;
        private Func<(bool succesfull, Vector2 point)> _tryGetLastWallPoint;

        public void Disable()
        {
            _roomPainter.OnSetPoint -= OnSetPoint;
            _roomPainter.OnStartPaintWall -= OnStartPaintWall;
            _roomPainter.OnChangeEndWallPosition -= OnChangeEndWallPosition;
            _roomPainter.OnPaintCanceled -= OnPaintCanceled;
        }

        public void Enable()
        {
            _roomPainter.OnSetPoint += OnSetPoint;
            _roomPainter.OnStartPaintWall += OnStartPaintWall;
            _roomPainter.OnChangeEndWallPosition += OnChangeEndWallPosition;
            _roomPainter.OnPaintCanceled += OnPaintCanceled;
        }

        public void Init(IRoomPainter roomPainter, Func<(bool successful, Vector2 point)> tryGetLastWallPoint)
        {
            _roomPainter = roomPainter;
            _tryGetLastWallPoint = tryGetLastWallPoint;
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        private void OnSetPoint(Vector2 position) => ClearPreview();

        private void OnStartPaintWall(Vector2 position) => ShowPreview(position);

        private void OnChangeEndWallPosition(Vector2 position) => ShowPreview(position);

        private void OnPaintCanceled() => ClearPreview();

        private void ShowPreview(Vector2 position)
        {
            (bool successful, Vector2 lastWallPoint) = _tryGetLastWallPoint();
            if (successful == false)
            {
                lastWallPoint = position;
            }

            Vector3 offset = Vector3.forward * _distance;
            lineRenderer.SetPosition(0, (Vector3)lastWallPoint + offset);
            lineRenderer.SetPosition(1, (Vector3)position + offset);

            lineRenderer.enabled = true;
        }

        private void ClearPreview()
        {
            lineRenderer.enabled = false;
        }
    }
}