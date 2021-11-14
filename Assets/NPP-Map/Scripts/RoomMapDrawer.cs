using DG.Tweening;
using NPPMap.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace NPPMap
{
    public class RoomMapDrawer : MonoBehaviour
    {
        [SerializeField] private Image map;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform parent;

        [SerializeField] private Pool<MapMachineButton, MachineInformation> machines;
        private bool _isOpened;

        [Inject] private MachinePopup _machinePopup;

        private RoomInformation _room;

        public void Open(RoomInformation roomInformation)
        {
            TryInitPool();

            if (_isOpened == false)
                Open();

            _room = roomInformation;
            map.sprite = _room.LoadMap();
            title.text = _room.Title;

            machines.SetupData(roomInformation.MachinesInformation);
        }

        public void Close()
        {
            ((RectTransform) transform).DOAnchorPosX(100, 0.3f);
            canvasGroup.DOFade(0, 0.3f).onComplete += () =>
            {
                gameObject.SetActive(false);
                _isOpened = false;
            };
        }

        private void TryInitPool()
        {
            if (machines.Initialized)
                return;

            machines.Init(
                button => MapMachineButton.Create(button, parent, _machinePopup),
                (button, information) =>
                {
                    button.SetData(information);
                    button.SetPosition(information.PositionOnMap);
                }
            );
        }

        private void Open()
        {
            var rectTransform = ((RectTransform) transform);
            rectTransform.anchoredPosition = Vector2.right * 100;
            rectTransform.DOAnchorPosX(1, 0.3f);
            canvasGroup.DOFade(1, 0.3f);
            gameObject.SetActive(true);
            _isOpened = true;
        }
    }
}