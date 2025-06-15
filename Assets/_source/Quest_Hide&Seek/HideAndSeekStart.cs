using System;
using UnityEngine;
using GameContracts;

namespace GameHideAndSeek
{
    public class HideAndSeekStart : MonoBehaviour, IDialogueAction
    {
        [SerializeField] private Kitten[] _kittens;
        [SerializeField] private Transform[] _hidePoints;
        [SerializeField] private GameObject _trigger;

        private ILoadingScreen _loadingScreen;

        private void Awake()
        {
            if (_kittens.Length != _hidePoints.Length)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < _kittens.Length; i++)
                _kittens[i].DoDialogueAction();
        }

        public void SetFadeScreen(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }

        public void DoDialogueAction()
        {
            _loadingScreen.BlinkAction();
            HideKittens();
            _trigger.SetActive(false);
        }

        private void HideKittens()
        {
            for (int i = 0; i < _kittens.Length; i++)
                _kittens[i].MoveToHidePosition(_hidePoints[i].position);
        }
    }
}