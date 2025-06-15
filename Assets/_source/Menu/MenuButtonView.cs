using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace GameMenu
{
    internal class MenuButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _localizationKey;

        public void OnPointerEnter(PointerEventData eventData) => OnSelectAnimation();
        public void OnPointerExit(PointerEventData eventData) => OnDeselectAnimation();

        internal void UpdateText() => _text.text = _localizationKey;
        internal virtual void Init() { }

        internal virtual void OnSelectAnimation() { }
        internal virtual void OnDeselectAnimation() { }
    }
}