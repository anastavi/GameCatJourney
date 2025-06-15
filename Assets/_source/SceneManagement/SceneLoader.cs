using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameContracts;

namespace GameSceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutinesHandler _coroutinesHandler;
        private readonly ILoadingScreen _loadingScreen;

        private AsyncOperation _loadingOperation;
        private Coroutine _loadingCoroutine;

        public SceneLoader(ILoadingScreen loadingScreen, ICoroutinesHandler coroutinesHandler)
        {
            _loadingScreen = loadingScreen;
            _coroutinesHandler = coroutinesHandler;
        }

        public string CurrentSceneName => SceneManager.GetActiveScene().name;

        public void Load(Scenes scene, bool allowReloadScene = false)
        {
            var sceneName = scene.ToString();
            bool dontReloadScene = allowReloadScene == false && sceneName == CurrentSceneName;

            if (CurrentSceneName != Scenes.Boot.ToString() && dontReloadScene)
                return;

            _coroutinesHandler.ActivateCoroutine(ref _loadingCoroutine, Loading(sceneName));
        }

        private IEnumerator Loading(string sceneName)
        {
            _loadingScreen.Show();

            while (_loadingScreen.InProgress)
                yield return null;

            _loadingOperation = SceneManager.LoadSceneAsync(sceneName);

            while (_loadingOperation.isDone == false)
                yield return null;

            _loadingScreen.Hide();
        }
    }
}