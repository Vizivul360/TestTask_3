using UnityEngine;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        private float time2start;
        private OnTime callback;

        public bool active { get { return time2start > 0; } }

        public void Run(float seconds, OnTime onTime)
        {
            time2start = seconds;
            callback = onTime;
            enabled = true;

            callback.OnSeconds(seconds);
        }

        private void Update()
        {
            time2start -= Time.deltaTime;

            if (time2start < 0)
            {
                time2start = 0;
                enabled = false;
                callback.TimeEnd();

                return;
            }

            callback.OnSeconds(time2start);
        }
    }

    public interface OnTime
    {
        void OnSeconds(float sec);
        void TimeEnd();
    }
}
