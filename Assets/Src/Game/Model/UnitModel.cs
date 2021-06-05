using UnityEngine;
using Game;
using Ecs;

namespace Models
{
    public class UnitModel : BaseModel, Listener<Move>, Listener<Look>
    {
        public void On(Look obj)
        {
            setRotation(obj.direction);
        }

        public void On(Move obj)
        {
            var dir = obj.direction;
            if (dir.magnitude > 0) setRotation(dir);
        }

        private void setRotation(Vector3 to)
        {
            transform.rotation = Quaternion.Euler(0, -Mathf.Atan2(to.z, to.x) * Mathf.Rad2Deg, 0);
        }
    }
}
