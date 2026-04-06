using chesboard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetEntry_StoredCell_ReturnsCorrectValue()
        {
            Chessboard chessboard = new Chessboard(3, 3, (i, j) => (i + 1) * 10 + (j + 1));

            Assert.AreEqual(11, chessboard.getEnteries(1, 1));
            Assert.AreEqual(0, chessboard.getEnteries(1, 2));
            Assert.AreEqual(13, chessboard.getEnteries(1, 3));
            Assert.AreEqual(22, chessboard.getEnteries(2, 2));
        }

        [TestMethod]
        public void SetEntry_StoredCell_SetsCorrectValue()
        {
            Chessboard chessboard = new Chessboard(3, 3);

            chessboard.SetEntry(1, 1, 7);
            Assert.AreEqual(7, chessboard.getEnteries(1, 1));
        }

        [TestMethod]
        public void Add_SameDimensions_AddsSuccessfully()
        {
            Chessboard a = new Chessboard(2, 2, (i, j) => 1);
            Chessboard b = new Chessboard(2, 2, (i, j) => 2);

            a.Add(b);

            Assert.AreEqual(3, a.getEnteries(1, 1));
            Assert.AreEqual(0, a.getEnteries(1, 2));
            Assert.AreEqual(0, a.getEnteries(2, 1));
            Assert.AreEqual(3, a.getEnteries(2, 2));
        }

        [TestMethod]
        public void Multiply_CompatibleMatrices_WorksCorrectly()
        {
            Chessboard a = new Chessboard(2, 2);
            a.SetEntry(1, 1, 2);
            a.SetEntry(2, 2, 3);

            Chessboard b = new Chessboard(2, 2);
            b.SetEntry(1, 1, 4);
            b.SetEntry(2, 2, 5);

            a.multiply(b);

            Assert.AreEqual(8, a.getEnteries(1, 1));
            Assert.AreEqual(0, a.getEnteries(1, 2));
            Assert.AreEqual(0, a.getEnteries(2, 1));
            Assert.AreEqual(15, a.getEnteries(2, 2));
        }
    }
}