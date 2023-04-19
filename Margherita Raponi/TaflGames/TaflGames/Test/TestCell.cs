using NUnit.Framework;
using TaflGames.Code;

namespace TaflGames.Test
{
    /// <summary>
    /// Test for Cell.
    /// </summary>
    public class TestCell
    {
        private AbstractCell _classicCell;

        /// <summary>
        /// Initialize a ClassicCell.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _classicCell = new ClassicCell();
        }

        /// <summary>
        /// Test if a cell can accept a particular piece.
        /// </summary>
        [Test]
        public void TestCanAccept()
        {
            IPieceMock piece = new PieceMock(new Position(0,0), Player.ATTACKER);
            Assert.True(_classicCell.CanAccept(piece));
        }

        /// <summary>
        ///  Test if a cell is free or not.
        /// </summary>
        [Test]
        public void TestIsFree()
        {
            Assert.True(_classicCell.IsFree);
        }

        /// <summary>
        /// Test the setting of the fild isFree.
        /// </summary>
        [Test]
        public void TestSetFree()
        {
            _classicCell.SetFree(true);
            Assert.True(_classicCell.IsFree);
            _classicCell.SetFree(false);
            Assert.False(_classicCell.IsFree);
        }

        /// <summary>
        /// Test the getter for the cell type.
        /// </summary>
        [Test]
        public void TestGetType()
        {
            Assert.That(_classicCell.Type, Is.EqualTo("ClassicCell"));
        }
    }
}