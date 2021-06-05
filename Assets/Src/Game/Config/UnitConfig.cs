using UnityEngine;
using System;

namespace Game
{
    [CreateAssetMenu]
    public class UnitConfig : ScriptableObject, IUnits
    {
        public float _radius;
        public float unitRadius { get { return _radius; } }

        public float _stopDist;
        public float stopDistance { get { return _stopDist; } }

        public Bullet _bullet;
        public Bullet bullet { get { return _bullet; } }

        public int _frag2Coin;
        public int frag2Coin { get { return _frag2Coin; } }

        [Space]
        public UnitMeta _player;
        public UnitMeta Player { get { return _player; } }

        [Space]
        public BotMeta flyBot, landBot;

        public BotMeta FlyBot { get { return flyBot; } }
        public BotMeta LandBot { get { return landBot; } }

        [Header("Masks")]
        public LayerMask _moveGround;
        public LayerMask moveGround { get { return _moveGround; } }
        
        public LayerMask _moveFly;
        public LayerMask moveFly { get { return _moveFly; } }

        public LayerMask _hit;
        public LayerMask hit { get { return _hit; } }

        public LayerMask _overlap;
        public LayerMask overlap { get { return _overlap; } }

        public LayerMask _door;
        public LayerMask door { get { return _door; } }
    }

    [Serializable]
    public class UnitMeta
    {
        public float moveSpeed, attackSpeed, hp, dmg;
    }

    [Serializable]
    public class AIMeta
    {
        public float idle, walkDistance;
    }

    [Serializable]
    public class BotMeta
    {
        public int count;

        public AIMeta ai;

        public UnitMeta unit;
    }

    [Serializable]
    public class Bullet
    {
        public float speed, radius;
    }
}
