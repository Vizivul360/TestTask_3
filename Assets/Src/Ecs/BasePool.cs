using System.Collections.Generic;

namespace Ecs.Pool
{
    public abstract class BasePool<T> : IPool<T> where T : class
    {
        private Stack<T> data = new Stack<T>();

        private Callback<T> callback = new EmptyCallback<T>();

        private bool empty { get { return data.Count == 0; } }

        private IStats stats = StatManager.GetStats(typeof(T));

        public T Get()
        {
            stats.OnGet(empty);

            var obj = empty ? createNew() : data.Pop();

            callback.OnGet(obj);

            return obj;
        }

        public void Return(T obj)
        {
            stats.OnReturn();

            callback.OnReturn(obj);

            reset(obj);

            data.Push(obj);
        }

        protected void setCallback(Callback<T> obj)
        {
            callback = obj;
        }

        protected abstract T createNew();

        protected abstract void reset(T obj);
    }

    public class EmptyCallback<T> : Callback<T>
    {
        public void OnGet(T obj) { }
        public void OnReturn(T obj) { }
    }

    public interface Callback<T>
    {
        void OnGet(T obj);
        void OnReturn(T obj);
    }

    public interface IPool<T>
    {
        T Get();

        void Return(T obj);
    }
}
