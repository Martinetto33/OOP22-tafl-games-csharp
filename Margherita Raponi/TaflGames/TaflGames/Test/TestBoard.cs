using NUnit.Framework;
using TaflGames.Code;

namespace TaflGames.Test
{
    public class TestBoard
    {
        private const int BoardSize= 5;

        private static IBoard board;
        private static Dictionary<Position, ICell> _cells;
        private static Dictionary<Player, Dictionary<Position, IPiece>> _pieces;
        private static Player p1 = Player.ATTACKER;
        private static Player p2 = Player.DEFENDER;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestIsStartingPointValid()
        {
            
        }

        [Test]
        public void TestGetAdjacentPositions()
        {

        }

        [Test]

        public void TestGetPieceAtPosition()
        {

        }

        [Test]

        public void TestIsPathFree()
        {

        }
    }
}
