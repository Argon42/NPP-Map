using System.Collections.Generic;

namespace NPPMap.MapCreating
{
    /// <summary>
    /// Плоский класс с данными комнат
    /// </summary>
    public class MapData
    {
        public List<RoomData> Rooms { get; set; } = new List<RoomData>();
    }
}