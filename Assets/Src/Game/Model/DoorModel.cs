using UnityEngine;
using Game;
using Ecs;

namespace Models
{
    public class DoorModel : CellModel, Listener<Door>
    {
        public Animator _animator;

        public void On(Door obj)
        {
            if (obj.isOpen)
            {
                _animator.enabled = true;
                _animator.Play("open", 0);
            }
            else
            {
                _animator.Play("idle", 0);
                _animator.enabled = false;
            }
        }
    }
}
