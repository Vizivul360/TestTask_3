using System.Collections.Generic;
using System.Collections;

namespace Ecs
{
    public class ListEnum<T> : IEnumerator<T>
    {
        private List<T> data;
        private int index;

        public T Current
        {
            get { return data[index]; }
        }

        object IEnumerator.Current
        {
            get { return data[index]; }
        }

        public ListEnum(List<T> data)
        {
            this.data = data;
        }

        public bool MoveNext()
        {
            index++;
            return index < data.Count;
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose()
        {
            Reset();
        }
    }
}
