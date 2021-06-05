using UnityEngine;

namespace Models
{
    public class CellModel : BaseModel, ICell
    {
        public Vector3 mult = Vector3.one;

        public bool immediateZ;

        public void setSize(Vector3 size)
        {
            var z = immediateZ ? mult.z : size.z * mult.z;

            transform.localScale = new Vector3(size.x * mult.x, size.y * mult.y, z);
        }
    }

    public interface ICell
    {
        void setSize(Vector3 value);
    }
}
