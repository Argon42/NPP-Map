using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace NPPMap.MapCreating
{
    /// <summary>
    /// Модель с объектами созданными на сцене
    /// </summary>
    public class Map<T> where T : Room
    {
        public MapData MapData { get; set; }
        public List<T> Rooms { get; set; }
        
        public Map(MapData mapData, List<T> createRooms)
        {
            MapData = mapData;
            Rooms = createRooms ?? throw new ArgumentNullException(nameof(createRooms));
        }

        public void Clear()
        {
            Rooms.ForEach(room => Object.Destroy(room.gameObject));
            Rooms.Clear();
        }

        public void AddRoom(T createRoom)
        {
            MapData.Rooms.Add(createRoom.Data);
            Rooms.Add(createRoom);
        }
    }
}