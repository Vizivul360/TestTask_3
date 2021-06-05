using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class BotSystem : Executor
    {
        private Group idleBots, walkBots, player;
        private IMechanics src;

        public BotSystem(Context context, IMechanics mech)
        {
            idleBots = context.GetGroup(new Idlers());
            walkBots = context.GetGroup(new Walkers());
            player = context.GetGroup(new PickPlayer());

            src = mech;
        }

        public void Exec()
        {
            idleBots.Select();
            walkBots.Select();

            processIdle();
            processWalk();
        }

        private void processIdle()
        {
            foreach(var obj in idleBots)
            {
                var time = obj.botidle.time2walk;
                time -= src.deltaTime;

                if (time > 0)
                    obj.setBotIdle(time);
                else
                {
                    obj.unsetBotIdle();
                    obj.setBotWalk(obj.position);
                }
            }
        }

        private void processWalk()
        {
            var to = player.First().position.value;

            foreach(var obj in walkBots)
            {
                var dist = (obj.botwalk.from - obj.position).magnitude;
                var look = to - obj.position;
                
                var distMax = obj.ai.walkDist;

                if (dist < distMax && look.magnitude > src.meta.stopDistance)
                    obj.setMove(obj.ai.walkSpeed, new Vector3(look.x, 0, look.z).normalized);
                else
                {
                    obj.unsetBotWalk();
                    obj.unsetMove(false);

                    obj.setBotIdle(obj.ai.idleTime);
                }
            }
        }

        private class Walkers : Selector
        {
            private Selector bot = new PickBot();

            public bool check(Entity obj)
            {
                return obj.hasBotWalk && bot.check(obj);
            }
        }

        private class Idlers : Selector
        {
            private Selector bot = new PickBot();

            public bool check(Entity obj)
            {
                return obj.hasBotIdle && bot.check(obj);
            }
        }
    }
}
