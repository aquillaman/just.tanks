using Pooling;

namespace Projectiles
{
    public class CanisterShot : Projectile
    {

        public override void Reset()
        {
            base.Reset();
            Pools.CanisterShot.Put(this);
        }
    }
}