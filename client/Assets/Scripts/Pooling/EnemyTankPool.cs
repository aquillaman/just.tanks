using Units;
using UnityEngine;

namespace Pooling
{
    public class EnemyTankPool : ObjectPoolBase<EnemyTank>
    {
        protected override int MaxPoolSize => 10;
        protected override int DefaultCapacity => 10;
        protected override bool CollectionChecks => true;

        public EnemyTankPool(Transform parent) : base(parent) { }

        protected override EnemyTank CreatePooledItem()
        {
            var item = ObjectFactory.Create<EnemyTank>();
            item.transform.SetParent(Root);
            return item;
        }

        private void OnReturnToPool(IPoolable poolable)
        {
            Pool.Release((EnemyTank)poolable);
        }

        protected override void OnReturnedToPool(EnemyTank tank)
        {
            tank.ReturnToPool -= OnReturnToPool;
            ((IPoolable)tank).Reset();
            tank.gameObject.SetActive(false);
        }

        protected override void OnTakeFromPool(EnemyTank tank)
        {
            tank.ReturnToPool += OnReturnToPool;
            ((IPoolable)tank).Setup();
            tank.gameObject.SetActive(true);
        }

        protected override void OnDestroyPoolObject(EnemyTank tank)
        {
            Object.Destroy(tank.gameObject);
        }
    }
}