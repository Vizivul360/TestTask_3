using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class ModelConfig : ScriptableObject
    {
        public List<GameObject> objs;

        public string[] names()
        {
            return objs.ConvertAll(obj => obj.name).ToArray();
        }

        public GameObject Get(string name)
        {
            return objs.Find(obj => obj && obj.name == name);
        }
    }
}
