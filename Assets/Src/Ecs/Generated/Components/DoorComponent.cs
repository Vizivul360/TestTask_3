using System.Collections.Generic;

//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs
{
    public partial class Entity
	{
		public Game.Door door { get; private set; }

        public bool hasDoor { get { return door != null; } }

        public void setDoor(System.Boolean isOpen)
        {
			if (door == null)
				door = pool.door.Get();

			door.isOpen = isOpen;

			react.On(door);
        }

		private void addListener(Listener<Game.Door> obj)
		{
			if (obj == null) return;

			react.Add(obj);

			if (door != null) obj.On(door);
		}

		public void unsetDoor(bool needReact = true) 
		{
			remove(door, pool.door);
			door = null;

			if (needReact)
				react.On(door);
		}
	}

	public partial class EntityListener
	{
		private List<Listener<Game.Door>> onDoor = new List<Listener<Game.Door>>();

		public void Add(Listener<Game.Door> obj)
		{
			onDoor.Add(obj);
		}

		public void On(Game.Door door)
		{
			foreach(var obj in onDoor) obj.On(door);
		}
	}
}