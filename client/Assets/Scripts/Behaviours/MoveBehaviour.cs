using Units;
using UnityEngine;

namespace Behaviours
{
    public abstract class MoveBehaviour : Behaviour
    {
        private readonly Rigidbody _rigidbody;
        protected readonly Tank Owner;

        protected MoveBehaviour(Tank owner, Rigidbody rigidbody)
        {
            Owner = owner;
            _rigidbody = rigidbody;
        }

        protected void Move(Vector3 input)
        {
            if (input == Vector3.zero) return;
            
            var force = (Vector3.forward * input.z) + (Vector3.right * input.x);
            _rigidbody.AddForce(force * 5 - _rigidbody.velocity, ForceMode.VelocityChange);
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, Quaternion.LookRotation(force), Time.fixedDeltaTime * 10);
        }
    }
}