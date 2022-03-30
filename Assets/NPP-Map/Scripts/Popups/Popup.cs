using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NPPMap
{
    public abstract class Popup<T> : MonoBehaviour, IDragHandler
    {
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private UnityEvent onStartOpen;

        public bool IsOpened { get; private set; }

        public async Task Close()
        {
            await AnimatePopup(GetDefaultScale(), Vector3.zero, animationDuration);
            gameObject.SetActive(false);
            IsOpened = false;
        }

        public void CloseSync() => Close();

        public virtual void OnDrag(PointerEventData eventData) =>
            SetPopupPosition(transform.position + (Vector3)eventData.delta);

        public async Task Open(T data, Vector3 position)
        {
            if (IsOpened)
                await Close();

            SetData(data);
            SetPopupPosition(position);
            gameObject.SetActive(true);
            await Task.Yield();
            onStartOpen?.Invoke();
            await AnimatePopup(Vector3.zero, GetDefaultScale(), animationDuration);

            IsOpened = true;
        }

        public virtual void SetPopupPosition(Vector3 position)
        {
            var rectTransform = (RectTransform)transform;
            var canvas = rectTransform.root.GetComponentInChildren<Canvas>();
            Vector2 sizeDelta = rectTransform.sizeDelta;
            float positionY = position.y - rectTransform.pivot.y * sizeDelta.y + sizeDelta.y;
            float height = ((RectTransform)canvas.transform).sizeDelta.y;

            rectTransform.position = position;
            if (positionY >= height)
                rectTransform.position -= Vector3.up * (positionY - height);
        }

        protected virtual Vector3 GetDefaultScale() => Vector3.one;

        protected abstract void SetData(T data);

        private async Task AnimatePopup(Vector3 start, Vector3 end, float duration)
        {
            var timer = 0f;
            while (timer < duration)
            {
                await Task.Yield();
                timer += Time.deltaTime;
                transform.localScale = Vector3.Lerp(start, end, timer / duration);
            }
        }
    }
}