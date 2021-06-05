using System.Collections.Generic;
using UnityEngine;
using Game;
using Base;

namespace Models
{
    public class ModelPool
    {
        private Transform root;

        private ModelConfig template;

        private Dictionary<string, IPool> pools;

        public ModelPool(ModelConfig obj)
        {
            root = new GameObject("[models]").transform;

            template = obj;

            createPools();
        }

        private void createPools()
        {
            pools = new Dictionary<string, IPool>();

            foreach (var name in template.names())
                pools[name] = new SubPool(template.Get(name), root);

            pools["none"] = new EmptyPool();
        }

        public object Get(string name)
        {
            return pools[name].Get();
        }

        private class EmptyPool : IPool
        {
            public object Get()
            {
                return null;
            }
        }

        private class SubPool : ObjPool, IPool
        {
            public SubPool(GameObject prefab, Transform root)
            {
                template = prefab;
                createRoot(root);
            }
        }

        private interface IPool
        {
            object Get();
        }
    }
}
