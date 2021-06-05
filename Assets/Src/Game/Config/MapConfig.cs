using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        public Texture2D[] maps;

        public int count
        {
            get { return maps.Length; }
        }

        public IMap this[int index]
        {
            get { return new MapObj(maps[index]); }
        }
    }

    public enum LayerType
    {
        none = 0,

        water = 4,
        wall = 8,
        hitbox = 9,
        border = 10,
        door = 11
    }
}
