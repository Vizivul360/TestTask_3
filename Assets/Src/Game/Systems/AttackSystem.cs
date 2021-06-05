using Ecs.Systems;
using Ecs;

namespace Game
{
    public class AttackSystem : Executor
    {
        private Group ready2Attack;
        private Context context;

        private float projSpeed;

        public AttackSystem(Context context, IMechanics src)
        {
            ready2Attack = context.GetGroup(new Ready2Attack());

            projSpeed = src.meta.bullet.speed;

            this.context = context;
        }

        public void Exec()
        {
            foreach(var shooter in ready2Attack.Select())
            {
                shooter.setCooldown(1.0f / shooter.attack.speed);

                if (shooter.hasLook)
                    createProjectile(shooter);
            }
        }

        private void createProjectile(Entity shooter)
        {
            var identity = shooter.identity;
            var atk = shooter.attack;
            
            var obj = context.CreateEntity();

            obj.setPosition(shooter.position);
            obj.setMove(projSpeed, shooter.look.direction.normalized);
            obj.setDamage(atk.dmg, identity.side);
            obj.setModel("bullet");
        }

        private class Ready2Attack : Selector
        {
            private Selector move = new Moves();

            public bool check(Entity obj)
            {
                var ready = obj.hasCooldown && obj.cooldown.time2end <= 0;
                var stay = move.check(obj) == false;

                return ready && stay;
            }
        }
    }
}
