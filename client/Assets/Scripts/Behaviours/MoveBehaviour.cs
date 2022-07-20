using Units;
using UnityEngine;

namespace Behaviours
{
    public abstract class MoveBehaviour : Behaviour
    {
        private Vector2 _input;
        private readonly Rigidbody _rigidbody;

        protected MoveBehaviour(Tank owner, Rigidbody rigidbody) : base(owner)
        {
            _rigidbody = rigidbody;
        }

        protected void Move(Vector3 position)
        {
            var rbPosition = _rigidbody.position;
            var direction = new Vector3(rbPosition.x, 0, rbPosition.z).DirectionTo(position);
            var rotation = Quaternion.LookRotation(direction);
            _rigidbody.position = position;
            _rigidbody.rotation = rotation;
            _rigidbody.transform.rotation = rotation;
        }
    }
}