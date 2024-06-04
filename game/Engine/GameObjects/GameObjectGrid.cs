using Blok3Game.Engine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blok3Game.Engine.GameObjects
{
	public class GameObjectGrid : GameObject
	{
		private GameObject[,] grid;
		protected int cellWidth = 1, cellHeight = 1;

		public GameObjectGrid(int rows, int columns, int layer = 0, string id = "")
			: base(layer, id)
		{
			grid = new GameObject[columns, rows];
			for (int x = 0; x < columns; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					grid[x, y] = null;
				}
			}
		}

		/// <summary>
		/// Adds the given GameObject to the grid in world space, the index multiplied by the size of the cell. If the position is outside the grid, nothing happens.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public virtual void Add(GameObject obj, int x, int y)
		{
			grid[x, y] = obj;
			obj.Parent = this;
			obj.Position = new Vector2(x * cellWidth, y * cellHeight);
		}

		/// <summary>
		/// Returns the GameObject at the given position in the grid. If the position is outside the grid, returns null.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public virtual GameObject Get(int x, int y)
		{
			if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
			{
				return grid[x, y];
			}

			return null;
		}

		public virtual void Set(int x, int y, GameObject obj)
		{
			if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
			{
				grid[x, y] = obj;
			} else {
				throw new System.Exception("Index out of bounds");
			}
		}

		/// <summary>
		/// Returns the position of the anchor of the given GameObject in the grid. If the GameObject is not in the grid, returns (0, 0).
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public Vector2 GetAnchorPosition(GameObject s)
		{
			for (int x = 0; x < Columns; x++)
			{
				for (int y = 0; y < Rows; y++)
				{
					if (grid[x, y] == s)
					{
						return new Vector2(x * cellWidth, y * cellHeight);
					}
				}
			}
			return Vector2.Zero;
		}
		
		/// <summary>
		/// Returns the index of the given GameObject in the grid. If the GameObject is not in the grid, returns (-1, -1).
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public Vector2 GetIndex(GameObject gameObject)
		{
			for (int x = 0; x < Columns; x++)
			{
				for (int y = 0; y < Rows; y++)
				{
					if (grid[x, y] == gameObject)
					{
						return new Vector2(x, y);
					}
				}
			}
			return new Vector2(-1, -1);
		}

		public int Rows
		{
			get { return grid.GetLength(1); }
		}

		public int Columns
		{
			get { return grid.GetLength(0); }
		}

		public int CellWidth
		{
			get { return cellWidth; }
			set { cellWidth = value; }
		}

		public int CellHeight
		{
			get { return cellHeight; }
			set { cellHeight = value; }
		}

		public override void HandleInput(InputHelper inputHelper)
		{
			base.HandleInput(inputHelper);
			
			foreach (GameObject obj in grid)
			{
				obj.HandleInput(inputHelper);
			}
		}

		public override void Update(GameTime gameTime)
		{
			foreach (GameObject obj in grid)
			{
				obj.Update(gameTime);
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			foreach (GameObject obj in grid)
			{
				obj.Draw(gameTime, spriteBatch);
			}
		}

		public override void Reset()
		{
			base.Reset();
			foreach (GameObject obj in grid)
			{
				obj.Reset();
			}
		}
	}
}