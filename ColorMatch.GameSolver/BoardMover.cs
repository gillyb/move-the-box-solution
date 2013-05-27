using System;
using System.Collections;
using System.Collections.Generic;
using ColorMatch.GameEngine;

namespace ColorMatch.GameSolver
{
	public class BoardMover : IEnumerable<GameMove>
	{
		IEnumerator<GameMove> IEnumerable<GameMove>.GetEnumerator()
		{
			return new BoardMoverEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return new BoardMoverEnumerator();
		}
	}

	public class BoardMoverEnumerator : IEnumerator<GameMove>
	{
		private int currentTile = 0;
		private int currentDirection = 0;

		public void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public bool MoveNext()
		{
			if (currentDirection < 4)
			{
				currentDirection++;
			}
			else
			{
				currentDirection = 0;
				currentTile++;
			}

			return currentTile <= 56;
		}

		public void Reset()
		{
			throw new System.NotImplementedException();
		}

		public GameMove Current
		{
			get
			{
				var tileX = currentTile % 7;
				var tileY = (int)Math.Floor((double)currentTile / 7);
				return new GameMove{Direction = GetDirection(currentDirection), X = tileX, Y = tileY};
			}
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		private MoveDirection GetDirection(int d)
		{
			if (d % 4 == 0) return MoveDirection.Up;
			if (d % 4 == 1) return MoveDirection.Right;
			if (d % 4 == 2) return MoveDirection.Down;
			if (d % 4 == 3) return MoveDirection.Left;
			throw new Exception("Something's fucked up!");
		}
	}
}