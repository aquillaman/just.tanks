using Projectiles;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Game/WeaponSettings", fileName = "WeaponSettings")]
    public class WeaponSettings : ScriptableObject
    {
        public ProjectileType Type;
        public int Damage = 1;
        public int ProjectilesCount = 8;
        public float ProjectilesVelocity = 20;
        public float Delay = 1;
    }
}