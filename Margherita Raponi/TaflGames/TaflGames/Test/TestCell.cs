using NUnit.Framework;
using TaflGames.Code;

namespace TaflGames.Test
{
    public class TestCell
    {
        private AbstractCell _classicCell;

        [SetUp]
        public void Setup()
        {
            _classicCell = new ClassicCell();
        }

        [Test]
        public void TestCanAccept()
        {
            IPieceMock piece = new PieceMock(new Position(0,0), Player.ATTACKER);
            Assert.True(_classicCell.CanAccept(piece));
        }

        [Test]
        public void TestIsFree()
        {
            Assert.True(_classicCell.IsFree);
        }

        [Test]
        public void TestSetFree()
        {
            _classicCell.SetFree(true);
            Assert.True(_classicCell.IsFree);
            _classicCell.SetFree(false);
            Assert.False(_classicCell.IsFree);
        }

        [Test]
        public void TestGetType()
        {
            Assert.That(_classicCell.Type, Is.EqualTo("ClassicCell"));
        }
    }
}