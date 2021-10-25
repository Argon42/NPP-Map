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

        public RoomInformation Information => roomInformation;

        private void OnEnable()
        {
            roomName.text = Information.Title;
        }

        public void Open()
        {
            _roomInformationPopup.Open(Information, transform.position);
        }
    }
}