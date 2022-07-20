using System;
using UnityEngine;

namespace PlayerInput
{
    public interface IAgentInput
    {
        event Action OnWeaponChange;
        event Action<Vector2> OnMovement;
    }
}