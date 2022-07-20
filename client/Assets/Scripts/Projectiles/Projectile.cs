using Pooling;
using Units;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour, IPoolItem
    {
        public Rigidbody Rigidbody => _rigidbody;
        private Rigidbody _rigidbody;
        private int _damage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(int damage, Vector3 dir, float velocity)
        {
            _damage = damage;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(dir * velocity, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.GetComponentInParent<ITarget>()?.TakeDamage(_damage);
            Reset();
        }

        public void Setup()
        {
        
        }

        public virtual void Reset()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}