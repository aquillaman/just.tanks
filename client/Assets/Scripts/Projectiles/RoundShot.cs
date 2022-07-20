using Pooling;

namespace Projectiles
{
    public class RoundShot : Projectile
    {
        public override void Reset()
        {
            base.Reset();
            Pools.RoundShot.Put(this);
        }
    }
}