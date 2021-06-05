using UnityEngine;
using Ecs;

namespace Game
{
    public class Cell : IComponent
    {
        public LayerType type;

        public Vector3 size;
    }
}
