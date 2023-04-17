using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;

namespace TaflGames.Code
{
    /// <summary>
    /// This class models a IBoard.
    /// </summary>
    public class Board : IBoard
    {
        private Dictionary<Position, ICell> _cells;
        private Dictionary<Player, Dictionary<Position, PieceMock>> _pieces;
        private readonly int _size;

        /// <summary>
        /// Create a new Board based on the Map cells, the Map pieces and the size given. 
        /// </summary>
        /// <param name="cells">
        /// the Map of Position and Cell that associate
        /// to each Position of the Board the type of Cell that is placed there.
        /// </param>
        /// <param name="pieces">
        /// the Map that associate to each Player it's own Map of Piece and Position.
        /// </param>
        /// <param name="size">
        /// the size of the board.
        /// </param>
        public Board(Dictionary<Position, ICell> cells, Dictionary<Player, Dictionary<Position, PieceMock>> pieces, int size)
        {
            _cells = cells;
            _pieces = pieces;
            _size = size;

        }

        /// <inheritdoc/>
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

        /// <summary>
        /// Find the adjacent positions to the one given.
        /// </summary>
        /// <param name="currPos">
        /// The Position of which to find the adjacent ones.
        /// </param>
        /// <returns>
        /// An HashSet containing the adjacent positions.
        /// </returns>
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

        /// <summary>
        /// Return the Piece that is on the given Position. 
        /// </summary>
        /// <param name="pos"> 
        /// the position where the piece is located. 
        /// </param>
        /// <returns> 
        /// the Piece that is on the Position given, if there's no Piece on it return null. 
        /// </returns>
        public IPieceMock GetPieceAtPosition(Position pos) => _pieces.Where(x => x.Value.ContainsKey(pos)).Select(x => x.Value[pos]).FirstOrDefault();

        /// <summary>
        /// Verify if the path between two Position that are on the same row or column is free from pieces.
        /// </summary>
        /// <param name="start"> 
        /// the starting Position. 
        /// </param>
        /// <param name="dest"> 
        /// the Position to reach. 
        /// </param>
        /// <returns> 
        /// true if the path is free, false otherwise 
        /// </returns>
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
