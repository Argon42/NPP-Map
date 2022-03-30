using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NPPMap.MapCreating
{
    public class RoomFactory<T> where T : Room
    {
        private readonly T _prefab;
        private readonly Transform _parent;

        public RoomFactory(T roomPrefab, Transform parentForRooms = null)
        {
            _prefab = roomPrefab;
            _parent = parentForRooms;
        }

        public Map<T> CreateMap(string mapJson)
        {
            if (string.IsNullOrEmpty(mapJson))
                throw new ArgumentException("Json is empty or null");

            var mapData = JsonConvert.DeserializeObject<MapData>(mapJson);
            if (mapData == null)
                throw new ArgumentException("Json is incorrect");

            return new Map<T>(mapData, CreateRooms(mapData));
        }

        public T CreateRoom(RoomData roomData)
        {
            T room = Object.Instantiate(_prefab, roomData.RoomPosition, Quaternion.identity, _parent);
            room.Init(roomData);
            return room;
        }

        public List<T> CreateRooms(MapData mapData)
        {
            var result = new List<T>();
            foreach (RoomData roomData in mapData.Rooms)
                result.Add(CreateRoom(roomData));

            return result;
        }
    }
}