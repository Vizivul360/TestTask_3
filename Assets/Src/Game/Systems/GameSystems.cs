using Ecs.Systems;
using Ecs;

namespace Game
{
    public class GameSystems : SystemList
    {
        public GameSystems(Context context, Source src)
        {
            Add(new LevelBuilder(context, src));
            Add(new UnitSpawner(context, src));
            
            Add(new InputSystem(context, src));
            Add(new MoveSystem(context, src));
            Add(new CollisionCorrect(context, src));
            Add(new LookSystem(context));

            Add(new CooldownSystem(context, src));
            Add(new AttackSystem(context, src));
            
            Add(new DamageSystem(context, src));
            Add(new BotSystem(context, src));

            if (src.render)
                Add(new ViewSystem(context, src));

            Add(new DeathSystem(context, src));
            Add(new DoorSystem(context, src));
        }
    }
}
