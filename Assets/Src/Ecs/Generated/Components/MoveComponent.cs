using System.Collections.Generic;

//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs
{
    public partial class Entity
	{
		public Game.Move move { get; private set; }

        public bool hasMove { get { return move != null; } }

        public void setMove(System.Single speed, UnityEngine.Vector3 direction)
        {
			if (move == null)
				move = pool.move.Get();

			move.speed = speed;
			move.direction = direction;

			react.On(move);
        }

		private void addListener(Listener<Game.Move> obj)
		{
			if (obj == null) return;

			react.Add(obj);

			if (move != null) obj.On(move);
		}

		public void unsetMove(bool needReact = true) 
		{
			remove(move, pool.move);
			move = null;

			if (needReact)
				react.On(move);
		}
	}

	public partial class EntityListener
	{
		private List<Listener<Game.Move>> onMove = new List<Listener<Game.Move>>();

		public void Add(Listener<Game.Move> obj)
		{
			onMove.Add(obj);
		}

		public void On(Game.Move move)
		{
			foreach(var obj in onMove) obj.On(move);
		}
	}
}