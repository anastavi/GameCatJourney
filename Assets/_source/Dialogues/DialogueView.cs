using TMPro;
using UnityEngine;

namespace GameDialogues
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minX;

        internal void Show(Vector3 position, string text)
        {
            SetText(text);
            transform.position = position;
            gameObject.SetActive(true);
        }

        internal void Hide()
        {
            _text.text = string.Empty;
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }

        internal void SetText(string text)
        {
            _text.text = text;
            //FitToText();
        }

        [ContextMenu(nameof(FitToText))]
        private void FitToText()
        {
            RefreshTextSize();
            Vector2 size = _text.GetPreferredValues();

            float x = size.x <= _minX ? _minX : size.x;
            float y = size.y;

            if (x > _maxX)
            {
                int xCount = (int)(x / _maxX);
                x = _maxX;
                y *= xCount + 1;
            }

            _rect.sizeDelta = new(x, y);
            _text.rectTransform.sizeDelta = new(x, y);
        }

        private void RefreshTextSize()
        {
            _text.ForceMeshUpdate();
            _text.rectTransform.sizeDelta = new(0f, 0f);
        }
    }
}