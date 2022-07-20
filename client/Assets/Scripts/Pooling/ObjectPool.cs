using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolItem
    {
        private Transform PoolRoot;

        private Queue<T> Pool = new Queue<T>();

        public ObjectPool(int size, Transform parent)
        {
            PoolRoot = new GameObject(typeof(T).Name).transform;
            PoolRoot.SetParent(parent);
            PoolRoot.position = Vector3.left * 100;

            for (int i = 0; i < size; i++)
            {
                Put(ObjectFactory.Create<T>());
            }
        }

        public T Take()
        {
            if (Pool.TryDequeue(out var result))
            {
                OnTake(result);
                return result;
            }

            return ObjectFactory.Create<T>();
        }

        public void Put(T item)
        {
            Pool.Enqueue(item);

            item.transform.position = PoolRoot.position;
            item.transform.SetParent(PoolRoot);
            item.enabled = false;
        }

        private void OnTake(T item)
        {
            item.Setup();
            item.transform.SetParent(null);
            item.enabled = true;
        }
    }
}