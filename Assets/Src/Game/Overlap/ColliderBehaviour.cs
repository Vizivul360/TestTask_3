using Base;
using Game;
using Ecs;

namespace Collisions
{
    public class ColliderBehaviour : BaseObj, ColliderBind, Listener<Position>, DestroyListener
    {
        public int id
        {
            get { return gameObject.GetInstanceID(); }
        }

        public LayerType layer
        {
            set { gameObject.layer = (int)value; }
        }

        private RootPool root;

        public void setMap(RootPool obj)
        {
            root = obj;
        }

        public void On(Position obj)
        {
            transform.position = obj.value;
        }

        public void EntityDestroy()
        {
            root.mapRemove(id);
            back2pool();
        }
    }

    public interface ColliderBind
    {
        int id { get; }

        LayerType layer { set; }

        void setMap(RootPool root);
    }
}
