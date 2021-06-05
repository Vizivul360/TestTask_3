using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class GizmoSystem : Executor
    {
        private IGizmos flag;
        private IMechanics mech;

        private Group cells, units, projectiles;

        public GizmoSystem(Context context, Source src)
        {
            flag = src;
            mech = src;

            cells = context.GetGroup(new Cells());
            units = context.GetGroup(new Units());
            projectiles = context.GetGroup(new Projectiles());
        }

        public void Exec()
        {
            if (flag.level) drawLevel();

            if (flag.units) drawUnits();

            if (flag.projectiles) drawProjectiles();
        }

        private void drawLevel()
        {
            foreach (var obj in cells.Select())
                drawCell(obj);
        }

        private void drawUnits()
        {
            foreach(var obj in units.Select())
            {
                var player = obj.identity.id == UID.PLAYER;
                Gizmos.color = player ? Color.green : Color.red;

                Gizmos.DrawWireSphere(obj.position, mech.meta.unitRadius);

                if (player == false)
                {
                    var p = obj.position;

                    if (obj.hasMove)
                        Gizmos.DrawLine(p, obj.move.direction + p);
                    else if (obj.hasLook)
                        Gizmos.DrawLine(p, obj.look.direction + p);
                }   

                var tag = string.Empty;

                if (obj.hasBotIdle) tag = "idle";
                if (obj.hasBotWalk) tag = "walk";
                if (obj.hasCoin) tag = "coin_" + obj.coin.value;

                label(obj.position,
                    "{0} : hp_{1} cd_{2} {3}",
                    obj.identity.id,
                    obj.health.value,
                    obj.cooldown.time2end.ToString("0.00"),
                    tag);
            }
        }

        private void drawProjectiles()
        {
            foreach(var obj in projectiles.Select())
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(obj.position, mech.meta.bullet.radius);

                var dmg = obj.damage;

                label(obj.position, "side_{0}, dmg_{1}", dmg.side, dmg.value);
            }
        }

        private void drawCell(Entity obj)
        {
            var cell = obj.cell;
            if (cell.size.z == 0) return;

            Gizmos.color = color(cell.type);
            Gizmos.DrawWireCube(obj.position, cell.size);
        }

        private void label(Vector3 point, string format, params object[] args)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.Label(point, new GUIContent(string.Format(format, args)));
#endif
        }

        private Color color(LayerType type)
        {
            switch(type)
            {
                case LayerType.border:
                    return Color.black;

                case LayerType.water:
                    return Color.blue;

                case LayerType.wall:
                    return Color.yellow;

                case LayerType.door:
                    return Color.white;

                default:
                    return Color.white;
            }
        }

        private class Cells : Selector
        {
            public bool check(Entity obj)
            {
                return obj.hasCell;
            }
        }

        private class Units : Selector
        {
            public bool check(Entity obj)
            {
                return obj.hasCooldown && obj.hasIdentity && obj.hasHealth;
            }
        }

        private class Projectiles : Selector
        {
            public bool check(Entity obj)
            {
                return obj.hasDamage;
            }
        }
    }
}
