using UnityEngine;

namespace GameContracts
{
    public interface IInput
    {
        public Vector2 Direction { get; }
        public bool InteractionWasPerformed { get; }
        public bool PauseWasPerformed { get; }
    }

    public interface IInterfaceInput
    {
        public bool UpPerformed { get; }
        public bool DownPerformed { get; }
        public bool EnterPerformed { get; }

        public void SetGameplayInputActive(bool isActive);
    }
}