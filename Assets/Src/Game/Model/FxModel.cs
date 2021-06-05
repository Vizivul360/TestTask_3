using System.Collections;
using UnityEngine;
using Game;
using Base;
using Ecs;

namespace Models
{
    public class FxModel : BaseObj, Listener<Position>
    {
        public ParticleSystem particle;
        public float duration;

        public void On(Position obj)
        {
            transform.position = obj;
            particle.Play();

            StartCoroutine(back2parent());
        }

        IEnumerator back2parent()
        {
            yield return new WaitForSeconds(duration);
            back2pool();
        }
    }
}
