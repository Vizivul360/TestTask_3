using System.Collections.Generic;
using System.Collections;
using UnityEngine.Profiling;

namespace Ecs
{
    public class Group : Entities
    {
        private Entities all;

        private Selector select;

        private List<Entity> selected = new List<Entity>();
        private ListEnum<Entity> enumerator;

        private string name;

        public Group(Entities all, Selector select)
        {
            this.all = all;
            this.select = select;

            name = "Select " + select.GetType().Name;
            enumerator = new ListEnum<Entity>(selected);
        }

        public Entity First()
        {
            //using (Sample.Get(select))
            try
            {
                Profiler.BeginSample(name);

                foreach (var obj in all)
                    if (select.check(obj)) return obj;

                return null;
            }
            finally
            {
                Profiler.EndSample();
            }
        }

        public bool Exists()
        {
            //using (Sample.Get(select))
            try
            {
                Profiler.BeginSample(name);

                foreach (var obj in all)
                    if (select.check(obj)) return true;

                return false;
            }
            finally
            {
                Profiler.EndSample();
            }
        }

        public Entities Select()
        {
            //using (Sample.Get(select))
            try
            {
                Profiler.BeginSample(name);

                selected.Clear();

                foreach (var obj in all)
                    if (select.check(obj)) selected.Add(obj);

                return this;
            }
            finally
            {
                Profiler.EndSample();
            }
        }

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
