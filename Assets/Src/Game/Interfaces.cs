using UnityEngine;
using Collisions;
using Ecs;
using UI;

namespace Game
{
    public interface Source : IMechanics, IViews, IGizmos, IGame { }

    public interface IGame
    {
        void Pause();

        void Resume();

        void Loose();

        void Win();
    }

    public interface IMechanics
    {
        float deltaTime { get; }

        bool rawAxis { get; }

        bool render { get; }

        ICollision collision { get; }

        IMap map { get; }

        IUnits meta { get; }
    }

    public interface IUnits
    {
        int frag2Coin { get; }

        float unitRadius { get; }

        float stopDistance { get; }

        LayerMask moveGround { get; }

        LayerMask moveFly { get; }

        LayerMask hit { get; }

        LayerMask overlap { get; }

        LayerMask door { get; }

        UnitMeta Player { get; }

        BotMeta LandBot { get; }

        BotMeta FlyBot { get; }

        Bullet bullet { get; }
    }

    public interface ICollision
    {
        Entity this[Collider collider] { get; }

        ICube GetCube(Entity obj);
        IPlane GetPlane(Entity obj);
        ISphere GetSphere(Entity obj);
    }

    public interface IViews
    {
        object GetModel(string name);

        IFormBind form { get; }
    }

    public interface IGizmos
    {
        bool level { get; }

        bool units { get; }

        bool projectiles { get; }
    }
}
