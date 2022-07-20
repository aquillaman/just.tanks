using Units;

namespace Behaviours
{
    public abstract class Behaviour
    {
        protected Tank Owner;

        private protected Behaviour(Tank owner)
        {
            Owner = owner;
        }

        public abstract void Update();
        public abstract void FixedUpdate();
    }
}