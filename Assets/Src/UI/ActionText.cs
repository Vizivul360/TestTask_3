using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using Base;
using Game;
using Ecs;

namespace UI
{
    public class ActionText : BaseObj, Listener<Hit>, Listener<Position>
    {
        public Text label;

        public float duration;

        public FloatRange dx, dy;

        public void On(Hit obj)
        {
            label.text = obj.kill ? "KILL" : obj.dmg.ToString("#");
            label.color = Color.red;
        }

        public void On(Position obj)
        {
            transform.position = Camera.main.WorldToScreenPoint(obj);
            StartCoroutine(animCoro());
        }

        IEnumerator animCoro()
        {
            var iterations = duration / Time.deltaTime;

            var dp = new Vector2(dx.rnd(), dy.rnd()) / iterations;
            var da = 1.0f / iterations;

            for (var i = 0; i < iterations; i++)
            {
                shift(dp);

                var color = label.color;
                color.a -= da;

                label.color = color;

                yield return null;
            }

            back2pool();
        }

        private void shift(Vector3 delta)
        {
            var p = transform.localPosition;
            transform.localPosition = p + delta;
        }

        [Serializable]
        public class FloatRange
        {
            public float min, max;

            public float rnd()
            {
                return UnityEngine.Random.Range(min, max);
            }
        }
    }
}
