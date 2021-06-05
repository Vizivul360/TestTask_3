using Ecs.Systems;
using Models;
using Ecs;

namespace Game
{
    public class ViewSystem : Initializer, Executor, Selector
    {
        private Group models, hits;
        private IViews src;

        public ViewSystem(Context context, IViews views)
        {
            models = context.GetGroup(this);
            hits = context.GetGroup(new Hits());
            src = views;
        }

        public void Init()
        {
            foreach (var obj in models.Select())
            {
                var model = addModel(obj);
                if (obj.hasCell) (model as ICell).setSize(obj.cell.size);

                if (obj.hasHealth) obj.AddListener(src.form.GetHpBar());

                if (obj.hasCoin) obj.AddListener(src.form);
            }
        }

        public void Exec()
        {
            foreach (var obj in models.Select())
                addModel(obj);

            showHits();
        }

        private void showHits()
        {
            foreach (var obj in hits.Select())
            {
                var hit = obj.hit;

                if (hit.empty)
                {
                    obj.AddListener(src.GetModel("hitFx"));
                }
                else
                {
                    var text = src.form.GetHitText();
                    obj.AddListener(text);
                }

                if (hit.kill)
                {
                    obj.AddListener(src.GetModel("killFx"));
                }
            }
        }

        public bool check(Entity obj)
        {
            return obj.hasModel && obj.model.created == false;
        }

        private object addModel(Entity obj)
        {
            var model = src.GetModel(obj.model);

            obj.AddListener(model);
            obj.ModelCreated();

            return model;
        }
    }
}
