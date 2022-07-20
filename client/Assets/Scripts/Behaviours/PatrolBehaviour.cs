﻿using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public class PatrolBehaviour : MoveBehaviour
    {
        private int _index = 0;
        private readonly Vector3[] _path;
        private Vector3 _destination;
        private NavMeshAgent _agent;

        internal PatrolBehaviour(Tank owner, Vector3[] path, NavMeshAgent agent, Rigidbody rigidbody) : base(owner,
            rigidbody)
        {
            _path = path;
            _agent = agent;
            _rigidbody = rigidbody;

            SelectNextPatrolPoint();
        }

        public override void Update()
        {
        }


        private Vector3 _corner;
        private Queue<Vector3> _corners = new Queue<Vector3>();
        private Rigidbody _rigidbody;

        public override void FixedUpdate()
        {
            if (Owner.Target != null) return;

            UpdateWayPoint();

            Move(new Vector3(_agent.nextPosition.x, 0, _agent.nextPosition.z));
        }

        private void UpdateWayPoint()
        {
            if (_agent.remainingDistance > 1.6f) return;

            SelectNextPatrolPoint();
        }

        private void SelectNextPatrolPoint()
        {
            if (_path.Length > 0)
            {
                _destination = _path[_index++ % _path.Length];
                _agent.SetDestination(_destination);
            }
        }
    }
}