using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeLogic
{
	public class Point
	{
		public int X;
		public int Y;
		public Point()
		{
		}
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		//public static implicit operator System.Drawing.Point(Point p)
		//{
		//	return new System.Drawing.Point(p.X, p.Y);
		//}
	}

	public enum Direction
	{
		Up, Down, Left, Right
	}

	public class SnakeModel
	{
		public LinkedList<Point> Body = new LinkedList<Point>();

		private Direction direction;
		private Arena arena;

		private int growth = 2;

		Point delta = new Point();

		public SnakeModel(Arena arena)
		{
			Body.AddLast(new Point(1, 1));

			this.arena = arena;
			ChangeDirection(Direction.Right);
		}

		public void Move()
		{
			Point newHead = GetNewHead();

			if (arena.Cells[newHead.X, newHead.Y] == Food.Apple)
			{
				growth += 2;
			}
			else if (arena.Cells[newHead.X, newHead.Y] == Food.Orange)
			{
				growth++;
			}
			arena.Cells[newHead.X, newHead.Y] = Food.None;

			if (growth > 0)
			{
				growth--;
			}
			else
			{
				Body.RemoveFirst();
			}
			Body.AddLast(newHead);
		}

		public void ChangeDirection(Direction newDirection)
		{
			switch (newDirection)
			{
				case Direction.Up:
					if (direction != Direction.Down)
					{
						delta.X = 0;
						delta.Y = -1;
						direction = newDirection;
					}
					break;

				case Direction.Down:
					if (direction != Direction.Up)
					{
						delta.X = 0;
						delta.Y = 1;
						direction = newDirection;
					}
					break;

				case Direction.Left:
					if (direction != Direction.Right)
					{
						delta.X = -1;
						delta.Y = 0;
						direction = newDirection;
					}
					break;

				case Direction.Right:
					if (direction != Direction.Left)
					{
						delta.X = 1;
						delta.Y = 0;
						direction = newDirection;
					}
					break;
			}
		}

		private Point GetNewHead()
		{
			Point head = Body.Last.Value;

			int newX = (head.X + delta.X) % arena.Width;
			if (newX < 0)
			{
				newX += arena.Width;
			}

			int newY = (head.Y + delta.Y) % arena.Height;
			if (newY < 0)
			{
				newY += arena.Height;
			}

			Point newHead = new Point(newX, newY);

			return newHead;
		}

		public int[,] GetData()
		{
			int[,] data = new int[arena.Width, arena.Height];
			var current = Body.First;
            while (current != null)
            {
				data[current.Value.X, current.Value.Y] = 1;
                current = current.Next;
			}
			return data;
		}
	}
}
