using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class MoveSystem : Executor
    {
        private Group group;
        private IMechanics src;

        public MoveSystem(Context context, IMechanics mech)
        {
            group = context.GetGroup(new Moves());
            src = mech;
        }

        public void Exec()
        {
            foreach(var obj in group.Select())
            {
                var point = delta(obj.move) + obj.position;
                obj.setPosition(point);
            }
        }

        private Vector3 delta(Move move)
        {
            return move.direction * move.speed * src.deltaTime;
        }
    }

    public class Moves : Selector
    {
        const float EPS = 0.01f;

        public bool check(Entity obj)
        {
            return obj.hasMove && nonZero(obj.move);
        }

        private bool nonZero(Move move)
        {
            return move.direction.magnitude > EPS && move.speed > EPS;
        }
    }
}