using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class RoomButton : MonoBehaviour
    {
        [SerializeField] private RoomInformation roomInformation;

        [Inject] private RoomInformationPopup _roomInformationPopup;

        public void Open()
        {
            _roomInformationPopup.Open(roomInformation, transform.position);
        }
    }
}