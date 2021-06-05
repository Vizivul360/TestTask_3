using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class CollisionCorrect : Executor
    {
        private Group group;

        private IUnits src;
        private ICollision collision;

        private float radius;

        private Collider[] buffer = new Collider[10];
        private Quaternion[] sector;

        public CollisionCorrect(Context context, IMechanics mech)
        {
            group = context.GetGroup(new MovedUnits());

            src = mech.meta;

            collision = mech.collision;

            radius = src.unitRadius;

            createSector();
        }

        private void createSector()
        {
            var len = 5;
            sector = new Quaternion[len];

            for (var i = 0; i < len; i++)
                sector[i] = Quaternion.Euler(0, -90 + i * 180.0f / (len - 1), 0);
        }

        public void Exec()
        {
            foreach(var obj in group.Select())
            {
                Vector3 point = obj.position + correctOverlap(obj);

                var dir = obj.move.direction.normalized;

                for (var i = 0; i < sector.Length; i++)
                    point += correct(sector[i] * dir, point);

                obj.setPosition(point);
            }
        }

        private Vector3 correct(Vector3 dir, Vector3 p)
        {
            RaycastHit hit;

            if (Physics.Raycast(p, dir, out hit, radius, src.moveGround))
            {
                var dp = (p - hit.point).magnitude;
                return dir * (dp - radius);
            }

            return Vector3.zero;
        }

        private Vector3 correctOverlap(Entity obj)
        {
            var len = Physics.OverlapSphereNonAlloc(obj.position, radius, buffer, src.overlap);
            var id = obj.identity.id;

            var delta = Vector3.zero;

            for (var i = 0; i < len; i++)
            {
                var e = collision[buffer[i]];

                if (e.identity.id != id)
                {
                    var dp = obj.position.value - e.position;
                    var l = 2 * radius - dp.magnitude;

                    delta += dp.normalized * l;
                }
            }

            return delta;
        }

        private class MovedUnits : Selector
        {
            private Selector move = new Moves();

            public bool check(Entity obj)
            {
                return obj.hasIdentity && move.check(obj);
            }
        }
    }
}
