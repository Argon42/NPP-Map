using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace NPPMap
{
    public class RoomButton : MonoBehaviour
    {
        [SerializeField] private RoomInformation roomInformation;
        [SerializeField] private TextMeshProUGUI roomName;

        [Inject] private RoomInformationPopup _roomInformationPopup;

        private void OnEnable()
        {
            roomName.text = roomInformation.RoomTitle;
        }

        public void Open()
        {
            _roomInformationPopup.Open(roomInformation, transform.position);
        }
    }
}