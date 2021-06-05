using UnityEngine;
using Game;
using Ecs;

namespace Models
{
    public class BulletModel : BaseModel, Listener<Move>
    {
        public void On(Move obj)
        {
            transform.rotation = Quaternion.LookRotation(obj.direction);
        }
    }
}
