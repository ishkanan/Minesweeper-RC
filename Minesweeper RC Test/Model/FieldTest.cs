using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Model;
using System;
using System.Drawing;

namespace Minesweeper_RC_Test.Model
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestGeneratedFieldIsLocked()
        {
            var field = new Field(5, 5, 5);
            for (var y = 0; y < 5; y++)
                for (var x = 0; x < 5; x++)
                    Assert.AreEqual(true, field.Get(x, y).Locked);
        }

        [TestMethod]
        public void TestGenerateInvalidParams()
        {
            foreach (int i in new int[] { -1, 0, 1, 2, 3 })
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Field(i, 5, 5));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Field(5, i, 5));
            }

            // min. mines is 1
            foreach (int i in new int[] { -2, -1, 0 })
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Field(5, 5, i));

            // max. mines is (5x5)-9=16
            foreach (int i in new int[] { 17, 18, 19 })
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Field(5, 5, i));
        }

        [TestMethod]
        public void TestGenerateMaximumValidMines()
        {
            _ = new Field(4, 4, 7);
            _ = new Field(5, 5, 16);
            _ = new Field(6, 6, 27);
        }

        [TestMethod]
        public void TestGenerateValidSafeStartLocation()
        {
            var field = new Field(5, 5, 16);
            Assert.AreEqual(0, field.Get(field.SafeStartLocation).Neighbours);
            foreach (var p in Field.GetAdjacentPoints(field.SafeStartLocation, 5, 5))
                Assert.AreEqual(false, field.Get(p).IsMine);
        }

        [TestMethod]
        public void TestGenerateFieldMatchesParams()
        {
            var field = new Field(5, 7, 6);
            Assert.AreEqual(new Size(5, 7), field.FieldSize);
            Assert.AreEqual(6, field.TotalMines);

            var numMines = 0;
            for (var y = 0; y < 7; y++)
                for (var x = 0; x < 5; x++)
                    numMines = (field.Get(x, y).IsMine ? numMines + 1 : numMines);
            Assert.AreEqual(6, numMines);
        }

        [TestMethod]
        public void TestValidNeighbourValues()
        {
            var field = new Field(5, 5, 7);
            foreach (var cell in field)
            {
                // tally up actual neighbour mine count
                var adjacents = Field.GetAdjacentPoints(cell.Location, 5, 5);
                var numMines = 0;
                foreach (var p in adjacents)
                    numMines += field.Get(p).IsMine ? 1 : 0;
                Assert.AreEqual(cell.Neighbours, numMines);
            }
        }

        [TestMethod]
        public void Test2DArrayIsClone()
        {
            var field = new Field(5, 5, 7);
            var theClone = field.As2DArray();
            theClone[0, 0] = null;
            Assert.IsNotNull(field.Get(0, 0));
        }

        [TestMethod]
        public void TestGetMultipleCells()
        {
            var field = new Field(5, 5, 7);
            Assert.ThrowsException<ArgumentNullException>(() => field.Get(null));
            Assert.ThrowsException<ArgumentException>(() => field.Get(new Point[] { }));
            var pointsToGet = new Point[] {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2)
            };
            var cells = field.Get(pointsToGet);
            Assert.AreEqual(pointsToGet[0], cells[0].Location);
            Assert.AreEqual(pointsToGet[1], cells[1].Location);
            Assert.AreEqual(pointsToGet[2], cells[2].Location);
        }

        [TestMethod]
        public void TestAdjacentPointsInvalidParams()
        {
            foreach (int i in new int[] { -3, -2, -1 })
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.GetAdjacentPoints(new Point(i, 5), 5, 5));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.GetAdjacentPoints(new Point(5, i), 5, 5));
            }
            var center = new Point(5, 5);
            foreach (int i in new int[] { -2, -1, 0 })
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.GetAdjacentPoints(center, i, 5));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.GetAdjacentPoints(center, 5, i));
            }
        }

        [TestMethod]
        public void TestFieldCellCountIsCorrect()
        {
            var field = new Field(5, 5, 5);
            Assert.AreEqual<int>(25, field.CellCount);
            field = new Field(4, 6, 1);
            Assert.AreEqual<int>(24, field.CellCount);
        }
    }
}
