using SimpleInputNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Services.Input.Providers
{
    public class JoystickInputProvider : MonoBehaviour, IInputProvider
    {
        [SerializeField, Required] private Joystick _joystick;

        public Vector2 GetMove()
        {
            return _joystick.Value;
        }
    }
}
