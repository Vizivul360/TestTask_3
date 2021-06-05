using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class InputSystem : Executor, Cleaner
    {
        private Group player;
        private float speed;

        private IMechanics src;

        public InputSystem(Context context, IMechanics src)
        {
            player = context.GetGroup(new Player());
            speed = src.meta.Player.moveSpeed;

            this.src = src;
        }

        public void Exec()
        {
            var x = getAxis("Horizontal");
            var z = getAxis("Vertical");
            var v = new Vector3(x, 0, z);

            player.First().setMove(speed, v);
        }

        private float getAxis(string name)
        {
            return src.rawAxis ? Input.GetAxisRaw(name) : Input.GetAxis(name);
        }

        public void Clear()
        {
            var obj = player.First();
            if (obj != null) obj.unsetMove(false);
        }

        private class Player : Selector
        {
            public bool check(Entity obj)
            {
                return obj.hasIdentity && obj.identity.id == UID.PLAYER;
            }
        }
    }
}
