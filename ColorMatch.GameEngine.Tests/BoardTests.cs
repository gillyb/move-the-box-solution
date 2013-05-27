using NUnit.Framework;

namespace ColorMatch.GameEngine.Tests
{
	[TestFixture]
	public class BoardTests
	{
		[Test]
		public void MovePiece_IllegalPosition_ThrowIllegalMoveException()
		{
			var board = new Board(4, 4);
			board.SetBoard(new[,] { { 0, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });

			Assert.Throws<IllegalMoveException>(() => board.MovePiece(11, 2, MoveDirection.Up));
			Assert.Throws<IllegalMoveException>(() => board.MovePiece(2, 5, MoveDirection.Down));
			Assert.Throws<IllegalMoveException>(() => board.MovePiece(-1, 2, MoveDirection.Right));
			Assert.Throws<IllegalMoveException>(() => board.MovePiece(0, -3, MoveDirection.Left));
			Assert.Throws<IllegalMoveException>(() => board.MovePiece(1, 0, MoveDirection.Down));
		}

		[Test]
		public void MovePiece_IllogicalPosition_BoardStaysTheSame()
		{
			var expected = new Board(4, 4);
			expected.SetBoard(new[,]{{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 1, 1, 0}});

			var board = new Board(4, 4);
			board.SetBoard(new [,]{{ 0, 0, 0, 0 },{ 0, 0, 0, 0 },{ 0, 0, 0, 0 },{ 0, 1, 1, 0 }});

			board.MovePiece(1, 0, MoveDirection.Up);
			Assert.IsTrue(board.Equals(expected));
		}

		[Test]
		public void MovePiece_SwapUp_PositionsChange()
		{
			var expected = new Board(4, 4);
			expected.SetBoard(new[,] { { 0, (int)BoardPiece.Green, (int)BoardPiece.Blue, 0 }, { 0, (int)BoardPiece.Red, 0, 0 }, { 0, (int)BoardPiece.Brown, 0, 0 }, { 0, 0, 0, 0 } });

			var board = new Board(4, 4);
			board.SetBoard(new[,] { { 0, (int)BoardPiece.Red, (int)BoardPiece.Blue, 0 }, { 0, (int)BoardPiece.Green, 0, 0 }, { 0, (int)BoardPiece.Brown, 0, 0 }, { 0, 0, 0, 0 } });

			board.MovePiece(1, 0, MoveDirection.Up);
			Assert.AreEqual(board, expected);
		}

		[Test]
		public void MovePiece_SwapRight_PositionsChange()
		{
			var expected = new Board(4, 4);
			expected.SetBoard(new[,] { { 0, (int)BoardPiece.Yellow, (int)BoardPiece.Red, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });

			var board = new Board(4, 4);
			board.SetBoard(new[,] { { 0, (int)BoardPiece.Red, (int)BoardPiece.Yellow, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });

			board.MovePiece(1, 0, MoveDirection.Right);
			Assert.AreEqual(board, expected);
		}

		[Test]
		public void MovePiece_SwapLeft_PositionsChange()
		{
			var expected = new Board(4, 4);
			expected.SetBoard(new[,] { { 0, (int)BoardPiece.Yellow, (int)BoardPiece.Red, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });

			var board = new Board(4, 4);
			board.SetBoard(new[,] { { 0, (int)BoardPiece.Red, (int)BoardPiece.Yellow, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });

			board.MovePiece(2, 0, MoveDirection.Left);
			Assert.AreEqual(board, expected);
		}

		[Test]
		public void MovePiece_SwapDown_PositionsChange()
		{
			var expected = new Board(4, 4);
			expected.SetBoard(new[,] { { 0, (int)BoardPiece.Yellow, (int)BoardPiece.Red, 0 }, { 0, (int)BoardPiece.Blue, 0, 0 }, { 0, (int)BoardPiece.Green, 0, 0 }, { 0, 0, 0, 0 } });

			var board = new Board(4, 4);
			board.SetBoard(new[,] { { 0, (int)BoardPiece.Blue, (int)BoardPiece.Red, 0 }, { 0, (int)BoardPiece.Yellow, 0, 0 }, { 0, (int)BoardPiece.Green, 0, 0 }, { 0, 0, 0, 0 } });

			board.MovePiece(1, 1, MoveDirection.Down);
			Assert.AreEqual(board, expected);
		}

		[Test]
		public void StraightenOut_OnePieceToDrop_DroppedToFloor()
		{
			var expected = new Board(4, 4);
			expected.SetBoard(new[,] { { 0, (int)BoardPiece.Yellow, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });

			var board = new Board(4, 4);
			board.SetBoard(new[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, (int)BoardPiece.Yellow, 0, 0 } });

			Assert.AreEqual(board, expected);
		}

		[Test]
		public void StraightenOut_OnePieceToDrop_DropOnAnotherPiece()
		{
			var expected = new Board(4, 4);
			expected.SetBoard(new[,] { { (int)BoardPiece.Blue, (int)BoardPiece.Red, (int)BoardPiece.Green, (int)BoardPiece.Brown }, { 0, (int)BoardPiece.Yellow, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });

			var board = new Board(4, 4);
			board.SetBoard(new[,] { { (int)BoardPiece.Blue, (int)BoardPiece.Red, (int)BoardPiece.Green, (int)BoardPiece.Brown }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, (int)BoardPiece.Yellow, 0, 0 } });

			Assert.AreEqual(board, expected);
		}

		[Test]
		public void StraightenOut_ComplicatedScenario1_ClearedCorrectly()
		{
			var expected = new Board(5, 5);
			expected.SetBoard(new int[,] { { 0, 1, 2, 1, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });

			var board = new Board(5, 5);
			board.SetBoard(new int[,] { { 0, 1, 2, 2, 1 }, { 0, 0, 0, 2, 0 }, { 0, 0, 0, 2, 0 }, { 0, 0, 0, 2, 0 }, { 0, 0, 0, 1, 0 } });

			Assert.AreEqual(board, expected);
		}

		[Test]
		public void StraightenOut_ComplicatedScenario2_ClearedCorrectly()
		{
			var expected = new Board(5, 5);
			expected.SetBoard(new int[,] { { 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });

			var board = new Board(5, 5);
			board.SetBoard(new int[,] { { 0, 2, 2, 2, 1 }, { 0, 3, 3, 2, 0 }, { 0, 0, 0, 2, 0 }, { 0, 0, 0, 3, 0 }, { 0, 0, 0, 1, 0 } });

			Assert.AreEqual(board, expected);
		}

		[Test]
		public void StraightenOut_ComplicatedScenario3_ClearedCorrectly()
		{
			var expected = new Board(5, 5);
			expected.SetBoard(new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });

			var board = new Board(5, 5);
			board.SetBoard(new int[,] { { 0, 1, 0, 1, 1 }, { 0, 2, 0, 2, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 2, 0, 0 } });

			Assert.AreEqual(board, expected);
		}

		[Test]
		public void StraightenOut_ComplicatedScenario4_ClearedCorrectly()
		{
			var expected = new Board(5, 5);
			expected.SetBoard(new int[,] { { 0, 3, 2, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });

			var board = new Board(5, 5);
			board.SetBoard(new int[,] { { 3, 3, 2, 0, 0 }, { 1, 1, 0, 0, 0 }, { 3, 0, 0, 0, 0 }, { 3, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 } });

			Assert.AreEqual(board, expected);
		}

		[Test]
		public void UndoMove_AfterOneMove_BoardIsLikeBeginningPosition()
		{
			var expected = new Board(5, 5);
			expected.SetBoard(new int[,] { { 0, 1, 1, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });
			
			expected.MovePiece(2, 0, MoveDirection.Right);
			expected.UndoMove();

			Assert.AreEqual(expected, expected);
		}

		[Test]
		public void UndoMove_AfterTwoMovesGoBackOne_LikeAfterFirstMove()
		{
			var board = new Board(5, 5);
			board.SetBoard(new int[,] { { 0, 1, 1, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });

			var expected = new Board(5, 5);
			expected.SetBoard(new int[,] { { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });

			board.MovePiece(2, 0, MoveDirection.Right);
			board.MovePiece(3, 0, MoveDirection.Right);
			board.UndoMove();

			Assert.AreEqual(board, expected);
		}
	}
}
