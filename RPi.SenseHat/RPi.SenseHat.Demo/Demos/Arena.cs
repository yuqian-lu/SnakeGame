using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace SnakeLogic
{
	public enum Food
	{
		None = 0,
		Apple,
		Orange
	}

	public class Arena
	{
		static object _locker = new object();
		public int Width { get; private set; }
		public int Height { get; private set; }

		public SnakeModel Snake { get; set; }

		public Food[,] Cells { get; private set; }
		private Random random = new Random();

		public Arena(int width, int height)
		{
			Width = width;
			Height = height;

			Cells = new Food[width, height];

			Snake = new SnakeModel(this);

		}

		/// <summary>
		/// Interface 1: called by a timer to move
		/// </summary>
		public void Update()
		{
			lock (_locker)
			{
				Snake.Move();
				if (random.Next(10) <= 4)
				{
					CreateFood();
				}
			}
		}

		/// <summary>
		/// Interface 1.5: GET data of area, and the 
		/// </summary>
		/// <returns></returns>
		public Tuple<int[,], Point> GetData()
		{
			Point foodPoint = null;
			for (int i = 0; i < Width;i++ )
				for (int j = 0; j < Height; j++)
			{
				if (Cells[i, j] != Food.None)
				{
					foodPoint = new Point(i, j);
					break;
				}
			}

			return new Tuple<int[,],Point>(Snake.GetData(), foodPoint);
		}

		/// <summary>
		/// Interface 2: change direction
		/// </summary>
		public void ChangeDirection(Direction direction)
		{
			Snake.ChangeDirection(direction);
		}

		private void CreateFood()
		{
			foreach (var cell in Cells)
			{
				if (cell != Food.None)
					return;
			}

			//Cells[random.Next(0, Width), random.Next(0, Height)] = (Food)random.Next(1, 3);
			Cells[random.Next(0, Width), random.Next(0, Height)] = Food.Orange;
		}
	}
}
