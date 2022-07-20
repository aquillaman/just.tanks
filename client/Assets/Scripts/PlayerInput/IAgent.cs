using UnityEngine;

namespace PlayerInput
{
    public interface IAgent
    {
        void ChangeWeapon();
        void Move(Vector2 value);
    }
}