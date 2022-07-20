using Units;
using UnityEngine;

namespace Behaviours
{
    public class AttackBehaviour : Behaviour
    {
        public AttackBehaviour(Tank owner) : base(owner) { }

        public override void Update()
        {
            if (Owner.Target != null)
            {
                if (Vector3.Distance(Owner.Transform.position, Owner.Target.Transform.position) <= Owner.FireRadius)
                {
                    Owner.CurrentWeapon?.Shoot();
                }
            }
        }
    
        public override void FixedUpdate()
        {
            // no op
        }
    }
}