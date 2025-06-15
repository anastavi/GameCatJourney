using UnityEngine;
using GameContracts;

namespace GameInteraction
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractionTrigger : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _interactAction;

        private IInput _input;
        private InteractionTriggerView _view;
        private bool _isActive;

        private IInteractAction InteractAction
        {
            get => (IInteractAction)_interactAction;
            set => InteractAction = value;
        }

        private Vector3 ViewPosition => new(transform.position.x, transform.position.y + 2f, transform.position.z);

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
            _isActive = false;
        }

        public void Init(InteractionTriggerView view, IInput input)
        {
            _input = input;
            _view = view;
        }

        public void Init(InteractionTriggerView view, IInteractAction interactAction, IInput input)
        {
            InteractAction = interactAction;
            _input = input;
            _view = view;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlayer _))
                SetActiveInteraction(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlayer _))
                SetActiveInteraction(false);
        }

        public void Tick()
        {
            if (_isActive == false)
                return;

            if (_input.InteractionWasPerformed)
                Interact();
        }

        private void SetActiveInteraction(bool isActive)
        {
            _isActive = isActive;

            if (isActive)
            {
                _view.Activate(ViewPosition);
                return;
            }

            _view.Deactivate();
            InteractAction.TryToStop();
        }

        private void Interact()
        {
            InteractAction.Interact(out bool wasCancelled);

            if (wasCancelled)
            {
                _view.Activate(ViewPosition);
                return;
            }

            _view.Deactivate();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_interactAction is IInteractAction || _interactAction == null)
                return;

            _interactAction = null;
            Debug.LogWarning($"{nameof(_interactAction)} doesn't implement {typeof(IInteractAction)}");
        }
#endif
    }
}