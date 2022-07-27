using Units;
using UnityEngine;

namespace Behaviours
{
    public class AimBehaviour : Behaviour
    {
        private readonly Tank _owner;
        public AimBehaviour(Tank owner)
        {
            _owner = owner;
        }

        public override void Update()
        {
            var target = _owner.Target;
            var turret = _owner.Turret;
            var direction = _owner.transform.forward;
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