using Helpers;
using UnityEngine;

namespace Units
{
    public class TargetProvider : ITargetProvider
    {
        private readonly LayerMask _mask;
        private readonly ITargetProviderPrefs _prefs;

        public TargetProvider(ITargetProviderPrefs prefs, LayerMask mask)
        {
            _mask = mask;
            _prefs = prefs;
        }

        public bool TryGetTarget(out ITarget result)
        {
            var colliders = NonAllocHelper.Colliders;
            var count = Physics.OverlapSphereNonAlloc(_prefs.Position, _prefs.Radius, colliders, _mask);
            if (count > 0)
            {
                var minDist = float.MaxValue;
                ITarget closest = null;
                for (var i = 0; i < count; i++)
                {
                    var target = colliders[i].GetComponentInParent<ITarget>();

                    if (target == null) continue;

                    var dist = Vector3.Distance(_prefs.Position, target.Transform.position);
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