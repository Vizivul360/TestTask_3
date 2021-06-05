using UnityEngine;
using Ecs;

namespace Game
{
    public class Position : IComponent
    {
        public Vector3 value;

        public static implicit operator Vector3(Position p)
        {
            return p == null ? Vector3.zero : p.value;
        }
    }
}
