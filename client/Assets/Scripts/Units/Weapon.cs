using System;
using Pooling;
using Projectiles;
using Settings;
using UnityEngine;

namespace Units
{
    public class Weapon
    {
        private float CoolDown;
        private Transform LaunchPoint;
        private WeaponSettings Settings;

        public Weapon(WeaponSettings settings, Transform launchPoint)
        {
            Settings = settings;
            LaunchPoint = launchPoint;
        }

        public void Shoot()
        {
            if ((CoolDown -= Time.deltaTime) < 0)
            {
                CoolDown = Settings.Delay;
                var direction = LaunchPoint.up;
                var count = Settings.ProjectilesCount;

                for (int i = 0; i < count; i++)
                {
                    var projectile = GetProjectile(Settings.Type);
                    projectile.Rigidbody.isKinematic = true;
                    projectile.transform.position = LaunchPoint.position;
                    projectile.Rigidbody.position = LaunchPoint.position;
                    var maxRadiansDelta = (Mathf.Deg2Rad * i) - (Mathf.Deg2Rad * (count - 1)) / 2f;
                    direction = Vector3.RotateTowards(direction, -direction, maxRadiansDelta, 0);
                    projectile.Rigidbody.isKinematic = false;
                    projectile.Launch(Settings.Damage, direction, Settings.ProjectilesVelocity);
                }
            }
        }

        private Projectile GetProjectile(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.RoundShot: return Pools.RoundShot.Take();
                case ProjectileType.CanisterShot: return Pools.CanisterShot.Take();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}