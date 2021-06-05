using UnityEngine;

namespace Collisions
{
    public class PlaneObj : ColliderBehaviour, IPlane
    {
        private void Awake()
        {
            var collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = createMesh();

            transform.localRotation = Quaternion.Euler(90, 0, 0);
        }

        private Mesh createMesh()   //not best solution
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            var mesh = obj.GetComponent<MeshFilter>().sharedMesh;

            Destroy(obj);
            return mesh;
        }

        public void setSize(Vector2 value)
        {
            transform.localScale = value;
        }
    }

    public interface IPlane : ColliderBind
    {
        void setSize(Vector2 value);
    }
}
