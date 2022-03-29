using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleFileBrowser;
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
        private float _offsetZ = 10;

        private void Start()
        {
            _roomSketch = new RoomSketch(simpleRoomVisualizer);
            roomPainter.Init(_roomSketch.TryGetLastPoint);
            simpleWallVisualizer.Init(roomPainter, _roomSketch.TryGetLastPoint);
        }

        public void ClearRoom()
        {
            _roomSketch.Clear();
        }

        public void ClearMap()
        {
            _rooms.ForEach(room => Destroy(room.gameObject));
            _rooms.Clear();
        }

        public void Disable()
        {
            _roomSketch.Clear();
            roomPainter.Disable();
            simpleWallVisualizer.Disable();

            ClearRoom();
            ClearMap();
            
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

        public void LoadMap()
        {
            FileBrowser.ShowLoadDialog(paths => LoadMap(paths[0]), null, FileBrowser.PickMode.Files);
        }

        public void SaveMap()
        {
            FileBrowser.ShowSaveDialog(paths => SaveMap(paths[0]), null, FileBrowser.PickMode.Files);
        }

        public void SaveRoom()
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
            Vector2 center = FindCenter(roomSketchPoints);
            Vector3 roomPosition = (Vector3)center + Vector3.forward * _offsetZ;
            var roomData = new RoomData(roomSketchPoints.Select(point => point - center).ToList(), roomPosition);

            return CreateRoom(roomData);
        }

        private Room CreateRoom(RoomData roomData)
        {
            Room room = Instantiate(roomPrefab, roomData.RoomPosition, Quaternion.identity, parentForRooms);
            room.Init(roomData);
            return room;
        }

        private Vector2 FindCenter(Vector2[] roomSketchPoints)
        {
            Vector2 max = Vector2.negativeInfinity;
            Vector2 min = Vector2.positiveInfinity;
            foreach (Vector2 point in roomSketchPoints)
            {
                if (point.x > max.x)
                    max.x = point.x;
                if (point.x < min.x)
                    min.x = point.x;

                if (point.y > max.y)
                    max.y = point.y;
                if (point.y < min.y)
                    min.y = point.y;
            }

            Vector2 center = min + (max - min) / 2;
            return center;
        }

        private void LoadMap(string path)
        {
            string json = File.ReadAllText(path);

            if (string.IsNullOrEmpty(json))
                return;

            var rooms = JsonConvert.DeserializeObject<List<RoomData>>(json);
            if (rooms == null)
                return;

            ClearMap();
            foreach (RoomData roomData in rooms)
                _rooms.Add(CreateRoom(roomData));
        }

        private void SaveMap(string path)
        {
            string json = JsonConvert.SerializeObject(_rooms.Select(room => room.Data).ToList());
            File.WriteAllText(path, json);
        }
    }
}