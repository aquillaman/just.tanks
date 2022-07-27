using Behaviours;
using PlayerInput;
using UnityEngine;

namespace Units
{
    public class PlayerTank : Tank, IAgent, ITargetProviderPrefs
    {
        private Transform _transform;
        private PlayerMovementController _movementController;
        public float Radius => AimRadius;
        public Vector3 Position => Transform.position;
        public override Transform Transform => _transform;
        public override int LayerMask => Layers.Enemy;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
            _movementController = GetComponent<PlayerMovementController>();
        }

        public void Initialize()
        {
            AddBehaviour(new FindTargetBehaviour(this, new TargetProvider(this, LayerMask)));
            AddBehaviour(new AimBehaviour(this));
            AddBehaviour(new AttackBehaviour(this));
        }
    
        void IAgent.Move(Vector2 value)
        {
            _movementController.Move(value);
        }

        void IAgent.ChangeWeapon()
        {
            ChangeWeapon();
        }
        
        public override void Reset()
        {
            Destroy(gameObject);
        }
    }
}