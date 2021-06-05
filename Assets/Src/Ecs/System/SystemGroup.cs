using System.Collections.Generic;

namespace Ecs.Systems
{
    public abstract class SystemGroup<S> where S : class
    {
        private List<S> systems = new List<S>();

        protected abstract void call(S obj);

        public void Add(object obj)
        {
            var system = obj as S;

            if (system != null) systems.Add(system);
        }

        protected void Call()
        {
            foreach (var s in systems) call(s);
        }
    }

    public class InitGroup : SystemGroup<Initializer>, Initializer
    {
        public void Init() { Call(); }

        protected override void call(Initializer obj) { obj.Init(); }
    }

    public class ExecGroup : SystemGroup<Executor>, Executor
    {
        public void Exec() { Call(); }

        protected override void call(Executor obj) { obj.Exec(); }
    }

    public class CleanGroup : SystemGroup<Cleaner>, Cleaner
    {
        public void Clear() { Call(); }

        protected override void call(Cleaner obj) { obj.Clear(); }
    }
}
