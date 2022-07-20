using Units;

namespace Behaviours
{
    public class FindTargetBehaviour : Behaviour
    {
        public FindTargetBehaviour(Tank owner) : base(owner) { }
    
        public override void Update()
        {
            TargetProvider.GetTarget(Owner.Transform.position, Owner.AimRadius, Owner.LayerMask, out var target);
            Owner.Target = target;
        }
    
        public override void FixedUpdate()
        {
            // no op
        }
    }
}