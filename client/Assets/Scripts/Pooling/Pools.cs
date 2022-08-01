using Projectiles;
using Units;
using UnityEngine.Pool;

namespace Pooling
{
    public class Pools : MonoSingleton<Pools>
    {
        private EnemyTankPool _enemyTank;
        private RoundShotPool _roundShot;
        private CanisterShotPool _canisterShot;

        public static IObjectPool<EnemyTank> EnemyTank => Instance._enemyTank.Pool;
        public static IObjectPool<RoundShot> RoundShot => Instance._roundShot.Pool;
        public static IObjectPool<CanisterShot> CanisterShot => Instance._canisterShot.Pool;

        protected override void Init()
        {
            base.Init();
            var root = transform;
            _enemyTank = new EnemyTankPool(root);
            _roundShot = new RoundShotPool(root);
            _canisterShot = new CanisterShotPool(root);
        }
    }
}