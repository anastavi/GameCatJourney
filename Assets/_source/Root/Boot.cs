using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameContracts;
using GameSceneManagement;
using GameInput;

namespace GameRoot
{
    public class Boot : MonoBehaviour, ICoroutinesHandler
    {
        private const Scenes StartScene = Scenes.MainMenu;

        [SerializeField] private GlobalServices _globalServices;
        [SerializeField] private LoadingScreen _loadingScreen;

        private static Scenes _sceneToStart;

        private TestInput _input;

        private void Awake()
        {
            _input = new();
            var saveData = new SaveData();
            saveData.Load();

            _globalServices.LoadingScreen = _loadingScreen;
            _globalServices.SceneLoader = new SceneLoader(_loadingScreen, this);
            _globalServices.Input = _input;
            _globalServices.InterfaceInput = _input;
            _globalServices.SaveService = saveData;

            LoadNeededScene();

            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy() => _input.Disable();

        public void ActivateCoroutine(ref Coroutine coroutineReference, IEnumerator coroutine)
        {
            if (coroutineReference != null)
                StopCoroutine(coroutineReference);

            coroutineReference = StartCoroutine(coroutine);
        }

        private void LoadNeededScene()
        {
            if (_sceneToStart == Scenes.None)
            {
                _globalServices.SceneLoader.Load(StartScene);
                return;
            }

            _globalServices.SceneLoader.Load(_sceneToStart);
        }

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ToBootScene()
        {
            _sceneToStart = Scenes.None;
            var currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == Scenes.Boot.ToString())
                return;

            foreach (Scenes scene in Enum.GetValues(typeof(Scenes)))
                if (scene.ToString() == currentSceneName)
                    _sceneToStart = scene;

            if (_sceneToStart == Scenes.None)
                return;

            SceneManager.LoadScene(Scenes.Boot.ToString());
        }
#endif
    }
}