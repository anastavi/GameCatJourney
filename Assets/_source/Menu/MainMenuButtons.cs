using UnityEngine;
using GameContracts;

namespace GameMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private MenuButton _continue;
        [SerializeField] private MenuButton _newGame;
        [SerializeField] private MenuButton _settings;
        [SerializeField] private MenuButton _quit;

        private ISceneLoader _sceneLoader;
        private Scenes _sceneToContinue;
        private ButtonSelector _buttonSelector;

        public void Init(ISceneLoader sceneLoader, IInterfaceInput input, Scenes sceneToContinue)
        {
            _sceneLoader = sceneLoader;
            _sceneToContinue = sceneToContinue;

            var buttons = new MenuButton[] { _quit, _settings, _newGame, _continue };
            _buttonSelector = new(buttons, input);

            _continue.Init(OnContinueGameClick);
            _newGame.Init(OnStartNewGameClick);
            _settings.Init(OnSettingsClick);
            _quit.Init(OnQuitClick);

            _continue.gameObject.SetActive(_sceneToContinue != Scenes.None);
        }

        private void Update() => _buttonSelector.Tick();

        private void OnContinueGameClick()
        {
            _sceneLoader.Load(_sceneToContinue);
        }

        private void OnStartNewGameClick()
        {
            _sceneLoader.Load(Scenes.VillageScene);
        }

        private void OnSettingsClick()
        {
            AudioListener.pause = !AudioListener.pause;
        }

        private void OnQuitClick()
        {
            Debug.Log($"Quit button clicked");
        }
    }

    internal class ButtonSelector
    {
        [SerializeField] private MenuButton[] _buttons;

        private readonly IInterfaceInput _input;

        private int _currentIndex;

        private enum IndexOrder { Next, Previous, }

        public ButtonSelector(MenuButton[] buttons, IInterfaceInput input)
        {
            _buttons = buttons;
            _input = input;
            _currentIndex = -1;
        }

        internal void Tick()
        {
            if (_input.UpPerformed)
                SelectNextButton();

            if (_input.DownPerformed)
                SelectPreviousButton();

            if (_input.EnterPerformed)
                ClickButton();
        }

        private void ClickButton()
        {
            if (TryHandleFirstSelection())
                return;

            _buttons[_currentIndex].Click();
        }

        private void SelectNextButton()
        {
            if (TryHandleFirstSelection())
                return;

            _currentIndex = GetAvailableButtonIndex(IndexOrder.Next);
            Select();
        }

        private void SelectPreviousButton()
        {
            if (TryHandleFirstSelection())
                return;

            _currentIndex = GetAvailableButtonIndex(IndexOrder.Previous);
            Select();
        }

        private bool TryHandleFirstSelection()
        {
            if (_currentIndex >= 0)
                return false;

            _currentIndex = GetAvailableButtonIndex(IndexOrder.Previous);
            _buttons[_currentIndex].SetSelectView();
            return true;
        }

        private void Select()
        {
            for (int i = 0; i < _buttons.Length; i++)
                _buttons[i].SetDeselectView();

            _buttons[_currentIndex].SetSelectView();
        }

        private int GetAvailableButtonIndex(IndexOrder order)
        {
            var index = _currentIndex;

            if (index < 0 || index >= _buttons.Length)
                index = 0;

            for (int i = 0; i < _buttons.Length; i++)
            {
                if (order == IndexOrder.Next)
                {
                    index++;
                    index = index >= _buttons.Length ? 0 : index;
                }

                if (order == IndexOrder.Previous)
                {
                    index--;
                    index = index < 0 ? _buttons.Length - 1 : index;
                }

                if (_buttons[index].gameObject.activeSelf)
                    return index;
            }

            return -1;
        }

    }
}