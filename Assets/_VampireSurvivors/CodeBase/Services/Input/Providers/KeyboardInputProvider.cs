using System;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Services.Input.Providers
{
    public class KeyboardInputProvider : IInputProvider, IInitializable, IDisposable
    {
        private readonly InputActions _inputActions;

        public KeyboardInputProvider(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public void Initialize()
        {
            _inputActions.Enable();
        }

        public Vector2 GetMove()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            _inputActions.Disable();
        }
    }
}
