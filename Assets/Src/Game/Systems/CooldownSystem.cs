using Ecs.Systems;
using Ecs;

namespace Game
{
    public class CooldownSystem : Executor, Selector
    {
        private Group group;
        private IMechanics src;

        public CooldownSystem(Context context, IMechanics mech)
        {
            group = context.GetGroup(this);
            src = mech;
        }

        public bool check(Entity obj)
        {
            return obj.hasCooldown && obj.cooldown.time2end >= 0;
        }

        public void Exec()
        {
            foreach (var obj in group.Select())
            {
                var cd = obj.cooldown;

                obj.setCooldown(cd.time2end - src.deltaTime);
            }
        }
    }
}
