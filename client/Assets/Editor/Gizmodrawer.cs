using Units;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class GizmoDrawer
    {
        [DrawGizmo(GizmoType.Active, typeof(Tank))]
        public static void DrawTankGizmos(Tank tank, GizmoType gizmoType)
        {
            if (Application.isPlaying)
            {
                var position = tank.transform.position;
                Gizmos.DrawWireSphere(position, tank.AimRadius);
                var color = Gizmos.color;
                Gizmos.color= Color.red;
                Gizmos.DrawWireSphere(position, tank.FireRadius);
                Gizmos.color = color;
            }
        }
    }
}