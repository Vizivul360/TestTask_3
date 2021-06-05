using UnityEngine;

namespace Base
{
    [System.Serializable]
    public class ObjPool : BehaviourPool
    {
        [SerializeField]
        protected GameObject template;

        protected void createRoot(Transform parent)
        {
            root = new GameObject(template.name).transform;
            root.SetParent(parent);
        }

        protected override IObject clone()
        {
            var go = Object.Instantiate(template);

            var obj = go.GetComponent<IObject>();
            obj.SetParent(this);

            return obj;
        }
    }
}
