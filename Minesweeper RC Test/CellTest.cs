using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC;

namespace Minesweeper_RC_Test
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
            Assert.AreEqual(cell.IsFlagged, false);
            Assert.AreEqual(cell.IsMine, true);
            Assert.AreEqual(cell.IsRevealed, false);
            Assert.AreEqual(cell.Neighbours, 1);
            Assert.AreEqual(cell.Location, new Point(5, 10));
            Assert.AreEqual(cell.Locked, false);
        }

        [TestMethod]
        public void TestGetPropertiesAfterLockAndReveal()
        {
            var cell = new Cell(true, 1, 5, 10);
            cell.Lock();
            cell.Reveal();
            Assert.AreEqual(cell.IsFlagged, false);
            Assert.AreEqual(cell.IsMine, true);
            Assert.AreEqual(cell.IsRevealed, true);
            Assert.AreEqual(cell.Neighbours, 1);
            Assert.AreEqual(cell.Location, new Point(5, 10));
            Assert.AreEqual(cell.Locked, true);
        }

        [TestMethod]
        public void TestGetPropertiesAfterLockNoReveal()
        {
            var cell = new Cell(true, 1, 5, 10);
            cell.Lock();
            Assert.AreEqual(cell.IsFlagged, false);
            Assert.ThrowsException<InvalidOperationException>(() => cell.IsMine);
            Assert.AreEqual(cell.IsRevealed, false);
            Assert.ThrowsException<InvalidOperationException>(() => cell.Neighbours);
            Assert.AreEqual(cell.Location, new Point(5, 10));
            Assert.AreEqual(cell.Locked, true);
        }

        [TestMethod]
        public void TestGetPropertiesAfterRevealNoUnlock()
        {
            var cell = new Cell(true, 1, 5, 10);
            cell.Reveal();
            Assert.AreEqual(cell.IsFlagged, false);
            Assert.AreEqual(cell.IsMine, true);
            Assert.AreEqual(cell.IsRevealed, true);
            Assert.AreEqual(cell.Neighbours, 1);
            Assert.AreEqual(cell.Location, new Point(5, 10));
            Assert.AreEqual(cell.Locked, false);
        }

        [TestMethod]
        public void TestSetIsFlagged()
        {
            var cell = new Cell(true, 1, 5, 10) { IsFlagged = true };
            Assert.AreEqual(cell.IsFlagged, true);
        }

        [TestMethod]
        public void TestSetIsMine()
        {
            var cell = new Cell(true, 1, 5, 10) { IsMine = false };
            Assert.AreEqual(cell.IsMine, false);
            cell.Lock();
            Assert.ThrowsException<InvalidOperationException>(() => cell.IsMine = true);
        }

        [TestMethod]
        public void TestSetLocation()
        {
            var cell = new Cell(true, 1, 5, 10) { Location = new Point(2, 3) };
            Assert.AreEqual(cell.Location, new Point(2, 3));
            cell.Lock();
            Assert.ThrowsException<InvalidOperationException>(() => cell.Location = new Point(5, 10));
        }

        [TestMethod]
        public void TestSetNeighbours()
        {
            var cell = new Cell(true, 1, 5, 10) { Neighbours = 2 };
            Assert.AreEqual(cell.Neighbours, 2);
            cell.Lock();
            Assert.ThrowsException<InvalidOperationException>(() => cell.Neighbours = 1);
        }
    }
}
