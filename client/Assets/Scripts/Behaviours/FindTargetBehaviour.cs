using Units;

namespace Behaviours
{
    public class FindTargetBehaviour : Behaviour
    {
        private readonly TargetProvider _targetProvider;

        public FindTargetBehaviour(TargetProvider targetProvider) : base(null)
        {
            _targetProvider = targetProvider;
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            _targetProvider.TryGetTarget(out var target);
            Owner.Target = target;
        }
    
        public override void FixedUpdate()
        {
            // no op
        }
    }
}