using System;
using System.Collections.Generic;
using _VampireSurvivors.CodeBase.Dto.Input;
using _VampireSurvivors.CodeBase.Services.Input.Providers;
using _VampireSurvivors.CodeBase.Services.Network;
using Fusion;
using R3;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Services.Input
{
    public class PlayerInputService : IInitializable, ITickable, IDisposable
    {
        private readonly FusionCallbacks _fusionCallbacks;
        private readonly CompositeDisposable _disposables = new();

        private NetworkPlayerInput _currentInput;

        private readonly IReadOnlyList<IInputProvider> _inputProviders;

        public PlayerInputService(FusionCallbacks fusionCallbacks, List<IInputProvider> inputProviders)
        {
            _fusionCallbacks = fusionCallbacks;
            _inputProviders = inputProviders;
        }

        public void Initialize()
        {
            _fusionCallbacks.InputReceived.Subscribe(OnInput).AddTo(_disposables);
        }

        public void Tick()
        {
            foreach (var provider in _inputProviders)
            {
                _currentInput.MoveDirection += provider.GetMove();
            }
        }

        private void OnInput(NetworkInput input)
        {
            input.Set(_currentInput);

            _currentInput.MoveDirection = Vector2.zero;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
