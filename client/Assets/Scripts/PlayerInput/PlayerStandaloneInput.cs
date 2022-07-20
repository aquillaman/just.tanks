using System;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerStandaloneInput: MonoBehaviour, IAgentInput
    {
        public event Action OnWeaponChange;
        public event Action<Vector2> OnMovement;

        private void Update()
        {
            ReadDirection();
            ReadToggle();
        }

        private void ReadToggle()
        {
            if (Input.GetKeyDown(KeyCode.Space)) OnToggle();
            if (Input.GetKeyDown(KeyCode.Q)) OnToggle();
            if (Input.GetKeyDown(KeyCode.E)) OnToggle();
        }

        private void ReadDirection()
        {
            var direction = Vector2.zero;
            
            if (Input.GetKey(KeyCode.W)) direction += Vector2.up; 
            if (Input.GetKey(KeyCode.S)) direction += Vector2.down; 
            
            if (Input.GetKey(KeyCode.A)) direction += Vector2.left; 
            if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

            OnMove(direction);
        }

        private void OnToggle()
        {
            OnWeaponChange?.Invoke();
        }

        private void OnMove(Vector2 v)
        {
            OnMovement?.Invoke(v);
        }
    }
}