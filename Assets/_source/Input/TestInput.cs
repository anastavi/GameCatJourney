using UnityEngine;
using GameContracts;

namespace GameInput
{
    public class TestInput : IInput, IInterfaceInput
    {
        private readonly GameInputSystem _input;

        public Vector2 Direction => _input.Gameplay.Movement.ReadValue<Vector2>();
        public bool InteractionWasPerformed => _input.Gameplay.Interaction.WasPerformedThisFrame();
        public bool PauseWasPerformed => _input.Gameplay.Pause.WasPerformedThisFrame();
        public bool UpPerformed => _input.Interface.Up.WasPerformedThisFrame();
        public bool DownPerformed => _input.Interface.Down.WasPerformedThisFrame();
        public bool EnterPerformed => _input.Interface.Enter.WasPerformedThisFrame();

        public TestInput()
        {
            _input = new();
            _input.Enable();
        }

        public void Disable()
        {
            _input.Disable();
        }

        public void SetGameplayInputActive(bool isActive)
        {
            if (isActive)
            {
                _input.Gameplay.Enable();
                return;
            }

            _input.Gameplay.Disable();
        }
    }
}