using System;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerMobileInput: MonoBehaviour, IAgentInput
    {
        [SerializeField]
        private MobileToggle _toggle;
        [SerializeField]
        private MobileJoystick _joystick;

        public event Action OnWeaponChange;
        public event Action<Vector2> OnMovement;

        private void Awake()
        {
            _toggle.OnToggle += OnToggle;
            _joystick.OnMove += OnMove;
        }

        private void OnToggle(bool obj)
        {
            OnWeaponChange?.Invoke();
        }

        private void OnMove(Vector2 v)
        {
            OnMovement?.Invoke(v);
        }
    }
}