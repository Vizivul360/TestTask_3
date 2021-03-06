using System.Collections.Generic;

//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs
{
    public partial class Entity
	{
		public Game.AI ai { get; private set; }

        public bool hasAI { get { return ai != null; } }

        public void setAI(System.Single idleTime, System.Single walkDist, System.Single walkSpeed)
        {
			if (ai == null)
				ai = pool.ai.Get();

			ai.idleTime = idleTime;
			ai.walkDist = walkDist;
			ai.walkSpeed = walkSpeed;

			react.On(ai);
        }

		private void addListener(Listener<Game.AI> obj)
		{
			if (obj == null) return;

			react.Add(obj);

			if (ai != null) obj.On(ai);
		}

		public void unsetAI(bool needReact = true) 
		{
			remove(ai, pool.ai);
			ai = null;

			if (needReact)
				react.On(ai);
		}
	}

	public partial class EntityListener
	{
		private List<Listener<Game.AI>> onAI = new List<Listener<Game.AI>>();

		public void Add(Listener<Game.AI> obj)
		{
			onAI.Add(obj);
		}

		public void On(Game.AI ai)
		{
			foreach(var obj in onAI) obj.On(ai);
		}
	}
}