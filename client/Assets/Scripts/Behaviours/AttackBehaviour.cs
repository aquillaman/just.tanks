using Units;
using UnityEngine;

namespace Behaviours
{
    public class AttackBehaviour : Behaviour
    {
        private readonly Tank _owner;
        public AttackBehaviour(Tank owner)
        {
            _owner = owner;
        }

        public override void Update()
        {
            if (_owner.Target != null)
            {
                if (Vector3.Distance(_owner.Transform.position, _owner.Target.Transform.position) <= _owner.FireRadius)
                {
                    _owner.CurrentWeapon?.Shoot();
                }
            }
        }
    
        public override void FixedUpdate()
        {
            // no op
        }
    }
}