using UnityEngine.UI;
using UnityEngine;
using Base;
using Game;
using Ecs;

namespace UI
{
    public class HpBar : BaseObj, Listener<Position>, Listener<Health>, DestroyListener
    {
        public Slider slider;

        private Vector3 anchor;

        public void On(Health hp)
        {
            slider.maxValue = hp.max;
            slider.value = hp.value;
        }

        public void On(Position obj)
        {
            anchor = obj.value;
        }

        public void EntityDestroy()
        {
            back2pool();
        }

        private void LateUpdate()
        {
            transform.position = Camera.main.WorldToScreenPoint(anchor);
        }
    }
}
