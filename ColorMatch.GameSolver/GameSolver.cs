using System;
using System.Collections.Generic;
using System.Linq;
using ColorMatch.GameEngine;

namespace ColorMatch.GameSolver
{
	public class GameSolver
	{
		private readonly Board _board;
		private const int _boardWidth = 7;
		private const int _boardHeight = 8;

		private readonly int[,] boardMap;

		private const int NumberOfMoves = 2;

		private static List<GameMove> moveList;

		public static void Main(string[] args)
		{
			var solver = new GameSolver();
			solver.Solve();
		}

		public GameSolver()
		{
			_board = new Board(_boardHeight, _boardWidth);
			//boardMap = new[,]{ // Osaka 2 // Works
			//    { 0, (int) BoardPiece.Brown, (int) BoardPiece.Blue, (int) BoardPiece.Brown, (int) BoardPiece.Blue,(int) BoardPiece.Blue, 0},
			//    { 0, (int) BoardPiece.Brown, (int) BoardPiece.Yellow, (int) BoardPiece.Brown, (int) BoardPiece.Yellow, (int) BoardPiece.Blue, 0},
			//    {0, 0, (int) BoardPiece.Brown, (int) BoardPiece.Yellow, (int) BoardPiece.Blue, 0, 0},
			//    {0, 0, (int) BoardPiece.Yellow, (int) BoardPiece.Blue, (int) BoardPiece.Yellow, 0, 0},
			//    {0, 0, (int) BoardPiece.Red, (int) BoardPiece.Brown, (int) BoardPiece.Red, 0, 0},
			//    {0, 0, 0, (int) BoardPiece.Yellow, 0, 0, 0},
			//    {0, 0, 0, (int) BoardPiece.Red, 0, 0, 0},
			//    {0, 0, 0, 0, 0, 0, 0}
			//};

			//boardMap = new[,]{ // Hamburg 15
			//    {0, 0, (int)BoardPiece.Brown, (int)BoardPiece.Brown, (int)BoardPiece.Yellow, (int)BoardPiece.Yellow, 0},
			//    {0, 0, (int)BoardPiece.Blue, (int)BoardPiece.Brown, (int)BoardPiece.Brown, 0, 0},
			//    {0, 0, (int)BoardPiece.Blue, (int)BoardPiece.Green, (int)BoardPiece.Green, 0, 0},
			//    {0, 0, (int)BoardPiece.Yellow, (int)BoardPiece.Yellow, (int)BoardPiece.Green, 0, 0},
			//    {0, 0, (int)BoardPiece.Blue, (int)BoardPiece.Green, (int)BoardPiece.Yellow, 0, 0},
			//    {0, 0, 0, (int)BoardPiece.Green, (int)BoardPiece.Green, 0, 0},
			//    {0, 0, 0, (int)BoardPiece.Brown, (int)BoardPiece.Yellow, 0, 0},
			//    {0, 0, 0, (int)BoardPiece.Brown, 0, 0, 0}
			//};

			boardMap = new[,]{ // Osaka 10
			    {0, (int)BoardPiece.Blue, (int)BoardPiece.Red, (int)BoardPiece.Red, (int)BoardPiece.Brown, 0, 0},
			    {0, 0, (int)BoardPiece.Blue, (int)BoardPiece.Red, (int)BoardPiece.Blue, 0, 0},
			    {0, 0, (int)BoardPiece.Green, (int)BoardPiece.Brown, (int)BoardPiece.Brown, 0, 0},
			    {0, 0, 0, (int)BoardPiece.Green, (int)BoardPiece.Green, 0, 0},
			    {0, 0, 0, 0, 0, 0, 0},
			    {0, 0, 0, 0, 0, 0, 0},
			    {0, 0, 0, 0, 0, 0, 0},
			    {0, 0, 0, 0, 0, 0, 0}
			};

			//boardMap = new[,] { // Osaka 22
			//    {0,0,(int)BoardPiece.Brown,(int)BoardPiece.Green,(int)BoardPiece.Green,0,0},
			//    {0,0,(int)BoardPiece.Yellow,(int)BoardPiece.Brown,(int)BoardPiece.Green,0,0},
			//    {0,0,(int)BoardPiece.Blue,(int)BoardPiece.Blue,(int)BoardPiece.Brown,0,0},
			//    {0,0,(int)BoardPiece.Yellow,(int)BoardPiece.Green,(int)BoardPiece.Brown,0,0},
			//    {0,0,(int)BoardPiece.Brown,(int)BoardPiece.Brown,(int)BoardPiece.Blue,0,0},
			//    {0,0,(int)BoardPiece.Yellow,(int)BoardPiece.Green,(int)BoardPiece.Green,0,0},
			//    {0,0,0,0,0,0,0},
			//    {0,0,0,0,0,0,0}
			//};

			//boardMap = new[,] { // Osaka 24
			//    {0,(int)BoardPiece.Brown,(int)BoardPiece.Green,(int)BoardPiece.Brown,(int)BoardPiece.Red,(int)BoardPiece.Red,0},
			//    {0,(int)BoardPiece.Blue,(int)BoardPiece.Green,(int)BoardPiece.Red,(int)BoardPiece.Green,(int)BoardPiece.Yellow,0},
			//    {0,0,(int)BoardPiece.Brown,(int)BoardPiece.Blue,(int)BoardPiece.Yellow,0,0},
			//    {0,0,(int)BoardPiece.Green,(int)BoardPiece.Yellow,(int)BoardPiece.Green,0,0},
			//    {0,0,0,(int)BoardPiece.Blue,0,0,0},
			//    {0,0,0,(int)BoardPiece.Green,0,0,0},
			//    {0,0,0,0,0,0,0},
			//    {0,0,0,0,0,0,0}
			//};

			_board.SetBoard(boardMap);
		}

		public List<GameMove> Solve()
		{
			moveList = new List<GameMove>();
			BoardEnumeration();
			return moveList;
		}

		private void BoardEnumeration()
		{
			if (_board.IsBoardClear()) return;

			if (_board.MoveCount >= NumberOfMoves)
			{
				_board.UndoMove();
				moveList.Remove(moveList.Last());
				return;
			}

			var enumerator = new GameMoveEnumerator();
			while (enumerator.MoveNext() && !_board.IsBoardClear())
			{
				var gameMove = (GameMove) enumerator.Current;
				try
				{
					_board.MovePiece(gameMove.X, gameMove.Y, gameMove.Direction);
					
					moveList.Add(new GameMove { X = gameMove.X, Y = gameMove.Y, Direction = gameMove.Direction });

					if (_board.IsBoardClear()) return;

					BoardEnumeration();

					if (_board.IsBoardClear()) return;
				}
				catch (Exception)
				{
				}
			}

			if (_board.IsBoardClear()) return;

			moveList.Remove(moveList.Last());
			_board.UndoMove();

			return;
		}
	}
}
