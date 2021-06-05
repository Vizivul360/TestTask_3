using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcsEditor
{
    public class TEMPLATE
    {
        const string root = "Assets/Src/Ecs/Editor/template/";

        public const string
            POOL = root + "pool.txt",
            POOLS = root + "pools.txt",
            ENTITY_POOL = root + "entityPool.txt",
            ENTITY_LISTENER = root + "entityListener.txt",
            COMPONENT = root + "component.txt",
            ENTITY = root + "entity.txt";
    }

    public class GENERATED
    {
        const string root = "Assets/Src/Ecs/Generated/";

        public const string
            POOLS = root + "ComponentPools.cs",
            ENTITY_POOL = root + "EntityPool.cs",
            ENTITY_LISTENER = root + "EntityListener.cs",
            COMPONENT = root + "/Components/{0}Component.cs",
            ENTITY = root + "Entity.cs";
    }
}
