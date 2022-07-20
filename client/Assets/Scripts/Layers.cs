using UnityEngine;

public static class Layers
{
    public static readonly LayerMask Enemy = LayerMask.GetMask("Enemy");
    public static readonly LayerMask Player = LayerMask.GetMask("Player");
}