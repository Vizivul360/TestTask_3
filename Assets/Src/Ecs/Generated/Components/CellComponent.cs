using System.Collections.Generic;

//DON'T MODIFY, AUTOGENERATED CODE HERE (25.02.2020 07:49)

namespace Ecs
{
    public partial class Entity
	{
		public Game.Cell cell { get; private set; }

        public bool hasCell { get { return cell != null; } }

        public void setCell(Game.LayerType type, UnityEngine.Vector3 size)
        {
			if (cell == null)
				cell = pool.cell.Get();

			cell.type = type;
			cell.size = size;

			react.On(cell);
        }

		private void addListener(Listener<Game.Cell> obj)
		{
			if (obj == null) return;

			react.Add(obj);

			if (cell != null) obj.On(cell);
		}

		public void unsetCell(bool needReact = true) 
		{
			remove(cell, pool.cell);
			cell = null;

			if (needReact)
				react.On(cell);
		}
	}

	public partial class EntityListener
	{
		private List<Listener<Game.Cell>> onCell = new List<Listener<Game.Cell>>();

		public void Add(Listener<Game.Cell> obj)
		{
			onCell.Add(obj);
		}

		public void On(Game.Cell cell)
		{
			foreach(var obj in onCell) obj.On(cell);
		}
	}
}