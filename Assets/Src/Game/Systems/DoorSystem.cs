using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class DoorSystem : Executor, Selector
    {
        private Group player, openDoor;

        private IUnits meta;
        private IGame game;

        private Collider[] buffer = new Collider[1];

        public DoorSystem(Context context, Source src)
        {
            player = context.GetGroup(new PickPlayer());
            openDoor = context.GetGroup(this);

            meta = src.meta;
            game = src;
        }

        public void Exec()
        {
            if (openDoor.Exists())
            {
                var p = player.First().position;
                var overlap = Physics.OverlapSphereNonAlloc(p, meta.unitRadius, buffer, meta.door) > 0;

                if (overlap) game.Win();
            }
        }

        public bool check(Entity obj)
        {
            return obj.hasDoor && obj.door.isOpen;
        }
    }

    public class PickDoor : Selector
    {
        public bool check(Entity obj)
        {
            return obj.hasDoor;
        }
    }
}
