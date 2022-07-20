using Units;
using UnityEngine;

namespace Behaviours
{
    public class AimBehaviour : Behaviour
    {
        public AimBehaviour(Tank owner) : base(owner) { }

        public override void Update()
        {
            var target = Owner.Target;
            var turret = Owner.Turret;
            var direction = Owner.transform.forward;
            if (target != null)
            {
                direction = turret.position.DirectionTo(target.Transform.position);
            }
            turret.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }

        public override void FixedUpdate()
        {
            // no op
        }
    }
}