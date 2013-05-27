using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorMatch.GameEngine
{
	public class Board : IEquatable<Board>
	{
		private int _boardWidth, _boardHeight;
		private int[,] _boardMap;

		public List<int[,]> _undoList;
		public int MoveCount
		{
			get { return _undoList.Count; }
		}

		public int[,] BoardMap
		{
			get { return _boardMap; }
		}

		public Board(int boardHeight, int boardWidth)
		{
			_boardWidth = boardWidth;
			_boardHeight = boardHeight;
			_boardMap = new int[boardHeight,boardWidth];
			_undoList = new List<int[,]>();
		}

		public void SetBoard(int[,] boardMap)
		{
			if (boardMap == null) 
				throw new ArgumentNullException("boardMap");
			if (boardMap.Length != _boardMap.Length)
				throw new ArgumentException("illegal board size");

			_boardMap = (int[,])boardMap.Clone();
		}

		public void MovePiece(int x, int y, MoveDirection direction)
		{
			ValidateCoordinate(x, y);

			if (_boardMap[y, x] == (int)BoardPiece.Empty)
			{
				_undoList.Add((int[,])_boardMap.Clone());
				return;
			}

			switch (direction)
			{
				case MoveDirection.Down:
					if (y == 0) throw new IllegalMoveException();
					SwapPositions(x, y, x, y-1);
					break;
				case MoveDirection.Right:
					if (x + 1 > _boardWidth) throw new IllegalMoveException();
					SwapPositions(x, y, x+1, y);
					break;
				case MoveDirection.Left:
					if (x == 0) throw new IllegalMoveException();
					SwapPositions(x, y, x-1, y);
					break;
				case MoveDirection.Up:
					if (y + 1 > _boardHeight) throw new IllegalMoveException();
					// TODO: think of the right way to not do this move, but let the caller know we didn't really do a move
					//if (_boardMap[x, y + 1] == (int)BoardPiece.Empty) return false;
					SwapPositions(x, y, x, y+1);
					break;
			}

			StraightenOut();
		}

		private void StraightenOut()
		{
			DropPieces();

			while (ClearMatches())
				DropPieces();
		}

		private void DropPieces()
		{
			var dropped = true;

			while (dropped)
			{
				dropped = false;
				for (var i = 1; i < _boardHeight; i++)
				{
					for (var j = 0; j < _boardWidth; j++)
					{
						if (_boardMap[i, j] != (int) BoardPiece.Empty && _boardMap[i - 1, j] == 0)
						{
							var heightToDrop = i - 1;
							while (heightToDrop >= 0 && _boardMap[heightToDrop, j] == 0)
							{
								heightToDrop--;
							}

							_boardMap[heightToDrop + 1, j] = _boardMap[i, j];
							_boardMap[i, j] = (int) BoardPiece.Empty;

							dropped = true;
						}
					}
				}
			}
		}

		private bool ClearMatches()
		{
			var clearList = new List<Tuple<int, int>>();

			#region Looking for sets of 3+ in rows (Ugly Code)
			for (var i = 0; i < _boardHeight; i++)
			{
				var count = 1;
				var prevPiece = -1;
				for (var j = 0; j < _boardWidth; j++)
				{
					if (prevPiece == -1)
					{
						prevPiece = _boardMap[i, j];
						continue;
					}

					if (_boardMap[i,j] != (int)BoardPiece.Empty && _boardMap[i, j] == prevPiece)
					{
						count++;
					}
					else
					{
						if (count >= 3)
						{
							for (var t=j-1; t>(j-1-count); t--)
								clearList.Add(new Tuple<int, int>(i, t));
						}

						count = 1;
						prevPiece = _boardMap[i, j];
					}
				}

				if (count >= 3)
				{
					for (var t = _boardWidth-1; t > (_boardWidth-1 - count); t--)
						clearList.Add(new Tuple<int, int>(i, t));
				}
			}
			#endregion

			#region Looking for sets of 3+ in columns (Ugly Code)
			for (var i = 0; i < _boardWidth; i++)
			{
				var count = 1;
				var prevPiece = -1;
				for (var j = 0; j < _boardHeight; j++)
				{
					if (prevPiece == -1)
					{
						prevPiece = _boardMap[j, i];
						continue;
					}

					if (_boardMap[j, i] != (int)BoardPiece.Empty && _boardMap[j, i] == prevPiece)
					{
						count++;
					}
					else
					{
						if (count >= 3)
						{
							for (var t = j - 1; t > (j - 1 - count); t--)
								clearList.Add(new Tuple<int, int>(t, i));
						}

						count = 1;
						prevPiece = _boardMap[j, i];
					}
				}

				if (count >= 3)
				{
					for (var t = _boardHeight - 1; t > (_boardHeight - 1 - count); t--)
						clearList.Add(new Tuple<int, int>(t, i));
				}
			}
			#endregion

			if (clearList.Count == 0)
				return false;

			foreach (var piece in clearList)
			{
				_boardMap[piece.Item1, piece.Item2] = (int) BoardPiece.Empty;
			}

			return true;
		}

		public bool IsBoardClear()
		{
			for (var i = 0; i < _boardHeight; i++)
				for (var j = 0; j < _boardWidth; j++)
					if (_boardMap[i, j] != (int)BoardPiece.Empty)
						return false;

			return true;
		}

		public void UndoMove()
		{
			if (_undoList == null || _undoList.Count == 0)
				return;

			SetBoard(_undoList.Last());
			_undoList.Remove(_undoList.Last());
		}

		public bool Equals(Board other)
		{
			if (other._boardHeight != _boardHeight || other._boardWidth != _boardWidth)
				return false;

			for (var i = 0; i < _boardHeight; i++)
				for (var j = 0; j < _boardWidth; j++)
					if (_boardMap[i,j] != other._boardMap[i,j])
						return false;

			return true;
		}

		private void ValidateCoordinate(int x, int y)
		{
			if (x > _boardWidth || x < 0 || y > _boardHeight || y < 0) 
				throw new IllegalMoveException();
		}

		private void SwapPositions(int x1, int y1, int x2, int y2)
		{
			_undoList.Add((int[,])_boardMap.Clone());

			try
			{
				var tempBoardPiece = _boardMap[y1, x1];
				_boardMap[y1, x1] = _boardMap[y2, x2];
				_boardMap[y2, x2] = tempBoardPiece;
			}
			catch (Exception)
			{
				_undoList.Remove(_undoList.Last());
				throw;
			}
		}
	}
}