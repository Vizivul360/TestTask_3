using System.Collections.Generic;
using System.Collections;
using System;

namespace Ecs.Pool
{
    public partial class EntityPool : BasePool<Entity>, Callback<Entity>, Entities
    {
        private List<Entity> active = new List<Entity>();
        private ListEnum<Entity> enumerator;

        public EntityPool()
        {
            enumerator = new ListEnum<Entity>(active);

            setCallback(this);
            createPools();
        }

        public void OnGet(Entity obj)
        {
            active.Add(obj);
        }

        public void OnReturn(Entity obj)
        {
            active.Remove(obj);
        }

        protected override Entity createNew()
        {
            var obj = new Entity();
            setPool(obj);

            return obj;
        }

        protected override void reset(Entity entity)
        {
            entity.Reset();
        }

        partial void setPool(Entity entity);

        partial void createPools();

        #region -= enumerable =-
        public IEnumerator<Entity> GetEnumerator()
        {
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return enumerator;
        }
        #endregion
    }
}
