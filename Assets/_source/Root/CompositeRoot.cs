using UnityEngine;

namespace GameRoot
{
    public abstract class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private GlobalServices _globalServices;

        protected GlobalServices GlobalServices => _globalServices;

        private void Start() => Init();

        internal abstract void Init();
    }
}