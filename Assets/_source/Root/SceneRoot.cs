using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using GameMenu;
using GameContracts;
using GameInteraction;
using GameSceneManagement;
using GameDialogues;
using GameHideAndSeek;

namespace GameRoot
{
    public class SceneRoot : CompositeRoot
    {
        [SerializeField] private CatFisher _catFisher;
        [SerializeField] private HideAndSeekStart _hideAndSeek;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private MonoBehaviour _player;
        [SerializeField] private SceneTravelingInfo _travelingInfo;
        [Header("ExitTriggers")]
        [SerializeField] private Transform _triggersContainer;
        [SerializeField] private SceneExitTrigger[] _exitTriggers;
        [Header("Interaction")]
        [SerializeField] private InteractionTrigger[] _interactionTriggers;
        [SerializeField] private InteractionTriggerView _interactionView;
        [Header("Camera")]
        [SerializeField] private CinemachineCamera _followCinemachineCamera;
        [Header("Dialogues")]
        [SerializeField] private Dialogue[] _dialogues;
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private ConfirmMenu _confirmMenu;

        private int _triggersAmount;

        private IInput _input;

        private IPlayer Player => (IPlayer)_player;

        internal override void Init()
        {
            _input = GlobalServices.Input;

            _pauseMenu.Init(GlobalServices.SceneLoader, GlobalServices.InterfaceInput);
            InitPlayer();
            InitExitTriggers();
            InitInteraction();
            InitDialogues();
            SaveCurrentScene();

            if (_hideAndSeek != null)
                _hideAndSeek.SetFadeScreen(GlobalServices.LoadingScreen);

            if (_catFisher != null)
                _catFisher.Init(GlobalServices.SceneLoader);
        }

        private void Update()
        {
            HandleInput();

            for (int i = 0; i < _triggersAmount; i++)
                _interactionTriggers[i].Tick();
        }

        private void HandleInput()
        {
            if (_input.PauseWasPerformed)
                _pauseMenu.SwitchVisibility();
        }

        private void InitPlayer()
        {
            Player.Init(GlobalServices.Input);
            _player.transform.position = GetEntryPointPosition();
            _followCinemachineCamera.Follow = _player.transform;
        }

        private void InitExitTriggers()
        {
            if (_exitTriggers == null)
                return;

            for (int i = 0; i < _exitTriggers.Length; i++)
                _exitTriggers[i].Init(GlobalServices.SceneLoader, _travelingInfo);
        }

        private void InitInteraction()
        {
            _triggersAmount = _interactionTriggers.Length;

            for (int i = 0; i < _triggersAmount; i++)
                _interactionTriggers[i].Init(_interactionView, _input);
        }

        private void InitDialogues()
        {
            _confirmMenu.Init(GlobalServices.InterfaceInput);

            for (int i = 0; i < _dialogues.Length; i++)
                _dialogues[i].Init(_dialogueView, _confirmMenu, Player);
        }

        private void SaveCurrentScene()
        {
            GlobalServices.SaveService.SceneToLoad = GetScene(GlobalServices.SceneLoader.CurrentSceneName);
            GlobalServices.SaveService.Save();
        }

        private Vector3 GetEntryPointPosition()
        {
            for (int i = 0; i < _exitTriggers.Length; i++)
                if (_exitTriggers[i].TravelFromScene == _travelingInfo.TravelFromScene)
                    return _exitTriggers[i].EntryPosition;

            return new(20f, 20f, 0f);
        }

        private Scenes GetScene(string name)
        {
            foreach (Scenes scene in Enum.GetValues(typeof(Scenes)))
                if (scene.ToString() == name)
                    return scene;

            return Scenes.None;
        }

        [ContextMenu(nameof(GetAllTriggers))]
        private void GetAllTriggers()
        {
            List<SceneExitTrigger> triggers = new();

            for (int i = 0; i < _triggersContainer.childCount; i++)
                if(_triggersContainer.GetChild(i).TryGetComponent(out SceneExitTrigger trigger))
                    triggers.Add(trigger);

            _exitTriggers = triggers.ToArray();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_player is IPlayer || _player == null)
                return;

            _player = null;
            Debug.LogWarning($"{nameof(_player)} doesn't implement {typeof(IPlayer)}");
        }
#endif
    }
}