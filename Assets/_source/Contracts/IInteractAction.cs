using UnityEngine;

namespace GameContracts
{
    public interface IInteractAction
    {
        public void Interact(out bool wasCancelled);
        public void TryToStop();
    }
}