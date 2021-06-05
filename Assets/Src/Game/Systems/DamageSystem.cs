using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class DamageSystem : Executor, Cleaner, Selector
    {
        private IMechanics src;
        private Group projectiles, hits;
        private Context context;

        private Collider[] buffer = new Collider[10];
        private float radius;

        public DamageSystem(Context context, IMechanics mech)
        {
            this.context = context;

            projectiles = context.GetGroup(this);
            hits = context.GetGroup(new Hits());
            src = mech;

            radius = src.meta.bullet.radius;
        }

        public bool check(Entity obj)
        {
            return obj.hasDamage;
        }

        public void Exec()
        {
            foreach (var obj in projectiles.Select()) process(obj);
        }

        private void process(Entity proj)
        {
            var len = overlap(proj.position);
            var dmg = proj.damage;

            var destroy = false;

            for(var i = 0; i < len; i++)
            {
                var obj = src.collision[buffer[i]];
                var hit = canHit(dmg, obj);

                if (hit)
                {
                    var newHp = obj.health.value - dmg.value;
                    var max = obj.health.max;

                    obj.setHealth(newHp, max);
                    
                    var hitObj = createHit(dmg, proj.position);                    

                    if (newHp <= 0)
                    {
                        hitObj.setPosition(obj.position);
                        hitObj.hit.kill = true;
                    }
                }

                if (hit || obj.hasCell)
                    destroy = true;
            }

            if (destroy)
            {
                createHit(null, proj.position);
                context.Destroy(proj);
            }
        }

        private Entity createHit(Damage dmg, Position p)
        {
            var value = dmg == null ? 0 : dmg.value;

            var obj = context.CreateEntity();
            obj.setHit(value, false);
            obj.setPosition(p);

            return obj;
        }

        private bool canHit(Damage dmg, Entity obj)
        {
            var ident = obj.hasIdentity && obj.identity.side != dmg.side;
            var alive = obj.hasHealth && obj.health.value > 0;

            return ident && alive;
        }

        private int overlap(Vector3 point)
        {
            return Physics.OverlapSphereNonAlloc(point, radius, buffer, src.meta.hit);
        }

        public void Clear()
        {
            foreach (var obj in hits.Select())
                context.Destroy(obj);
        }
    }

    public class Hits : Selector
    {
        public bool check(Entity obj)
        {
            return obj.hasHit;
        }
    }
}
