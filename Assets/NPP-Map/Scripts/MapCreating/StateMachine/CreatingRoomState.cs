using UnityEngine;


namespace NPPMap.MapCreating
{
    internal class CreatingRoomState : MonoBehaviour, IState
    {
        [SerializeField] private RoomPainter roomPainter;
        [SerializeField] private SimpleRoomVisualizer simpleRoomVisualizer;
        [SerializeField] private SimpleWallVisualizer simpleWallVisualizer;
        [SerializeField] private GameObject createRoomUI;

        private Room _room;

        private void Start()
        {
            _room = new Room(simpleRoomVisualizer);
            roomPainter.Init(_room.TryGetLastPoint);
            simpleWallVisualizer.Init(roomPainter, _room.TryGetLastPoint);
        }

        public void Disable()
        {
            _room.Clear();
            roomPainter.Disable();
            simpleWallVisualizer.Disable();
            roomPainter.OnSetPoint -= _room.AddPoint;
            createRoomUI.SetActive(false);
        }

        public void Enable()
        {
            simpleWallVisualizer.Enable();
            roomPainter.Enable();
            roomPainter.OnSetPoint += _room.AddPoint;
            createRoomUI.SetActive(true);
        }

        public void Save()
        {
        }

        public void Undo()
        {
            _room.RemoveLastPoint();
        }

        public void Clear()
        {
            _room.Clear();
        }
    }
}