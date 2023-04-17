using System.Collections.ObjectModel;
using System.Linq;

namespace TaflGames.Code
{
    public class Board : IBoard
    {
        private Dictionary<Position, ICell> _cells;
        private Dictionary<Player, Dictionary<Position, Piece>> _pieces;
        private readonly int _size;

        public Board(Dictionary<Position, ICell> cells, Dictionary<Player, Dictionary<Position, Piece>> pieces, int size)
        {
            _cells = cells;
            _pieces = pieces;
            _size = size;

        }

        public bool isStartingPointValid(Position start, Player player)
        {
            if (_pieces[player].ContainsKey(start) && _pieces[player][start].IsAlive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private HashSet<Position> GetAdjacentPositions(Position currPos)
        {
            HashSet<Position> setOfPosition = new()
            {
                new Position(currPos.X + 1, currPos.Y),
                new Position(currPos.X - 1, currPos.Y),
                new Position(currPos.X, currPos.Y + 1),
                new Position(currPos.X, currPos.Y - 1)
            };
            return setOfPosition.Where(pos => pos.X >= 0 && pos.Y >= 0
                                           && pos.X < _size && pos.Y < _size).ToHashSet();
        }

        private Piece GetPieceAtPosition(Position pos) => _pieces.Where(x => x.Value.ContainsKey(pos)).Select(x => x.Value[pos]).FirstOrDefault();


        private bool IsPathFree(Position start, Position dest)
        {
            if (start.X == dest.X)
            {
                if (start.Y < dest.Y)
                {
                    for (int i = start.Y + 1; i < dest.Y; i++)
                    {
                        if (!_cells[new Position(start.X, i)].CanAccept(GetPieceAtPosition(start)))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = start.Y - 1; i > dest.Y; i--)
                    {
                        if (!_cells[new Position(start.X, i)].CanAccept(GetPieceAtPosition(start)))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (start.X < dest.X)
                {
                    for (int i = start.X + 1; i < dest.X; i++)
                    {
                        if (!_cells[new Position(i, start.Y)].CanAccept(GetPieceAtPosition(start)))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = start.X - 1; i > dest.X; i--)
                    {
                        if (!_cells[new Position(i, start.Y)].CanAccept(GetPieceAtPosition(start)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
