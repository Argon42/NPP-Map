using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace NPPMap.MapCreating.MapTemplate
{
    public class MapTemplateSettings : MonoBehaviour
    {
        [SerializeField] private Button changeImage;
        [SerializeField] private Button enableMove;
        [SerializeField] private Button enableScale;
        [SerializeField] private Button deleteTemplate;
        [SerializeField] private Button closeSettings;

        private MapTemplate _template;
        private float _duration = 0.3f;

        public void CloseSettings()
        {
            changeImage.onClick.RemoveListener(ChangeImage);
            enableMove.onClick.RemoveListener(EnableMove);
            enableScale.onClick.RemoveListener(EnableScale);
            deleteTemplate.onClick.RemoveListener(DeleteTemplate);
            closeSettings.onClick.RemoveListener(CloseSettings);

            Hide();
        }

        public void OpenSettings(MapTemplate mapTemplate, Vector2 position)
        {
            SetPosition(position);


            changeImage.onClick.AddListener(ChangeImage);
            enableMove.onClick.AddListener(EnableMove);
            enableScale.onClick.AddListener(EnableScale);
            deleteTemplate.onClick.AddListener(DeleteTemplate);
            closeSettings.onClick.AddListener(CloseSettings);

            _template = mapTemplate;
            Show();
        }

        private void SetPosition(Vector2 position)
        {
            var rectTransform = (RectTransform)transform;
            var canvas = ((RectTransform)GetComponentInParent<Canvas>().transform);
            var normalPosition = new Vector2(
                Mathf.InverseLerp(0, Screen.width, position.x),
                Mathf.InverseLerp(0, Screen.height, position.y));
            var screenPosition = new Vector2(
                Mathf.Lerp(0, canvas.sizeDelta.x, normalPosition.x),
                Mathf.Lerp(0, canvas.sizeDelta.y, normalPosition.y));
            rectTransform.localPosition = screenPosition - canvas.sizeDelta / 2;
        }

        private void ChangeImage()
        {
            CloseSettings();
            _template.ChooseImage();
        }

        private void DeleteTemplate()
        {
            CloseSettings();
            _template.DisableTemplate();
        }

        private void EnableMove()
        {
            CloseSettings();
            _template.EnableMove();
        }

        private void EnableScale()
        {
            CloseSettings();
            _template.EnableScale();
        }

        private void Hide()
        {
            transform.DOScale(Vector3.zero, _duration)
                .onComplete += () => gameObject.SetActive(false);
        }

        private void Show()
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            transform.DOScale(Vector3.one, _duration);
        }
    }
}