
//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs.Pool
{
    public partial class EntityPool : IPools
    {
		public IPool<Game.AI> ai { get; private set; }
		public IPool<Game.Attack> attack { get; private set; }
		public IPool<Game.BotIdle> botidle { get; private set; }
		public IPool<Game.BotWalk> botwalk { get; private set; }
		public IPool<Game.Cell> cell { get; private set; }
		public IPool<Game.Coin> coin { get; private set; }
		public IPool<Game.Cooldown> cooldown { get; private set; }
		public IPool<Game.Damage> damage { get; private set; }
		public IPool<Game.Door> door { get; private set; }
		public IPool<Game.Health> health { get; private set; }
		public IPool<Game.Hit> hit { get; private set; }
		public IPool<Game.Identity> identity { get; private set; }
		public IPool<Game.Look> look { get; private set; }
		public IPool<Game.Model> model { get; private set; }
		public IPool<Game.Move> move { get; private set; }
		public IPool<Game.Position> position { get; private set; }

		partial void createPools()
		{
			ai = new AIPool();
			attack = new AttackPool();
			botidle = new BotIdlePool();
			botwalk = new BotWalkPool();
			cell = new CellPool();
			coin = new CoinPool();
			cooldown = new CooldownPool();
			damage = new DamagePool();
			door = new DoorPool();
			health = new HealthPool();
			hit = new HitPool();
			identity = new IdentityPool();
			look = new LookPool();
			model = new ModelPool();
			move = new MovePool();
			position = new PositionPool();
		}

		partial void setPool(Entity entity)
		{
			entity.pool = this;
		}
    }

	public interface IPools
	{
		IPool<Game.AI> ai { get; }
		IPool<Game.Attack> attack { get; }
		IPool<Game.BotIdle> botidle { get; }
		IPool<Game.BotWalk> botwalk { get; }
		IPool<Game.Cell> cell { get; }
		IPool<Game.Coin> coin { get; }
		IPool<Game.Cooldown> cooldown { get; }
		IPool<Game.Damage> damage { get; }
		IPool<Game.Door> door { get; }
		IPool<Game.Health> health { get; }
		IPool<Game.Hit> hit { get; }
		IPool<Game.Identity> identity { get; }
		IPool<Game.Look> look { get; }
		IPool<Game.Model> model { get; }
		IPool<Game.Move> move { get; }
		IPool<Game.Position> position { get; }
	}
}