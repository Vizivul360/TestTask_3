using UnityEngine;
using Game;
using Base;
using Ecs;

namespace Models
{
    public class BaseModel : BaseObj, Listener<Position>, DestroyListener
    {
        public Vector3 offset;

        public void On(Position obj)
        {
            transform.position = obj + offset;
        }

        public void EntityDestroy()
        {
            back2pool();
        }
    }
}
