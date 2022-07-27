using UnityEngine;

namespace Units
{
    public interface ITargetProviderPrefs
    {
        float Radius { get; }
        Vector3 Position { get; }
    }
}