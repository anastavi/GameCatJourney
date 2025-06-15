using System;
using UnityEngine;
using TMPro;
using GameContracts;

namespace GameMenu
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ConfirmMenu : MonoBehaviour, IConfirmSuggestion
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private MenuButton _yes;
        [SerializeField] private MenuButton _no;

        private IInterfaceInput _input;
        private Action _confirmAction;
        private Action _declineAction;
        private CanvasGroup _canvasGroup;
        private ButtonSelector _selector;

        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

        public void Init(IInterfaceInput input)
        {
            _input = input;
            var buttons = new MenuButton[] { _no, _yes };
            _selector = new(buttons, _input);

            SetEmptyActions();
            SetActive(false);

            _yes.Init(OnYesClick);
            _no.Init(OnNoClick);
        }

        private void Update() => _selector.Tick();

        public void Open(Action confirmAction, Action declineAction)
        {
            _confirmAction = confirmAction;
            _declineAction = declineAction;
            SetActive(true);
        }

        private void SetActive(bool activate)
        {
            _input.SetGameplayInputActive(activate == false);
            _canvasGroup.alpha = activate ? 1f : 0f;
            _canvasGroup.interactable = activate;
            _canvasGroup.blocksRaycasts = activate;
        }

        private void OnYesClick()
        {
            SetActive(false);
            _confirmAction();
            SetEmptyActions();
        }

        private void OnNoClick()
        {
            SetActive(false);
            _declineAction();
            SetEmptyActions();
        }

        private void SetEmptyActions()
        {
            _confirmAction = () => { };
            _declineAction = () => { };
        }
    }
}