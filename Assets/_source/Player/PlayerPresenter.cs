using UnityEngine;
using GameContracts;

namespace GamePlayer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerPresenter : MonoBehaviour, IPlayer
    {
        [SerializeField] private Items _startItem;
        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;
        [SerializeField] private InventoryView _inventoryView;

        private bool _facingLeft;
        private bool _isPushed;

        private IInput _input;
        private Vector2 _direction;
        private Vector2 _lastDirection;
        private Rigidbody2D _rigidBody;
        private Inventory _inventory;

        private bool IsCurrentlyStopped => _input.Direction == Vector2.zero && (_direction.x != 0 || _direction.y != 0);

        public void Init(IInput input)
        {
            _inventory = new(_inventoryView);
            _input = input;
            GetRigidbody();
            _facingLeft = true;
            _isPushed = false;
        }

        private void Update()
        {
            ProccessInput();
            Animate();
            Flip();
        }

        private void FixedUpdate()
        {
            if (_isPushed)
                return;

            Move();
        }

        public void AddItem(Items item) => _inventory.AddItem(item);
        public bool RemoveItem(Items item) => _inventory.RemoveItem(item);

        internal void ApplyPushForce(Vector2 direction)
        {
            _isPushed = true;
            _rigidBody.AddForce(direction);
        }

        internal void StopPushForce()
        {
            _isPushed = false;
        }

        private void Move() => _rigidBody.linearVelocity = _direction * _speed;

        private void ProccessInput()
        {
            if (IsCurrentlyStopped)
                _lastDirection = _direction;

            _direction = _input.Direction;
        }

        private void Animate()
        {
            _animator.SetFloat("MoveX", _direction.x);
            _animator.SetFloat("MoveY", _direction.y);
            _animator.SetFloat("MoveMagnitude", _direction.magnitude);
            _animator.SetFloat("LastMoveX", _lastDirection.x);
            _animator.SetFloat("LastMoveY", _lastDirection.y);
        }

        private void Flip()
        {
            if (_direction.x < 0 && !_facingLeft || _direction.x > 0 && _facingLeft)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                _facingLeft = !_facingLeft;
            }
        }

        private void GetRigidbody()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _rigidBody.gravityScale = 0f;
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}