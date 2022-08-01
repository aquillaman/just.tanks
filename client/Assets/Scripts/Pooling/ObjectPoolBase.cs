using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Pooling
{
    public class ObjectPoolBase<T> where T : class, IPoolable
    {
        protected virtual int MaxPoolSize { get; }
        protected virtual int DefaultCapacity { get; }
        protected virtual bool CollectionChecks { get; }
        public IObjectPool<T> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, CollectionChecks, DefaultCapacity, MaxPoolSize);
                }

                return _pool;
            }
        }
        
        private IObjectPool<T> _pool;

        protected readonly Transform Root;

        public ObjectPoolBase(Transform parent)
        {
            Root = new GameObject(GetType().Name).transform;
            Root.SetParent(parent);
        }
        
        protected virtual void OnDestroyPoolObject(T obj){}
        protected virtual void OnReturnedToPool(T obj){}
        protected virtual void OnTakeFromPool(T obj){}
        protected virtual T CreatePooledItem() { throw new NotImplementedException(); }
    }
}