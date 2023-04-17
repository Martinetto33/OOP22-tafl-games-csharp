﻿using NUnit.Framework;
using TaflGames.Code;

namespace TaflGames.Test
{
    public class TestBoard
    {
        private const int BoardSize= 4;

        private static Board _board;
        private static Dictionary<Position, ICell> _cells;
        private static Dictionary<Player, Dictionary<Position, Piece>> _pieces;
        private static Player p1 = Player.ATTACKER;
        private static Player p2 = Player.DEFENDER;

        [SetUp]
        public void Setup()
        {
            _cells = new Dictionary<Position, ICell>();
            _pieces = new Dictionary<Player, Dictionary<Position, Piece>>();
            Dictionary<Position, Piece> piecesPlayer1 = new();
            Dictionary<Position, Piece> piecesPlayer2 = new();
            piecesPlayer1.Add(new Position(0, 0), new Piece(new Position(0, 0), p1));
            piecesPlayer2.Add(new Position(3, 3), new Piece(new Position(3, 3), p2));
            _pieces.Add(p1, piecesPlayer1);
            _pieces.Add(p2, piecesPlayer2);
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    _cells.Add(new Position(i, j), new ClassicCell());
                    _cells[new Position(i, j)].SetFree(true);
                }
            }
            _cells[new Position(0, 0)].SetFree(false);
            _cells[new Position(3, 3)].SetFree(false);
            _board = new Board(_cells, _pieces, BoardSize);
        }

        [Test]
        public void TestIsStartingPointValid()
        {
            Assert.True(_board.IsStartingPointValid(new Position(3, 3), p2));
            Assert.True(_board.IsStartingPointValid(new Position(0, 0), p1));
            Assert.False(_board.IsStartingPointValid(new Position(3, 3), p1));
            Assert.False(_board.IsStartingPointValid(new Position(2, 2), p1));
        }

        [Test]
        public void TestGetAdjacentPositions()
        {
            HashSet<Position> setOfPosition = new()
            {
                new Position(3, 2),
                new Position(2, 3),
            };
            Assert.That(_board.GetAdjacentPositions(new Position(3, 3)), Is.EquivalentTo(setOfPosition));

            setOfPosition = new()
            {
                new Position(1, 0),
                new Position(0, 1),
            };
            Assert.That(_board.GetAdjacentPositions(new Position(0, 0)), Is.EquivalentTo(setOfPosition));

            setOfPosition = new()
            {
                new Position(1, 1),
                new Position(1, 3),
                new Position(0, 2),
                new Position(2, 2),
            };
            Assert.That(_board.GetAdjacentPositions(new Position(1, 2)), Is.EquivalentTo(setOfPosition));
        }

        [Test]

        public void TestGetPieceAtPosition()
        {
            Assert.That(_board.GetPieceAtPosition(new Position(0, 0)), Is.EqualTo(new Piece(new Position(0, 0), p1)));
            Assert.That(_board.GetPieceAtPosition(new Position(3, 3)), Is.EqualTo(new Piece(new Position(3, 3), p2)));
            Assert.That(_board.GetPieceAtPosition(new Position(1, 1)), Is.EqualTo(null));
        }

        [Test]

        public void TestIsPathFree()
        {
            //adding a Piece in Position 1,3
            _pieces[p2].Add(new Position(1, 3), new Piece(new Position(1, 3), p2));
            _cells[new Position(1, 3)].SetFree(false);

            Assert.True(_board.IsPathFree(new Position(3, 3), new Position(3, 0)));
            Assert.False(_board.IsPathFree(new Position(3, 3), new Position(0, 3)));
            Assert.True(_board.IsPathFree(new Position(0, 0), new Position(0, 3)));



        }
    }
}
