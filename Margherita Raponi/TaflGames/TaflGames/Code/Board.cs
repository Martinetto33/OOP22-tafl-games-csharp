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

        public bool IsStartingPointValid(Position start, Player player)
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

        public HashSet<Position> GetAdjacentPositions(Position currPos)
        {
            HashSet<Position> setOfPosition = new()
            {
                new Position(currPos.XPosition + 1, currPos.YPosition),
                new Position(currPos.XPosition- 1, currPos.YPosition),
                new Position(currPos.XPosition, currPos.YPosition + 1),
                new Position(currPos.XPosition, currPos.YPosition - 1)
            };
            return setOfPosition.Where(pos => pos.XPosition>= 0 && pos.YPosition >= 0
                                           && pos.XPosition< _size && pos.YPosition < _size).ToHashSet();
        }

        public IPiece GetPieceAtPosition(Position pos) => _pieces.Where(x => x.Value.ContainsKey(pos)).Select(x => x.Value[pos]).FirstOrDefault();


        public bool IsPathFree(Position start, Position dest)
        {
            if (start.XPosition == dest.XPosition)
            {
                if (start.YPosition < dest.YPosition)
                {
                    for (int i = start.YPosition + 1; i < dest.YPosition; i++)
                    {
                        if (!_cells[new Position(start.XPosition, i)].CanAccept(GetPieceAtPosition(start)))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = start.YPosition - 1; i > dest.YPosition; i--)
                    {
                        if (!_cells[new Position(start.XPosition, i)].CanAccept(GetPieceAtPosition(start)))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (start.XPosition< dest.XPosition)
                {
                    for (int i = start.XPosition+ 1; i < dest.XPosition; i++)
                    {
                        if (!_cells[new Position(i, start.YPosition)].CanAccept(GetPieceAtPosition(start)))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = start.XPosition- 1; i > dest.XPosition; i--)
                    {
                        if (!_cells[new Position(i, start.YPosition)].CanAccept(GetPieceAtPosition(start)))
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
