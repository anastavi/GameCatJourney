using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameMenu
{
    [RequireComponent(typeof(Button), typeof(MenuButtonView))]
    internal class MenuButton : MonoBehaviour
    {
        private MenuButtonView _view;
        private Button _button;
        private Action _onClickAction;

        private void Awake() => Construct();

        internal void Init(Action onClick)
        {
            _onClickAction = onClick;
            _view.Init();
        }

        internal void OnEnable() => _button.onClick.AddListener(Click);
        internal void OnDisable() => _button.onClick.RemoveListener(Click);

        internal void UpdateText() => _view.UpdateText();
        internal void SetSelectView() => _view.OnSelectAnimation();
        internal void SetDeselectView() => _view.OnDeselectAnimation();
        internal void Click() => _onClickAction?.Invoke();

        private void Construct()
        {
            _button = GetComponent<Button>();
            _view = GetComponent<MenuButtonView>();
        }
    }
}