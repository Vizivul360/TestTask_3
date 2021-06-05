using Ecs.Systems;
using Ecs;

namespace Game
{
    public class DeathSystem : Executor, Selector
    {
        private Group player, healthy, door;

        private Context context;

        private IUnits meta;
        private IGame game;

        public DeathSystem(Context context, Source src)
        {
            this.context = context;

            player = context.GetGroup(new PickPlayer());
            healthy = context.GetGroup(this);

            door = context.GetGroup(new PickDoor());

            meta = src.meta;
            game = src;
        }

        public bool check(Entity obj)
        {
            return obj.hasHealth;
        }

        public void Exec()
        {
            var pl = player.First();
            var alive = 0;

            foreach(var obj in healthy.Select())
            {
                if (obj.health.value > 0)
                {
                    alive++;
                    continue;
                }

                if (obj.identity.id == UID.PLAYER)
                {
                    context.Destroy(obj);
                    game.Loose();
                    break;
                }
                else
                {
                    var newValue = pl.coin.value + meta.frag2Coin;
                    pl.setCoin(newValue);

                    context.Destroy(obj);
                }
            }

            if (alive == 1 && player.First() != null)
                door.First().setDoor(true);
        }
    }
}
