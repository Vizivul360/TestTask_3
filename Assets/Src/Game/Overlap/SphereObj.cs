using UnityEngine;

namespace Collisions
{
    public class SphereObj : ColliderBehaviour, ISphere
    {
        private SphereCollider sphere;

        private void Awake()
        {
            sphere = gameObject.AddComponent<SphereCollider>();
        }

        public void setRadius(float value)
        {
            sphere.radius = value;
        }
    }

    public interface ISphere : ColliderBind
    {
        void setRadius(float value);
    }
}
