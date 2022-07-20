using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 DirectionTo(this Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }
}