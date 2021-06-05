using UnityEngine;

namespace Game
{
    public class MapObj : IMap
    {
        private Texture2D _data;

        public MapObj(Texture2D obj)
        {
            _data = obj;
        }

        public LayerType this[int j, int i]
        {
            get { return Pixel2Layer.convert(_data.GetPixel(j, i)); }
        }

        public int height
        {
            get { return _data.height; }
        }

        public int width
        {
            get { return _data.width; }
        }

        public Vector2 size
        {
            get { return new Vector2(width, height); }
        }
    }

    public interface IMap
    {
        int width { get; }
        int height { get; }

        Vector2 size { get; }

        LayerType this[int j, int i] { get; }
    }

    public class Pixel2Layer
    {
        const float EPS = 0.1f;

        public static LayerType convert(Color color)
        {
            if (near(color, Color.black))
                return LayerType.wall;

            if (near(color, Color.gray))
                return LayerType.water;

            return LayerType.none;
        }

        private static bool near(Color c0, Color c1)
        {
            return near(c0.r, c1.r) && near(c0.g, c1.g) && near(c0.b, c1.b);
        }

        private static bool near(float a, float b)
        {
            return Mathf.Abs(a - b) < EPS;
        }
    }   
}
