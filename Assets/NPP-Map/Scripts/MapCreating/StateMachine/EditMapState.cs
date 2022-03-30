using System.IO;
using Newtonsoft.Json;
using NPPMap.MapCreating;
using SimpleFileBrowser;
using UnityEngine;

namespace NPPMap
{
    public class EditMapState : MonoBehaviour, IState
    {
        [SerializeField] private GameObject stateUI;
        [SerializeField] private EditableRoom roomPrefab;
        [SerializeField] private Transform parentForRooms;

        private RoomFactory<EditableRoom> _factory;
        private Map<EditableRoom> _map;


        private void Awake()
        {
            _factory = new RoomFactory<EditableRoom>(roomPrefab, parentForRooms);
        }

        public void Disable()
        {
            ClearMap();
            stateUI.SetActive(false);
        }

        public void ClearMap()
        {
            _map?.Clear();
        }

        public void Enable()
        {
            stateUI.SetActive(true);
        }

        public void LoadMap()
        {
            FileBrowser.ShowLoadDialog(paths => LoadMap(paths[0]), null, FileBrowser.PickMode.Files);
        }

        public void SaveMap()
        {
            FileBrowser.ShowSaveDialog(paths => SaveMap(paths[0]), null, FileBrowser.PickMode.Files);
        }

        private void LoadMap(string path)
        {
            string mapJson = File.ReadAllText(path);
            ClearMap();
            _map = _factory.CreateMap(mapJson);
        }

        private void SaveMap(string path)
        {
            string json = JsonConvert.SerializeObject(_map.MapData);
            File.WriteAllText(path, json);
        }
    }
}