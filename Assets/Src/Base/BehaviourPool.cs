using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public abstract class BehaviourPool : IObjects
    {
        [SerializeField]
        protected Transform root;

        public Transform transform { get { return root; } }

        private Stack<IObject> objs = new Stack<IObject>();
        private bool empty { get { return objs.Count == 0; } }

        private string name;
        private int total;

        public object Get()
        {
            nameUpdate();

            var obj = empty ? clone() : objs.Pop();

            obj.SetActive(true);

            return obj;
        }

        protected abstract IObject clone();

        private void nameUpdate()
        {
            if (total == 0) name = root.gameObject.name;
            if (empty) total++;

            root.gameObject.name = string.Format("[{0} {1}]", name, total);
        }

        public void Return(IObject obj)
        {
            obj.SetActive(false);
            objs.Push(obj);
        }
    }

    public interface IObjects
    {
        Transform transform { get; }

        object Get();

        void Return(IObject obj);
    }
}
