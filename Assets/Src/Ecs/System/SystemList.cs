using System.Collections.Generic;
using UnityEngine.Profiling;

namespace Ecs.Systems
{
    public class SystemList : ISystems, Initializer, Executor, Cleaner
    {
        private List<Initializer> initializers = new List<Initializer>();
        private List<Executor> executors = new List<Executor>();
        private List<Cleaner> cleaners = new List<Cleaner>();

        private Dictionary<object, string> names = new Dictionary<object, string>();

        protected void Add(object system)
        {
            register(system, initializers);
            register(system, executors);
            register(system, cleaners);

            names[system] = system.GetType().Name;
        }

        private void register<S>(object obj, List<S> to) where S : class
        {
            var system = obj as S;

            if (system != null) to.Add(system);
        }

        public void Init()
        {
            foreach (var i in initializers) i.Init();
        }

        public void Exec()
        {
            sample(GetType().Name);

            foreach (var e in executors)
                ProfileExec(e);

            sampleEnd();
        }

        private void ProfileExec(Executor obj)
        {
            sample(names[obj]);

            obj.Exec();

            sampleEnd();
        }

        public void Clear()
        {
            foreach (var c in cleaners) c.Clear();
        }

        public void Update()
        {
            Exec();
            Clear();
        }

        private void sample(string name)
        {
            Profiler.BeginSample(name);
        }

        private void sampleEnd()
        {
            Profiler.EndSample();
        }
    }

    public interface ISystems
    {
        void Init();

        void Update();
    }
}
