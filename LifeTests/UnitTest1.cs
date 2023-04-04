using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using cli_life;

namespace LifeTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void TestInitialBoard_AllDead()
        {
            var board = new Board(4, 4, 0);
            Assert.IsFalse(board.Cells[0, 0].IsAlive);
            Assert.IsFalse(board.Cells[1, 1].IsAlive);
            Assert.IsFalse(board.Cells[2, 2].IsAlive);
            Assert.IsFalse(board.Cells[3, 3].IsAlive);
        }

        [TestMethod]
        public void TestInitialBoard_AllAlive()
        {
            var board = new Board(4, 4, 1);
            Assert.IsTrue(board.Cells[0, 0].IsAlive);
            Assert.IsTrue(board.Cells[1, 1].IsAlive);
            Assert.IsTrue(board.Cells[2, 2].IsAlive);
            Assert.IsTrue(board.Cells[3, 3].IsAlive);
        }

        [TestMethod]
        public void TestAdvance_StillLifeBlock()
        {
            var board = new Board(4, 4, 0);
            board.Cells[1, 1].IsAlive = true;
            board.Cells[1, 2].IsAlive = true;
            board.Cells[2, 1].IsAlive = true;
            board.Cells[2, 2].IsAlive = true;

            board.Advance();

            Assert.IsTrue(board.Cells[1, 1].IsAlive);
            Assert.IsTrue(board.Cells[1, 2].IsAlive);
            Assert.IsTrue(board.Cells[2, 1].IsAlive);
            Assert.IsTrue(board.Cells[2, 2].IsAlive);
        }

        [TestMethod]
        public void TestAdvance_Oscillator()
        {
            var board = new Board(5, 5, 0);
            board.Cells[2, 1].IsAlive = true;
            board.Cells[2, 2].IsAlive = true;
            board.Cells[2, 3].IsAlive = true;

            board.Advance();

            Assert.IsFalse(board.Cells[2, 1].IsAlive);
            Assert.IsTrue(board.Cells[1, 2].IsAlive);
            Assert.IsTrue(board.Cells[2, 2].IsAlive);
            Assert.IsTrue(board.Cells[3, 2].IsAlive);
            Assert.IsFalse(board.Cells[2, 3].IsAlive);
        }
        [TestMethod]
        public void TestAdvance_Glider()
        {
            var board = new Board(5, 5, 0);
            board.Cells[1, 2].IsAlive = true;
            board.Cells[2, 3].IsAlive = true;
            board.Cells[3, 1].IsAlive = true;
            board.Cells[3, 2].IsAlive = true;
            board.Cells[3, 3].IsAlive = true;

            board.Advance();

            Assert.IsFalse(board.Cells[1, 2].IsAlive);
            Assert.IsTrue(board.Cells[2, 1].IsAlive);
            Assert.IsTrue(board.Cells[2, 3].IsAlive);
            Assert.IsTrue(board.Cells[3, 2].IsAlive);
            Assert.IsTrue(board.Cells[3, 3].IsAlive);
        }
    }
}