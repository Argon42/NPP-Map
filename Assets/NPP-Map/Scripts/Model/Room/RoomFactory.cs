using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NPPMap.MapCreating
{
    public class RoomFactory
    {
        private readonly Room _prefab;
        private readonly Transform _parent;

        public RoomFactory(Room roomPrefab, Transform parentForRooms = null)
        {
            _prefab = roomPrefab;
            _parent = parentForRooms;
        }

        public Map CreateMap(string mapJson)
        {
            if (string.IsNullOrEmpty(mapJson))
                throw new ArgumentException("Json is empty or null");

            var mapData = JsonConvert.DeserializeObject<MapData>(mapJson);
            if (mapData == null)
                throw new ArgumentException("Json is incorrect");

            return new Map(mapData, CreateRooms(mapData));
        }

        public Room CreateRoom(RoomData roomData)
        {
            Room room = Object.Instantiate(_prefab, roomData.RoomPosition, Quaternion.identity, _parent);
            room.Init(roomData);
            return room;
        }

        public List<Room> CreateRooms(MapData mapData)
        {
            var result = new List<Room>();
            foreach (RoomData roomData in mapData.Rooms)
                result.Add(CreateRoom(roomData));

            return result;
        }
    }
}