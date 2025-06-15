using UnityEngine;
using GameContracts;
using System;

namespace GameSceneManagement
{
    public class SceneExitTrigger : MonoBehaviour
    {
        [SerializeField] private Scenes _sceneToLoad;
        [SerializeField] private Transform _entryPoint;
        [SerializeField] private bool _allowReload = true;

        private Scenes _currentScene;
        private ISceneLoader _sceneLoader;
        private SceneTravelingInfo _travelingInfo;

        public Scenes TravelFromScene => _sceneToLoad;
        public Vector3 EntryPosition => _entryPoint.position;

        public void Init(ISceneLoader sceneLoader, SceneTravelingInfo travelingInfo)
        {
            _sceneLoader = sceneLoader;
            _travelingInfo = travelingInfo;
            SetCurrentScene();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPlayer _))
            {
                _travelingInfo.TravelFromScene = _currentScene;
                _sceneLoader.Load(_sceneToLoad, _allowReload);
            }
        }

        private void SetCurrentScene()
        {
            var currentSceneName = gameObject.scene.name;

            foreach (Scenes scene in Enum.GetValues(typeof(Scenes)))
                if (scene.ToString() == currentSceneName)
                    _currentScene = scene;
        }
    }
}