using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public class ChaseBehaviour : MoveBehaviour
    {
        private int _index = 0;
        private Vector3 _corner;
        private Vector3 _destination;
        private Vector3 _lastPosition;
        private readonly NavMeshAgent _agent;
        private readonly Queue<Vector3> _corners = new Queue<Vector3>();

        private ITarget Target => Owner.Target;

        internal ChaseBehaviour(Tank owner, NavMeshAgent agent, Rigidbody rigidbody) : base(owner, rigidbody)
        {
            _agent = agent;
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            if (Target == null) return;

            UpdateWayPoint();

            if (_agent.remainingDistance > 1.4f)
            {
                Move(new Vector3(_agent.nextPosition.x, 0, _agent.nextPosition.z));
            }
            else
            {
                _agent.nextPosition = Owner.transform.position;
            }
        }

        private void UpdateWayPoint()
        {
            if (_agent.remainingDistance > 1.6f) return;

            SelectNextPatrolPoint();
        }

        private void SelectNextPatrolPoint()
        {
            if (!_lastPosition.Equals(Target.Transform.position))
            {
                _lastPosition = Target.Transform.position;
                _agent.SetDestination(_lastPosition);
            }
        }
    
    }
}