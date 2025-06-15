using UnityEngine;
using DG.Tweening;
using GameContracts;

namespace GameFishing
{
    public class KittenHand : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _distance;
        [SerializeField] private float _duration;

        private IInput _input;
        private Sequence _sequence;
        private bool _isMoving;
        private float _originY;
        private float _animationY;

        private void Awake()
        {
            _isMoving = false;
            _originY = transform.position.y;
            _animationY = _originY - _distance;
            _collider.enabled = false;
        }

        public void Init(IInput input)
        {
            _input = input;
        }

        internal void Tick()
        {
            if (_isMoving)
                return;

            if (_input.InteractionWasPerformed)
                Lunge();
        }

        internal void KillActiveSequence()
        {
            if(_sequence != null && _sequence.active)
                _sequence.Kill(true);
        }

        internal void MoveBack()
        {
            KillActiveSequence(false);
            _isMoving = true;
            _collider.enabled = false;
            transform.DOMoveY(_originY, _duration).OnComplete(() => _isMoving = false);
        }

        private void Lunge()
        {
            KillActiveSequence();
            _sequence = DOTween.Sequence();
            _isMoving = true;
            _collider.enabled = true;
            _sequence.Append(transform.DOMoveY(_animationY, _duration))
                     .AppendCallback(() =>_collider.enabled = false)
                     .Append(transform.DOMoveY(_originY, _duration)).OnComplete(() => _isMoving = false);
        }

        private void KillActiveSequence(bool complete)
        {
            if (_sequence != null && _sequence.active)
                _sequence.Kill(complete);
        }
    }
}