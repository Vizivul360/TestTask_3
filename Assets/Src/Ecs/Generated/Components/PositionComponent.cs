using System.Collections.Generic;

//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs
{
    public partial class Entity
	{
		public Game.Position position { get; private set; }

        public bool hasPosition { get { return position != null; } }

        public void setPosition(UnityEngine.Vector3 value)
        {
			if (position == null)
				position = pool.position.Get();

			position.value = value;

			react.On(position);
        }

		private void addListener(Listener<Game.Position> obj)
		{
			if (obj == null) return;

			react.Add(obj);

			if (position != null) obj.On(position);
		}

		public void unsetPosition(bool needReact = true) 
		{
			remove(position, pool.position);
			position = null;

			if (needReact)
				react.On(position);
		}
	}

	public partial class EntityListener
	{
		private List<Listener<Game.Position>> onPosition = new List<Listener<Game.Position>>();

		public void Add(Listener<Game.Position> obj)
		{
			onPosition.Add(obj);
		}

		public void On(Game.Position position)
		{
			foreach(var obj in onPosition) obj.On(position);
		}
	}
}