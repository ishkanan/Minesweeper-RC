using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Model;
using System;
using System.Drawing;

namespace Minesweeper_RC_Test.Model
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void TestInstantiateNoError()
        {
            Assert.IsInstanceOfType(new Cell(false, 0, 1, 1), typeof(Cell));
        }

        [TestMethod]
        public void TestGetPropertiesAfterInstantiate()
        {
            var cell = new Cell(true, 1, 5, 10);
            Assert.AreEqual(false, cell.IsFlagged);
            Assert.AreEqual(true, cell.IsMine);
            Assert.AreEqual(false, cell.IsRevealed);
            Assert.AreEqual(1, cell.Neighbours);
            Assert.AreEqual(new Point(5, 10), cell.Location);
            Assert.AreEqual(false, cell.Locked);
        }

        [TestMethod]
        public void TestGetPropertiesAfterLock()
        {
            var cell = new Cell(true, 1, 5, 10);
            cell.Lock();
            Assert.AreEqual(false, cell.IsFlagged);
            Assert.AreEqual(true, cell.IsMine);
            Assert.AreEqual(false, cell.IsRevealed);
            Assert.AreEqual(1, cell.Neighbours, 1);
            Assert.AreEqual(new Point(5, 10), cell.Location);
            Assert.AreEqual(true, cell.Locked);
        }

        [TestMethod]
        public void TestToggleIsFlagged()
        {
            var cell = new Cell(true, 1, 5, 10) { IsFlagged = true };
            Assert.AreEqual(true, cell.IsFlagged);
            cell.IsFlagged = false;
            Assert.AreEqual(false, cell.IsFlagged);
        }

        [TestMethod]
        public void TestSetIsMineBeforeAndAfterLock()
        {
            var cell = new Cell(true, 1, 5, 10) { IsMine = false };
            Assert.AreEqual(false, cell.IsMine);
            cell.Lock();
            Assert.ThrowsException<InvalidOperationException>(() => cell.IsMine = true);
        }

        [TestMethod]
        public void TestSetLocationBeforeAndAfterLock()
        {
            var cell = new Cell(true, 1, 5, 10) { Location = new Point(2, 3) };
            Assert.AreEqual(new Point(2, 3), cell.Location);
            cell.Lock();
            Assert.ThrowsException<InvalidOperationException>(() => cell.Location = new Point(5, 10));
        }

        [TestMethod]
        public void TestSetNeighboursBeforeAndAfterLock()
        {
            var cell = new Cell(true, 1, 5, 10) { Neighbours = 2 };
            Assert.AreEqual(2, cell.Neighbours);
            cell.Lock();
            Assert.ThrowsException<InvalidOperationException>(() => cell.Neighbours = 1);
        }
    }
}
