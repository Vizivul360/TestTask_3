using System.Collections.Generic;
using UnityEngine;
using Base;
using Game;
using Ecs;

namespace Collisions
{
    public class CollisionPool : RootPool, ICollision
    {
        private SubPool<SphereObj> spheres;
        private SubPool<PlaneObj> planes;
        private SubPool<CubeObj> cubes;

        private Dictionary<int, Entity> map;

        public Transform root { get; private set; }

        public Entity this[Collider collider]
        {
            get { return map[collider.gameObject.GetInstanceID()]; }
        }

        public CollisionPool()
        {
            root = new GameObject("[colliders]").transform;

            map = new Dictionary<int, Entity>();

            setPools();
        }

        private void setPools()
        {
            spheres = new SubPool<SphereObj>(this);
            planes = new SubPool<PlaneObj>(this);
            cubes = new SubPool<CubeObj>(this);
        }

        public void mapRemove(int id)
        {
            map.Remove(id);
        }

        public void mapSet(int id, Entity obj)
        {
            map[id] = obj;
        }

        public ICube GetCube(Entity obj)
        {
            return cubes.Pick(obj);
        }

        public IPlane GetPlane(Entity obj)
        {
            return planes.Pick(obj);
        }

        public ISphere GetSphere(Entity obj)
        {
            return spheres.Pick(obj);
        }

        private class SubPool<B> : BehaviourPool where B : ColliderBehaviour
        {
            private RootPool parent;

            public SubPool(RootPool obj)
            {
                parent = obj;
                createRoot();
            }

            private void createRoot()
            {
                root = new GameObject(getName()).transform;
                root.SetParent(parent.root);
            }

            private string getName()
            {
                return typeof(B).Name.Replace("Obj", "");
            }

            public B Pick(Entity obj)
            {
                var res = Get() as B;

                parent.mapSet(res.id, obj);

                return res;
            }

            protected override IObject clone()
            {
                var go = new GameObject(getName());
                go.transform.SetParent(root);

                var bind = go.AddComponent<B>();
                bind.SetParent(this);
                bind.setMap(parent);

                return bind;
            }
        }
    }

    public interface RootPool
    {
        Transform root { get; }

        void mapSet(int id, Entity obj);

        void mapRemove(int id);
    }
}
