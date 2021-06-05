using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecs.Pool;

namespace Ecs
{
    public partial class Context
    {
        private EntityPool entityPool = new EntityPool();

        public Entity CreateEntity()
        {
            return entityPool.Get();
        }

        public void Destroy(Entity entity)
        {
            entityPool.Return(entity);
        }

        public Group GetGroup(Selector select)
        {
            return new Group(entityPool, select);
        }
    }
}
