using UnityEngine;

namespace Base
{
    /// <summary>
    /// Base class for behaviour pooled object
    /// </summary>
    public class BaseObj : MonoBehaviour, IObject
    {
        private IObjects parent;

        public void SetParent(IObjects obj)
        {
            transform.SetParent(obj.transform);
            parent = obj;

            setName();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private void setName()
        {
            gameObject.name = GetType().Name;
        }

        protected void back2pool()
        {
            parent.Return(this);
        }
    }

    public interface IObject
    {
        void SetParent(IObjects obj);

        void SetActive(bool value);
    }
}
