using UnityEngine;

namespace Units
{
    public class TargetProvider
    {
        private static readonly Collider[] Colliders = new Collider[15];

        public static bool GetTarget(Vector3 position, float radius, LayerMask mask, out ITarget result)
        {
            var count = Physics.OverlapSphereNonAlloc(position, radius, Colliders, mask);
            if (count > 0)
            {
                var minDist = float.MaxValue;
                ITarget closest = null;
                for (var i = 0; i < count; i++)
                {
                    var target = Colliders[i].GetComponentInParent<ITarget>();

                    if (target == null) { continue; }

                    var dist = Vector3.Distance(position, target.Transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = target;
                    }
                }
                
                result = closest;
                return result != null;
            }

            result = null;
            return false;
        }
    }
}