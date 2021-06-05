using System.Collections.Generic;

namespace Ecs
{
    public interface IComponent { }

    public interface Selector
    {
        bool check(Entity obj);
    }

    public interface Entities : IEnumerable<Entity> { }
}

namespace Ecs.Systems
{
    public interface Initializer
    {
        void Init();
    }

    public interface Executor
    {
        void Exec();
    }

    public interface Cleaner
    {
        void Clear();
    }
}
