using Projectiles;
using UnityEngine;

namespace Pooling
{
    public class RoundShotPool : ObjectPoolBase<RoundShot>
    {
        protected override int MaxPoolSize => 10;
        protected override int DefaultCapacity => 10;
        protected override bool CollectionChecks => true;

        public RoundShotPool(Transform parent) : base(parent) { }
        
        protected override RoundShot CreatePooledItem()
        {
            return ObjectFactory.Create<RoundShot>();
        }

        private void OnReturnToPool(IPoolable poolable)
        {
            poolable.ReturnToPool -= OnReturnToPool; 
            Pool.Release((RoundShot)poolable);
        }

        protected override void OnReturnedToPool(RoundShot tank)
        {
            tank.ReturnToPool -= OnReturnToPool;
            tank.transform.SetParent(Root);
            tank.gameObject.SetActive(false);
        }

        protected override void OnTakeFromPool(RoundShot tank)
        {
            tank.ReturnToPool += OnReturnToPool;
            tank.gameObject.SetActive(true);
        }

        protected override void OnDestroyPoolObject(RoundShot tank)
        {
            Object.Destroy(tank.gameObject);
        }
    }
}