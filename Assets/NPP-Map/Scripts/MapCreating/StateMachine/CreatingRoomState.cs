using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NPPMap.MapCreating.MapTemplate;
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
        [SerializeField] private MapTemplateCreator templateCreator;

        private RoomSketch _roomSketch;
        private float _offsetZ = 10;

        private RoomFactory<Room> _roomFactory;
        private Map<Room> _map;

        private void Start()
        {
            _roomFactory = new RoomFactory<Room>(roomPrefab, parentForRooms);
            _roomSketch = new RoomSketch(simpleRoomVisualizer);
            roomPainter.Init(_roomSketch.TryGetLastPoint);
            simpleWallVisualizer.Init(roomPainter, _roomSketch.TryGetLastPoint);
        }

        public void ClearMap()
        {
            _map.Clear();
        }

        public void ClearRoom()
        {
            _roomSketch.Clear();
        }

        public void Disable()
        {
            _roomSketch.Clear();
            roomPainter.Disable();
            simpleWallVisualizer.Disable();
            templateCreator.CloseAllTemplates();
            
            ClearRoom();
            ClearMap();

            roomPainter.OnSetPoint -= _roomSketch.AddPoint;
            createRoomUI.SetActive(false);
        }

        public void Enable()
        {
            _map = new Map<Room>(new MapData(), new List<Room>());
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
            _map.AddRoom(CreateRoom(_roomSketch.Points));
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

            return _roomFactory.CreateRoom(roomData);
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
            string mapJson = File.ReadAllText(path);
            ClearMap();
            _map = _roomFactory.CreateMap(mapJson);
        }

        private void SaveMap(string path)
        {
            string json = JsonConvert.SerializeObject(_map.MapData);
            File.WriteAllText(path, json);
        }
    }
}