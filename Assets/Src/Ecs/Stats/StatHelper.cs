using UnityEngine;

namespace Ecs
{
    public class StatHelper : MonoBehaviour
    {
        public bool doPrint;

        private void Update()
        {
            if (doPrint)
            {
                Debug.Log(StatManager.Print());
                doPrint = false;
            }
        }
    }
}
