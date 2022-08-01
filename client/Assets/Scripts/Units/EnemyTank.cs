using Behaviours;
using Pooling;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class EnemyTank : Tank, ITargetProviderPrefs
    {
        private Vector3[] _path;
        private Vector3 _destination;
        private Transform _transform;
        private NavMeshAgent _agent;

        public float Radius => AimRadius;
        public Vector3 Position => Transform.position;

        private void Awake()
        {
            base.Awake();

            _agent = GetComponent<NavMeshAgent>();
            _agent.updateUpAxis = false;
            _agent.updatePosition = false;
            _agent.updateRotation = false;
        }

        public void Initialize()
        {
            AddBehaviour(new PatrolBehaviour(this, _path, _agent, Rigidbody));
            AddBehaviour(new ChaseBehaviour(this, _agent, Rigidbody));
            AddBehaviour(new FindTargetBehaviour(this, new TargetProvider(this, Layers.Player)));
            AddBehaviour(new AimBehaviour(this));
            AddBehaviour(new AttackBehaviour(this));
        }

        public void SetPatrolPath(Vector3[] path)
        {
            _path = path;
        }
    }
}