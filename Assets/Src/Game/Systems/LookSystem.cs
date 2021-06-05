using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class LookSystem : Executor
    {
        private Group stayPlayer, stayBots, allBots, player;

        public LookSystem(Context context)
        {
            var moves = new Moves();

            stayPlayer = context.GetGroup(new LookSelect(new PickPlayer(), moves));
            stayBots = context.GetGroup(new LookSelect(new PickBot(), moves));

            player = context.GetGroup(new PickPlayer());
            allBots = context.GetGroup(new PickBot());
        }

        public void Exec()
        {
            playerLook();

            botsLook();
        }

        private void playerLook()
        {
            var p = stayPlayer.First();
            if (p != null) setLook(p, nearestBot(p.position));
        }

        private Entity nearestBot(Vector3 from)
        {
            float dist = 0;
            Entity bot = null;

            foreach(var obj in allBots.Select())
            {
                var d = (from - obj.position).magnitude;

                if (d < dist || bot == null)
                {
                    dist = d;
                    bot = obj;
                }
            }

            return bot;
        }

        private void botsLook()
        {
            var p = player.First();
            if (p == null) return;

            foreach (var obj in stayBots.Select())
                setLook(obj, p);
        }

        private void setLook(Entity obj, Entity to)
        {
            if (to == null)
            {
                obj.unsetLook(false);
                return;
            }

            obj.setLook(to.position.value - obj.position);
        }

        private class LookSelect : Selector
        {
            private Selector basic, exclude;

            public LookSelect(Selector basic, Selector exclude)
            {
                this.basic = basic;
                this.exclude = exclude;
            }

            public bool check(Entity obj)
            {
                return basic.check(obj) && exclude.check(obj) == false;
            }
        }
    }

    public class PickPlayer : Selector
    {
        public bool check(Entity obj)
        {
            return obj.hasIdentity && obj.identity.id == UID.PLAYER;
        }
    }

    public class PickBot : Selector
    {
        public bool check(Entity obj)
        {
            return obj.hasIdentity && obj.identity.id != UID.PLAYER;
        }
    }
}
