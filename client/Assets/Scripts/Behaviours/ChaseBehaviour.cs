using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public class ChaseBehaviour : MoveBehaviour
    {
        private Vector3 _lastPosition;
        private readonly NavMeshAgent _agent;
        private readonly Rigidbody _rigidbody;

        private ITarget Target => Owner.Target;

        internal ChaseBehaviour(Tank owner, NavMeshAgent agent, Rigidbody rigidbody) : base(owner, rigidbody)
        {
            _agent = agent;
            _rigidbody = rigidbody;
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            if (Target == null) return;

            UpdateWayPoint();

            var velocity = _agent.velocity;
            Move(new Vector3(velocity.x, 0, velocity.z).normalized);
            _agent.nextPosition = _rigidbody.position;
        }

        private void UpdateWayPoint()
        {
            var position = Target.Transform.position;

            if (_agent.pathEndPosition == position && _agent.remainingDistance > 1.6f) return;

            SelectNextPatrolPoint(position);
        }

        private void SelectNextPatrolPoint(Vector3 position)
        {
            if (!_lastPosition.Equals(position))
            {
                _agent.SetDestination(_lastPosition = position);
            }
        }
    }
}