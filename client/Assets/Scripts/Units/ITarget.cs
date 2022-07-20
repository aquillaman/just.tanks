using System;
using UnityEngine;

namespace Units
{
    public interface ITarget
    {
        void TakeDamage(int amount);
        Transform Transform { get; }
        public event Action<Tank> Destroyed;
    }
}