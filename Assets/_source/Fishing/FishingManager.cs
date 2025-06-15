using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameContracts;

namespace GameFishing
{
    public class FishingManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private int _fishRequirement;
        [SerializeField] private int _poolCount;
        [SerializeField] private KittenHand _hand;
        [SerializeField] private Fish _fish;
        [SerializeField] private Transform[] _fishPoints;

        private ISceneLoader _sceneLoader;
        private Coroutine _coroutine;
        private List<Fish> _fishPool;
        private int _catchedFish;
        private bool _isFinished;

        private void Awake()
        {
            _isFinished = false;
            _catchedFish = 0;
            UpdateView();
            CreatePool();
            _coroutine = StartCoroutine(ActivateFish());
        }

        public void Init(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Update()
        {
            if (_isFinished)
                return;

            _hand.Tick();
        }

        internal void AddFish()
        {
            _catchedFish++;
            UpdateView();

            if (_catchedFish >= _fishRequirement)
                EndFishing();
        }

        private void EndFishing()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _hand.KillActiveSequence();
            DeactiveAllFish();
            _isFinished = true;
            _sceneLoader.Load(Scenes.MeadowScene);
        }

        private void UpdateView()
        {
            _countText.text = string.Format("{0}/{1}", _catchedFish, _fishRequirement);
        }

        private void CreatePool()
        {
            _fishPool = new();

            for (int i = 0; i < _poolCount; i++)
            {
                Fish fish = Instantiate(_fish);
                fish.Init(this);
                fish.gameObject.SetActive(false);
                _fishPool.Add(fish);
            }
        }

        private void TryActivateFish()
        {
            int randomIndex = Random.Range(0, _fishPoints.Length);

            for (int i = 0; i < _fishPool.Count; i++)
            {
                if (_fishPool[i].gameObject.activeSelf == false)
                {
                    _fishPool[i].gameObject.SetActive(true);
                    _fishPool[i].Activate(_fishPoints[randomIndex]);
                    return;
                }
            }
        }

        private IEnumerator ActivateFish()
        {
            WaitForSeconds delay = new(1.1f);

            while (gameObject.activeSelf)
            {
                yield return delay;
                TryActivateFish();
            }
        }

        private void DeactiveAllFish()
        {
            for (int i = 0; i < _fishPool.Count; i++)
                _fishPool[i].DeactivateTweens();
        }
    }
}