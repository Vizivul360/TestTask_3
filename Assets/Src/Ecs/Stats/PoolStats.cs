using System;

namespace Ecs.Pool
{
    public class PoolStats : IStats
    {
        public Type type;

        public int active, inactive, total;

        public PoolStats(Type type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return string.Format("{0} | A_{1} I_{2} T_{3}", type.Name, active, inactive, total);
        }

        public void OnGet(bool isNew)
        {
            if (isNew) total++;
            else inactive--;

            active++;
        }

        public void OnReturn()
        {
            active--;
            inactive++;
        }
    }

    public interface IStats
    {
        void OnGet(bool isNew);

        void OnReturn();
    }
}
