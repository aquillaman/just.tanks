using Projectiles;
using UnityEngine;

namespace Pooling
{
    public class CanisterShotPool : ObjectPoolBase<CanisterShot>
    {
        protected override int MaxPoolSize => 10;
        protected override int DefaultCapacity => 10;
        protected override bool CollectionChecks => true;

        public CanisterShotPool(Transform parent) : base(parent) { }
        
        protected override CanisterShot CreatePooledItem()
        {
            return ObjectFactory.Create<CanisterShot>();
        }

        private void OnReturnToPool(IPoolable poolable)
        {
            poolable.ReturnToPool -= OnReturnToPool; 
            Pool.Release((CanisterShot)poolable);
        }

        protected override void OnReturnedToPool(CanisterShot tank)
        {
            tank.ReturnToPool -= OnReturnToPool;
            tank.gameObject.SetActive(false);
        }

        protected override void OnTakeFromPool(CanisterShot tank)
        {
            tank.ReturnToPool += OnReturnToPool;
            tank.gameObject.SetActive(true);
        }

        protected override void OnDestroyPoolObject(CanisterShot tank)
        {
            Object.Destroy(tank.gameObject);
        }
    }
}