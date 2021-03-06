using System.Collections.Generic;

//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs
{
    public partial class Entity
	{
		public Game.Model model { get; private set; }

        public bool hasModel { get { return model != null; } }

        public void setModel(System.String name, System.Boolean created)
        {
			if (model == null)
				model = pool.model.Get();

			model.name = name;
			model.created = created;

			react.On(model);
        }

		private void addListener(Listener<Game.Model> obj)
		{
			if (obj == null) return;

			react.Add(obj);

			if (model != null) obj.On(model);
		}

		public void unsetModel(bool needReact = true) 
		{
			remove(model, pool.model);
			model = null;

			if (needReact)
				react.On(model);
		}
	}

	public partial class EntityListener
	{
		private List<Listener<Game.Model>> onModel = new List<Listener<Game.Model>>();

		public void Add(Listener<Game.Model> obj)
		{
			onModel.Add(obj);
		}

		public void On(Game.Model model)
		{
			foreach(var obj in onModel) obj.On(model);
		}
	}
}