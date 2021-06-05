using System.Collections.Generic;

namespace Ecs
{
    public partial class EntityListener
    {
        public List<DestroyListener> onDestroy = new List<DestroyListener>();

        public void OnDestroy()
        {
            foreach (var obj in onDestroy)
                obj.EntityDestroy();
        }

        public void Clear()
        {
            onDestroy.Clear();
            clearTyped();
        }

        partial void clearTyped();
    }

    public interface DestroyListener
    {
        void EntityDestroy();
    }

    public interface Listener<C>
    {
        void On(C obj);
    }
}
