using Ecs;

namespace Game
{
    public class Model : IComponent
    {
        public string name;

        public bool created;

        public static implicit operator string(Model obj)
        {
            return obj == null ? "none" : obj.name;
        }
    }

    public static class ModelExt
    {
        public static void setModel(this Entity e, string name)
        {
            e.setModel(name, false);
        }

        public static void ModelCreated(this Entity e)
        {
            if (e.hasModel) e.model.created = true;
        }
    }
}
