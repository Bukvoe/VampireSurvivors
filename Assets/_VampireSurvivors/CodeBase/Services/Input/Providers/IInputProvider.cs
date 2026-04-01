using UnityEngine;

namespace _VampireSurvivors.CodeBase.Services.Input.Providers
{
    public interface IInputProvider
    {
        public Vector2 GetMove();
    }
}
