using Projectiles;
using Units;

namespace Pooling
{
    public class Pools : MonoSingleton<Pools>
    {
        private ObjectPool<EnemyTank> _enemyTank;
        private ObjectPool<RoundShot> _roundShot;
        private ObjectPool<CanisterShot> _canisterShot;


        public static ObjectPool<EnemyTank> EnemyTank => Instance._enemyTank;
        public static ObjectPool<RoundShot> RoundShot => Instance._roundShot;
        public static ObjectPool<CanisterShot> CanisterShot => Instance._canisterShot;

        protected override void Init()
        {
            base.Init();

            var parent = transform;
            _enemyTank = new ObjectPool<EnemyTank>(1, parent);
            _roundShot = new ObjectPool<RoundShot>(10, parent);
            _canisterShot = new ObjectPool<CanisterShot>(10, parent);
        }
    }
}