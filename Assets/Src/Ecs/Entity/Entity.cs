using Ecs.Pool;

namespace Ecs
{
    public partial class Entity
    {
        private EntityListener react = new EntityListener();

        private void remove<T>(T obj, IPool<T> pool) where T : class
        {
            if (obj != null) pool.Return(obj);
        }

        public void AddListener(object obj)
        {
            var destroy = obj as DestroyListener;
            if (destroy != null) react.onDestroy.Add(destroy);

            addTypedListener(obj);
        }

        public void Reset()
        {
            removeComponents();

            react.OnDestroy();
            react.Clear();
        }

        partial void addTypedListener(object obj);

        partial void removeComponents();
    }
}
