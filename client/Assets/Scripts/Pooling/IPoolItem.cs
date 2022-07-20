using UnityEngine;

namespace Pooling
{
    public interface IPoolItem
    {
        GameObject gameObject { get; }
        void Setup();
        void Reset();
    }
}