using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC;

namespace Minesweeper_RC_Test
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestGeneratedFieldIsLocked()
        {
            var field = Field.Generate(5, 5, 5).Item2;
            for (var y = 0; y < 5; y++)
                for (var x = 0; x < 5; x++)
                    Assert.AreEqual(true, field[x, y].Locked);
        }

        [TestMethod]
        public void TestGenerateInvalidParams()
        {
            foreach (int i in new int[] { -1, 0, 1, 2, 3 })
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.Generate(i, 5, 5));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.Generate(5, i, 5));
            }

            // min. mines is 1
            foreach (int i in new int[] { -2, -1, 0 })
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.Generate(5, 5, i));

            // max. mines is (5x5)-9=16
            foreach (int i in new int[] { 17, 18, 19 })
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => Field.Generate(5, 5, i));
        }

        [TestMethod]
        public void TestGenerateSafeStartLocation()
        {
            var ret = Field.Generate(5, 5, 5);
            var start = ret.Item1;
            var field = ret.Item2;
            Assert.AreEqual(0, field[start.X, start.Y].Neighbours);
        }

        [TestMethod]
        public void TestGenerateFieldMatchesParams()
        {
            var field = Field.Generate(5, 7, 6).Item2;
            Assert.AreEqual(5, field.GetLength(0));
            Assert.AreEqual(7, field.GetLength(1));

            var numMines = 0;
            for (var y = 0; y < 7; y++)
                for (var x = 0; x < 5; x++)
                    numMines = (field[x, y].IsMine ? numMines + 1 : numMines);
            Assert.AreEqual(6, numMines);
        }
    }
}
