using System.Collections;
using UnityEngine;

namespace GamePlayer
{
    public class Wind : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Vector3 _direction;
        [SerializeField] private int _force;
        [SerializeField] private float _delay;
        [Header("Gizmos")]
        [SerializeField] private float _radius;
        [SerializeField] private float _length;

        private void Awake()
        {
            StartCoroutine(SwitchEnable());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerPresenter player))
                player.ApplyPushForce(_direction * _force);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerPresenter player))
                player.StopPushForce();
        }

        private IEnumerator SwitchEnable()
        {
            WaitForSeconds wait = new(_delay);

            while (true)
            {
                yield return wait;
                _collider.enabled = !_collider.enabled;
                _renderer.enabled = !_renderer.enabled;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, _radius);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + _direction * _length);
        }
    }
}