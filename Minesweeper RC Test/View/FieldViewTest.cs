using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Extensions;
using Minesweeper_RC.Model;
using Minesweeper_RC.View;
using Moq;

namespace Minesweeper_RC_Test.View
{
    [TestClass]
    public class FieldViewTest
    {
        [TestMethod]
        public void TestInstantiateNoError()
        {
            IFieldView _ = new FieldView();
        }

        [TestMethod]
        public void TestSetDefaultCellSize()
        {
            IFieldView v = new FieldView();
            Assert.AreEqual<Size>(new Size(10, 10), v.CellSize);
        }

        [TestMethod]
        public void TestSetCellSize()
        {
            IFieldView v = new FieldView
            {
                CellSize = new Size(53, 53)
            };
            Assert.AreEqual<Size>(new Size(53, 53), v.CellSize);
        }

        [TestMethod]
        public void TestRenderFieldAccessesAllCellsAtLeastOnce()
        {
            var field = new Field(5, 5, 5);
            var mockField = new Mock<IField>(Moq.MockBehavior.Strict);

            // mock object returns the same data as our actual field instance
            mockField.SetupGet(f => f.FieldSize).Returns(field.FieldSize);
            for (var y = 0; y < field.FieldSize.Height; y++)
                for (var x = 0; x < field.FieldSize.Width; x++)
                    mockField.Setup(f => f.Get(x, y)).Returns(field.Get(x, y));

            IFieldView v = new FieldView();
            v.RenderField(mockField.Object);

            // verify all cells were queried at least once
            for (var y = 0; y < field.FieldSize.Height; y++)
                for (var x = 0; x < field.FieldSize.Width; x++)
                    mockField.Verify(f => f.Get(x, y), Times.AtLeastOnce);
        }

        [TestMethod]
        public void TestRenderFieldSetsCorrectUISize()
        {
            var field = new Field(5, 5, 5);
            var v = new FieldView();
            v.RenderField(field);
            Assert.AreEqual<Size>(v.CellSize.MultiplyBy(5), v.Size);
        }
    }
}
