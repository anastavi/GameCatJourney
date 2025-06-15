using UnityEngine;
using DG.Tweening;

namespace GameFishing
{
    public class Fish : MonoBehaviour
    {
        private FishingManager _manager;

        private Sequence _sequence;
        private Tween _move;

        internal void Init(FishingManager manager)
        {
            _manager = manager;
        }

        internal void Activate(Transform point)
        {
            transform.localScale = new(-.4f, .4f, .4f);
            transform.position = point.position;
            KillActiveTween();
            _move = transform.DOMoveX(transform.position.x + 25, 2f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
        }

        internal void DeactivateTweens()
        {
            _sequence?.Kill();
            _move?.Kill();
        }

        private void KillActiveSequence()
        {
            if (_sequence != null && _sequence.active)
                _sequence.Kill(true);
        }

        private void KillActiveTween()
        {
            if (_move != null && _move.active)
                _move.Kill(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out KittenHand hand))
            {
                hand.MoveBack();
                KillActiveSequence();
                _sequence = DOTween.Sequence();
                _sequence.Append(transform.DOScale(Vector3.zero, 0.7f))
                         .Join(transform.DOMove(_manager.transform.position, 0.7f).OnComplete(() => Catch()));
            }
        }

        private void Catch()
        {
            _manager.AddFish();
            gameObject.SetActive(false);
        }
    }
}