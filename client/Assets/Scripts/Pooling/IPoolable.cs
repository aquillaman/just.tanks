using System;
using UnityEngine;

namespace Pooling
{
    public interface IPoolable
    {
        event Action<IPoolable> ReturnToPool;
        GameObject gameObject { get; }
        void Setup();
        void Reset();
    }
}