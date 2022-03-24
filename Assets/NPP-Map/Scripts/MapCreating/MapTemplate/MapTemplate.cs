using System.Linq;
using NPPMap.Utility;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NPPMap.MapCreating.MapTemplate
{
    public class MapTemplate : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RawImage image;
        [SerializeField] private MapTemplateSettings prefab;

        private MapTemplateSettings _settings;

        public void Init(Camera cameraForCanvas)
        {
            canvas.worldCamera = cameraForCanvas;
        }

        public void ChooseImage()
        {
            FileBrowser.ShowLoadDialog(paths => SetImage(paths.FirstOrDefault()), null, FileBrowser.PickMode.Files);
        }

        public void CloseSettings()
        {
            _settings.CloseSettings();
        }

        public void DisableTemplate()
        {
            Destroy(gameObject);
        }

        public void EnableMove()
        {
        }

        public void EnableScale()
        {
        }

        public void OpenSettings(Vector2 position)
        {
            if (_settings == null)
                _settings = Instantiate(prefab, transform);
            _settings.OpenSettings(this, position);
        }

        private async void SetImage(string path)
        {
            UnityWebRequest imageLoading = UnityWebRequestTexture.GetTexture(path);
            await imageLoading.SendWebRequest();

            if (string.IsNullOrEmpty(imageLoading.error) == false)
                return;

            Texture2D texture = DownloadHandlerTexture.GetContent(imageLoading);
            SetImage(texture);
        }

        private void SetImage(Texture2D texture)
        {
            image.texture = texture;

            Vector2 size = image.rectTransform.sizeDelta;
            size.y = size.x / texture.width * texture.height;
            image.rectTransform.sizeDelta = size;
        }
    }
}