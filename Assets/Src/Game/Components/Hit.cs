using Ecs;

namespace Game
{
    public class Hit : IComponent
    {
        public float dmg;

        public bool kill;

        public bool empty
        {
            get { return dmg == 0; }
        }
    }
}
