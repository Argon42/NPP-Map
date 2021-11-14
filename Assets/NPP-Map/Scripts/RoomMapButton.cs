using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace NPPMap
{
    [RequireComponent(typeof(Image))]
    public class RoomMapButton : MonoBehaviour
    {
        [SerializeField] private RoomInformation roomInformation;
        [SerializeField] private TextMeshProUGUI roomName;

        [Inject] private RoomInformationPopup _roomInformationPopup;
        [Inject] private RoomMapDrawer _map;

        public RoomInformation Information => roomInformation;


        private void Awake()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.3f;
        }

        private void OnEnable()
        {
            roomName.text = Information.Title;
        }

        public void Open()
        {
            _roomInformationPopup.Open(Information);
            _map.Open(roomInformation);
        }
    }
}