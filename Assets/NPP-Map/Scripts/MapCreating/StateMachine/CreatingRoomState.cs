using System.Collections.Generic;
using UnityEngine;

namespace NPPMap.MapCreating
{
    internal class CreatingRoomState : MonoBehaviour, IState
    {
        [SerializeField] private RoomPainter roomPainter;
        [SerializeField] private SimpleRoomVisualizer simpleRoomVisualizer;
        [SerializeField] private SimpleWallVisualizer simpleWallVisualizer;
        [SerializeField] private GameObject createRoomUI;
        [SerializeField] private Room roomPrefab;
        [SerializeField] private Transform parentForRooms;

        private RoomSketch _roomSketch;
        private readonly List<Room> _rooms = new List<Room>();

        private void Start()
        {
            _roomSketch = new RoomSketch(simpleRoomVisualizer);
            roomPainter.Init(_roomSketch.TryGetLastPoint);
            simpleWallVisualizer.Init(roomPainter, _roomSketch.TryGetLastPoint);
        }

        public void Clear()
        {
            _roomSketch.Clear();
        }

        public void Disable()
        {
            _roomSketch.Clear();
            roomPainter.Disable();
            simpleWallVisualizer.Disable();
            roomPainter.OnSetPoint -= _roomSketch.AddPoint;
            createRoomUI.SetActive(false);
        }

        public void Enable()
        {
            simpleWallVisualizer.Enable();
            roomPainter.Enable();
            roomPainter.OnSetPoint += _roomSketch.AddPoint;
            createRoomUI.SetActive(true);
        }

        public void Save()
        {
            _rooms.Add(CreateRoom(_roomSketch.Points));
            _roomSketch.Clear();
        }

        public void Undo()
        {
            _roomSketch.RemoveLastPoint();
        }

        private Room CreateRoom(Vector2[] roomSketchPoints)
        {
            Room room = Instantiate(roomPrefab, parentForRooms);
            room.Init(roomSketchPoints);
            return room;
        }
    }
}