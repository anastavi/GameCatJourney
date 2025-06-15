using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameContracts;

namespace GameSceneManagement
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup _screen;
        [Range(0.1f, 1f)]
        [SerializeField] private float _appearanceTime;

        private Coroutine _coroutine;

        private enum AppearanceType { Show, Hide };

        public bool InProgress => _screen.alpha > 0f && _screen.alpha < 1f;

        internal void Construct()
        {
            _screen.alpha = 1f;
            _screen.gameObject.SetActive(true);
        }

        public void Show()
        {
            transform.SetAsLastSibling();
            ActivateCoroutine(Appearance(AppearanceType.Show));
        }

        public void Hide()
        {
            transform.SetAsLastSibling();
            ActivateCoroutine(Appearance(AppearanceType.Hide));
        }

        public void BlinkAction() => StartCoroutine(Blink());

        private IEnumerator Blink()
        {
            yield return Appearance(AppearanceType.Show);
            yield return Appearance(AppearanceType.Hide);
        }

        private IEnumerator Appearance(AppearanceType type)
        {
            _screen.gameObject.SetActive(true);
            float speed;
            float elapsedTime = 0f;
            float appearanceSign = type == AppearanceType.Show ? 1f : -1f;

            while (elapsedTime < _appearanceTime)
            {
                speed = appearanceSign * (Time.deltaTime / _appearanceTime);
                _screen.alpha += speed;
                elapsedTime += Time.deltaTime;

                if (type == AppearanceType.Show && _screen.alpha >= 1f)
                    ForceStop(1f);

                if (type == AppearanceType.Hide && _screen.alpha <= 0f)
                    ForceStop(0f);

                yield return null;
            }

            _screen.gameObject.SetActive(type == AppearanceType.Show);

            void ForceStop(float alpha)
            {
                elapsedTime = _appearanceTime;
                _screen.alpha = alpha;
            }
        }

        private void ActivateCoroutine(IEnumerator coroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(coroutine);
        }
    }
}