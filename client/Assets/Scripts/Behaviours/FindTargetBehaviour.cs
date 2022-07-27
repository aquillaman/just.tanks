using Units;

namespace Behaviours
{
    public class FindTargetBehaviour : Behaviour
    {
        private readonly Tank _owner;
        private readonly TargetProvider _targetProvider;

        public FindTargetBehaviour(Tank owner, TargetProvider targetProvider)
        {
            _owner = owner;
            _targetProvider = targetProvider;
        }

        public override void Update()
        {
            _targetProvider.TryGetTarget(out var target);
            _owner.Target = target;
        }

        public override void FixedUpdate()
        {
            // no op
        }
    }
}