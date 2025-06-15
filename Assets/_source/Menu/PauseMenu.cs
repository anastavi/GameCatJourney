using UnityEngine;
using GameContracts;

namespace GameMenu
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private MenuButton _continue;
        [SerializeField] private MenuButton _exit;

        private ISceneLoader _sceneLoader;
        private CanvasGroup _canvasGroup;
        private ButtonSelector _selector;

        private bool IsActive => _canvasGroup.alpha > 0;

        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

        public void Init(ISceneLoader sceneLoader, IInterfaceInput input)
        {
            _sceneLoader = sceneLoader;

            var buttons = new MenuButton[] { _exit, _continue };
            _selector = new(buttons, input);

            SetActive(false);

            _continue.Init(OnContinueGameClick);
            _exit.Init(OnExitGameClick);
        }

        private void Update() => _selector.Tick();

        public void SwitchVisibility() => SetActive(IsActive == false);

        private void SetActive(bool activate)
        {
            Time.timeScale = activate ? 0f : 1f;
            _canvasGroup.alpha = activate ? 1f : 0f;
            _canvasGroup.interactable = activate;
            _canvasGroup.blocksRaycasts = activate;
        }

        private void OnExitGameClick()
        {
            _canvasGroup.interactable = false;
            _sceneLoader.Load(Scenes.MainMenu);
        }

        private void OnContinueGameClick() => SetActive(false);
    }
}