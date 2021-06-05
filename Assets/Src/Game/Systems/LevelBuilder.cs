using UnityEngine;
using Ecs.Systems;
using Collisions;
using Ecs;

namespace Game
{
    public class LevelBuilder : Initializer
    {
        private Context context;

        private IMechanics src;

        private IMap map { get { return src.map; } }
        private ICollision collision { get { return src.collision; } }

        public LevelBuilder(Context context, IMechanics src)
        {
            this.context = context;
            this.src = src;
        }

        public void Init()
        {
            createInnerCells();

            createBorders();

            createFloor();

            createTop();

            createDoor();
        }

        #region -= cells =-
        private void createInnerCells()
        {
            var offset = -new Vector3(map.width - 1, 0, map.height - 1) / 2;

            for (var i = 0; i < map.height; i++)
                for (var j = 0; j < map.width; j++)
                    innerCell(map[j, i], new Vector3(j, 0.5f, i) + offset);
        }

        private void innerCell(LayerType layer, Vector3 point)
        {
            if (layer == LayerType.none) return;

            var obj = createCell(point, Vector3.one, layer);
            obj.setModel(layer.ToString());
        }
        #endregion

        #region -= borders =-
        private void createFloor()
        {
            var s = map.size + 2 * Vector2.one;
            var obj = border(new Vector3(0, -0.5f, 0), new Vector3(s.x, 1, s.y));

            obj.setModel("floor");
        }

        private void createTop()
        {
            var s = map.size + 2 * Vector2.one;
            border(2.5f * Vector3.up, new Vector3(s.x, 1, s.y));
        }

        private void createBorders()
        {
            var ds = map.size / 2;

            var vertical = new Vector3(1, 2, map.height + 1);
            var horizontal = new Vector3(map.width + 1, 2, 1);

            border(new Vector3(-ds.x - 0.5f, 1, 0.5f), vertical);
            border(new Vector3(ds.x + 0.5f, 1, -0.5f), vertical);

            border(new Vector3(0.5f, 1, ds.y + 0.5f), horizontal);
            border(new Vector3(-0.5f, 1, -ds.y - 0.5f), horizontal);
        }

        private Entity border(Vector3 point, Vector3 size)
        {
            return createCell(point, size, LayerType.border);
        }
        #endregion

        private void createDoor()
        {
            var p = new Vector3(0, 0.5f, map.size.y / 2 - 0.05f);
            var s = new Vector3(1, 1, 0.1f);

            var obj = createCell(p, s, LayerType.door);
            obj.setModel("door");
            obj.setDoor(false);
        }

        #region -= base =-
        private Entity createCell(Vector3 point, Vector3 size, LayerType type)
        {
            var obj = context.CreateEntity();
            obj.setCell(type, size);
            obj.setPosition(point);

            var bind = getBind(obj, size);
            bind.layer = type;

            obj.AddListener(bind);

            return obj;
        }

        private ColliderBind getBind(Entity entity, Vector3 size)
        {
            if (size.z > 0)
            {
                var cube = collision.GetCube(entity);
                cube.setSize(size);

                return cube;
            }
            else
            {
                var plane = collision.GetPlane(entity);
                plane.setSize(size);

                return plane;
            }
        }
        #endregion
    }
}
