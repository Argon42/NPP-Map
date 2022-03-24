using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace NPPMap.MapCreating
{
    internal class RoomPainter : MonoBehaviour, IRoomPainter
    {
        private const int CountOfAxis = 8;
        [SerializeField] private new Camera camera;
        [SerializeField] private MouseButton mouseButton;
        private PainterState _currentState = PainterState.None;

        private bool _enable;
        private Vector2 _startPoint;
        private Func<(bool successful, Vector2 position)> _tryGetLastPoint;

        private void Update()
        {
            if (_enable == false)
                return;

            ClickCalculation();
        }

        public event Action<Vector2> OnSetPoint;
        public event Action<Vector2> OnStartPaintWall;
        public event Action<Vector2> OnChangeEndWallPosition;
        public event Action OnPaintCanceled;

        public void Init(Func<(bool successful, Vector2 position)> tryGetLastPoint)
        {
            _tryGetLastPoint = tryGetLastPoint;
        }

        public void Disable()
        {
            _enable = false;

            if (_currentState != PainterState.None)
                OnPaintCanceled?.Invoke();
            _currentState = PainterState.None;
        }

        public void Enable()
        {
            _enable = true;
        }

        private void ClickCalculation()
        {
            bool pointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
            bool buttonDown = Input.GetMouseButtonDown((int)mouseButton);
            bool buttonUp = Input.GetMouseButtonUp((int)mouseButton);
            bool buttonPressed = Input.GetMouseButton((int)mouseButton);

            if (pointerOverGameObject && buttonUp)
            {
                OnPaintCanceled?.Invoke();
                _currentState = PainterState.None;
                return;
            }

            if (pointerOverGameObject)
            {
                return;
            }

            Vector2 pointPosition = GetMousePosition();

            pointPosition = ProcessShiftPress(pointPosition);
            ProcessMouseClick(buttonDown, pointPosition, buttonUp, buttonPressed);
        }

        private Vector2 ProcessShiftPress(Vector2 pointPosition)
        {
            if (Input.GetKey(KeyCode.LeftShift) == false)
                return pointPosition;

            if (_tryGetLastPoint == null)
                return RecalculateLine(_startPoint, pointPosition);

            (bool successful, Vector2 position) = _tryGetLastPoint();
            return RecalculateLine(successful ? position : _startPoint, pointPosition);
        }

        private Vector2 RecalculateLine(Vector2 startPoint, Vector2 endPoint)
        {
            const float circle = 360f;
            const float halfCircle = 360 / 2f;
            const float pie = circle / CountOfAxis;
            
            float distance = Vector2.Distance(startPoint, endPoint);
            float signedAngle = Vector2.SignedAngle(Vector2.right, endPoint - startPoint);
            float angle = signedAngle + halfCircle;
            float niceAngle = Mathf.Floor(angle / pie + 0.5f) * pie - halfCircle;

            float x = Mathf.Cos(niceAngle / Mathf.Rad2Deg);
            float y = Mathf.Sin(niceAngle / Mathf.Rad2Deg);

            return startPoint + new Vector2(x, y) * distance;
        }

        private void ProcessMouseClick(bool buttonDown, Vector2 pointPosition, bool buttonUp, bool buttonPressed)
        {
            if (buttonDown)
            {
                _currentState = PainterState.StartPaint;
                _startPoint = pointPosition;
                OnStartPaintWall?.Invoke(_startPoint);
            }
            else if (buttonUp)
            {
                _currentState = PainterState.None;
                OnSetPoint?.Invoke(pointPosition);
            }
            else if (buttonPressed)
            {
                _currentState = PainterState.Painting;
                OnChangeEndWallPosition?.Invoke(pointPosition);
            }
        }

        private Vector2 GetMousePosition() => camera.ScreenToWorldPoint(Input.mousePosition);

        private enum PainterState
        {
            None,
            StartPaint,
            Painting
        }

        private enum MouseButton
        {
            Left = 0,
            Right = 1
        }
    }
}