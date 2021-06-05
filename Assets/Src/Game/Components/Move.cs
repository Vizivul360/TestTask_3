using UnityEngine;
using Ecs;

namespace Game
{
    public class Move : IComponent
    {
        public float speed;

        public Vector3 direction;
    }
}
