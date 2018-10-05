using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Utility;
using Moq;
using System;
using System.Globalization;
using System.Resources;
using System.Linq;

namespace Minesweeper_RC_Test
{
    [TestClass]
    public class UtilityTest
    {
        private MockRepository _strictRepo;
        private ResourceManager _resManagerMock;
        private string _validResName = "TestResource";
        private string _invalidResName = "MissingResource";
        private byte[] _resBytes = new byte[] { 1, 2, 3, 4, 5 };

        [TestInitialize]
        public void Setup()
        {
            _strictRepo = new MockRepository(MockBehavior.Strict);

            // setup various mocks
            var resSetMock = _strictRepo.Of<ResourceSet>()
                .Where(r => r.GetObject(_validResName) == _resBytes)
                .Where(r => r.GetObject(_invalidResName) == null)
                .First();
            _resManagerMock = _strictRepo.Of<ResourceManager>()
                .Where(m => m.GetResourceSet(CultureInfo.CurrentUICulture, true, true) == resSetMock)
                .First();
        }

        [TestMethod]
        public void TestGetResourceBytesInvalidParams()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Utility.GetResourceBytes(null, _validResName));
            Assert.ThrowsException<ArgumentNullException>(() => Utility.GetResourceBytes(_resManagerMock, null));
            Assert.ThrowsException<ArgumentException>(() => Utility.GetResourceBytes(_resManagerMock, ""));
        }

        [TestMethod]
        public void TestGetResourceBytesInvalidResource()
        {
            Assert.ThrowsException<ArgumentException>(() => Utility.GetResourceBytes(_resManagerMock, _invalidResName));
        }

        [TestMethod]
        public void TestGetResourceBytesValidResource()
        {
            var actualBytes = Utility.GetResourceBytes(_resManagerMock, _validResName);

            CollectionAssert.AreEqual(_resBytes, actualBytes);
        }

        [TestMethod]
        public void TestGetResourceBytesPointerInvalidParams()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Utility.GetResourceBytesPointer(null, _validResName));
            Assert.ThrowsException<ArgumentNullException>(() => Utility.GetResourceBytesPointer(_resManagerMock, null));
            Assert.ThrowsException<ArgumentException>(() => Utility.GetResourceBytesPointer(_resManagerMock, ""));
        }

        [TestMethod]
        public void TestGetResourceBytesPointerInvalidResource()
        {
            Assert.ThrowsException<ArgumentException>(() => Utility.GetResourceBytesPointer(_resManagerMock, _invalidResName));
        }

        [TestMethod]
        public void TestGetResourceBytesPointerValidResource()
        {
            var actualData = Utility.GetResourceBytesPointer(_resManagerMock, _validResName);

            Assert.AreNotEqual<int>(0, actualData.Item1.ToInt32());
            Assert.AreEqual<int>(_resBytes.Length, actualData.Item2);
        }
    }
}
