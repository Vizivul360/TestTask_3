using UnityEngine;
using Ecs.Systems;
using Ecs;

namespace Game
{
    public class UnitSpawner : Initializer
    {
        const float SKIN = 0.01f;

        private Context context;

        private IMechanics src;        

        private IMap map { get { return src.map; } }
        private IUnits meta { get { return src.meta; } }
        private ICollision collision { get { return src.collision; } }

        private Collider[] buff;

        public UnitSpawner(Context context, IMechanics src)
        {
            this.context = context;
            this.src = src;

            buff = new Collider[5];
        }

        public void Init()
        {
            createPlayer();

            createBots();
        }

        private void createPlayer()
        {
            var obj = createUnit(UID.PLAYER, 0, meta.Player);
            var point = new Vector3(0, meta.unitRadius + SKIN, -map.size.y / 2 + 0.5f);

            obj.setPosition(point);
            obj.setModel("player");
            obj.setCoin(0);
        }

        private void createBots()
        {
            createBots(0, meta.LandBot, meta.unitRadius + SKIN);
            createBots(meta.LandBot.count, meta.FlyBot, 1 + meta.unitRadius);
        }

        private void createBots(int id_offset, BotMeta pars, float dy)
        {
            for(var i = 0; i < pars.count; i++)
            {
                var point = botPosition(dy);

                var bot = createUnit(UID.BOT + (id_offset + i), 1, pars.unit);
                bot.setPosition(point);
                bot.setModel("bot");

                var ai = pars.ai;

                bot.setAI(ai.idle, ai.walkDistance, pars.unit.moveSpeed);
                bot.setBotIdle(ai.idle);
            }
        }

        private Vector3 botPosition(float dy)
        {
            var r = meta.unitRadius;
            var size = map.size;

            var offset = -new Vector3(size.x - 1, 0, size.y - 1) / 2;
            offset += new Vector3(0, 0, size.y / 3);
            offset += new Vector3(r, 0, r);

            var dx = size.x - 2 * r;
            var dz = 2 * size.y / 3 - 2 * r;

            var p = Vector3.zero;

            do
            {
                p = new Vector3(Random.Range(0, dx), dy, Random.Range(0, dz)) + offset;
            }
            while (isFree(p, r) == false);

            return p;
        }

        private bool isFree(Vector3 p, float r)
        {
            return Physics.OverlapSphereNonAlloc(p, r, buff) == 0;
        }

        private Entity createUnit(string id, int side, UnitMeta pars)
        {
            var obj = context.CreateEntity();

            obj.setIdentity(id, side);
            obj.setHealth(pars.hp, pars.hp);

            obj.setAttack(pars.dmg, pars.attackSpeed);
            obj.setCooldown(0);

            var sphere = collision.GetSphere(obj);
            sphere.setRadius(meta.unitRadius);
            sphere.layer = LayerType.hitbox;

            obj.AddListener(sphere);

            return obj;
        }
    }

    public class UID
    {
        public const string PLAYER = "player", BOT = "bot_";
    }
}
