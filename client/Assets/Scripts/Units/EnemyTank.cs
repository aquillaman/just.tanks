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
        public override Transform Transform => _transform;
        public override int LayerMask => Layers.Player;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
            _agent.updatePosition = false;
            _agent.updateRotation = false;
        }

        public void Initialize()
        {
            AddBehaviour(new PatrolBehaviour(this, _path, _agent, _rigidbody));
            AddBehaviour(new ChaseBehaviour(this, _agent, _rigidbody));
            AddBehaviour(new FindTargetBehaviour(new TargetProvider(this, LayerMask)));
            AddBehaviour(new AimBehaviour(this));
            AddBehaviour(new AttackBehaviour(this));
        }

        public void SetPatrolPath(Vector3[] path)
        {
            _path = path;
        }

        public override void Reset()
        {
            base.Reset();
            Pools.EnemyTank.Put(this);
        }
    }
}