using System.Collections.Generic;

//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs
{
    public partial class Entity
	{
		public Game.Attack attack { get; private set; }

        public bool hasAttack { get { return attack != null; } }

        public void setAttack(System.Single dmg, System.Single speed)
        {
			if (attack == null)
				attack = pool.attack.Get();

			attack.dmg = dmg;
			attack.speed = speed;

			react.On(attack);
        }

		private void addListener(Listener<Game.Attack> obj)
		{
			if (obj == null) return;

			react.Add(obj);

			if (attack != null) obj.On(attack);
		}

		public void unsetAttack(bool needReact = true) 
		{
			remove(attack, pool.attack);
			attack = null;

			if (needReact)
				react.On(attack);
		}
	}

	public partial class EntityListener
	{
		private List<Listener<Game.Attack>> onAttack = new List<Listener<Game.Attack>>();

		public void Add(Listener<Game.Attack> obj)
		{
			onAttack.Add(obj);
		}

		public void On(Game.Attack attack)
		{
			foreach(var obj in onAttack) obj.On(attack);
		}
	}
}