using UnityEngine;

namespace Collisions
{
    public class CubeObj : ColliderBehaviour, ICube
    {
        private BoxCollider box;

        private void Awake()
        {
            box = gameObject.AddComponent<BoxCollider>();
        }

        public void setSize(Vector3 value)
        {
            box.size = value;
        }
    }

    public interface ICube : ColliderBind
    {
        void setSize(Vector3 value);
    }
}
