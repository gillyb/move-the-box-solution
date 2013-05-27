using System;
using System.Collections.Generic;

namespace ColorMatch.GameEngine
{
	public class GameMoveEnumerator : IEnumerator<GameMove>
	{
		private int currentTile = 0;
		private MoveDirection currentDirection;
		private int moveIndication = 0;

		public bool MoveNext()
		{
			if (currentTile == 56)
				return false;

			if (moveIndication == 0)
			{
				currentDirection = MoveDirection.Up;
				moveIndication++;
			}
			else if (moveIndication == 1)
			{
				currentDirection = MoveDirection.Right;
				moveIndication++;
			}
			else if (moveIndication == 2)
			{
				currentDirection = MoveDirection.Down;
				moveIndication++;
			}
			else if (moveIndication == 3)
			{
				currentDirection = MoveDirection.Left;
				moveIndication++;
			}
			else
			{
				moveIndication = 0;
				currentTile++;
			}

			return true;
		}

		public void Reset()
		{
			currentTile = 0;
			currentDirection = MoveDirection.Up;
		}

		public object Current
		{
			get
			{
				return new GameMove {
				                    	X = currentTile % 7,
				                    	Y = (int)Math.Floor((double)currentTile / 7),
				                    	Direction = currentDirection
				                    };
			}
		}

		GameMove IEnumerator<GameMove>.Current
		{
			get
			{
				return new GameMove {
				                    	X = currentTile%7,
				                    	Y = (int) Math.Floor((double) currentTile/7),
				                    	Direction = currentDirection
				                    };
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}