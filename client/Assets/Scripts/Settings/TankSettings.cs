using Units;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Game/TankSettings", fileName = "TankSettings")]
    public class TankSettings : ScriptableObject
    {
        public Tank Prefab;
        public WeaponSettings[] Weapons;
        public int Helth = 17;
        public int AimRadius = 1;
        public int FireRadius = 1;
        public int CollisionDamage = 1;
    
        [Header("Movement")]
        public float Velocity = 0.5f;
        public float AngularVelocity = 0.5f;
    }
}