using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Tafl_games.api;
using System.Collections.ObjectModel;

namespace Tafl_games.code
{
    /// <summary>
    /// A class representing a Board.
    /// </summary>
    public class BoardExample
    {
        private static readonly int SIZE = 11;
        private static readonly int PIECES_ROWS = 2;
        public IDictionary<Position, CellExample> Cells { get; set; }
        public IDictionary<Position, PieceExample> Pieces { get; set; }

        /// <summary>
        /// Builds a new Board example, with a base configuration of pieces and cells.
        /// Te disposition of the elements does not reflect the actual game rules.
        /// </summary>
        public BoardExample() 
        {
            /* Creating a default board */
            Cells = new Dictionary<Position, CellExample>();
            for (int i = 0; i < SIZE; i++) 
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Cells.Add(new Position(i, j), new CellExample());
                }
            }
            Pieces = new Dictionary<Position, PieceExample>();
            /* Placing pieces on the two top rows and the two bottom
             rows */
            for (int i = 0; i < PIECES_ROWS; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Position currentPos = new(i, j);
                    Pieces.Add(currentPos, new PieceExample(currentPos, 1));
                    Cells[currentPos].CellStatus = false; // occupying the cells
                }
            }

            for (int i = SIZE - PIECES_ROWS; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Position currentPos = new(i, j);
                    Pieces.Add(currentPos, new PieceExample(currentPos, 1));
                    Cells[currentPos].CellStatus = false; // occupying the cells
                }
            }
        }

        /// <summary>
        /// A method used to move the pieces on the Board.
        /// </summary>
        /// <param name="startPosition">
        /// the starting <see cref="Position"/>.
        /// </param>
        /// <param name="endPosition">
        /// the ending <see cref="Position"/>.
        /// </param>
        public void Move(Position startPosition, Position endPosition)
        {
            bool isPresent = Pieces.TryGetValue(startPosition, out PieceExample? pieceToMove);
            if (isPresent && pieceToMove != null)
            { 
                Pieces.Remove(startPosition);
                Cells[startPosition].CellStatus = true;
                Debug.Assert(Cells[endPosition].CellStatus); //the Cell must be free.
                Pieces.Add(endPosition, pieceToMove);
                pieceToMove.Pos = endPosition;
                Cells[endPosition].CellStatus = false;
            } else
            {
                Console.WriteLine("No Piece was found at x: " + startPosition.XPosition + ", y: " + startPosition.YPosition);
            }
        }

        /// <summary>
        /// A method used to attack and possibly kill pieces on the board.
        /// </summary>
        /// <param name="attackedPosition">
        /// the <see cref="Position"/> where the attack occurred.
        /// </param>
        public void Eat(Position attackedPosition)
        {
            Debug.Assert(Pieces.TryGetValue(attackedPosition, out PieceExample? pieceToKill));
            pieceToKill.NumberOfLives--;
            if (pieceToKill.NumberOfLives == 0)
            {
                Cells[attackedPosition].CellStatus = true; //the Cell is now free
            }
        }

        private void Restore(IBoardMemento boardMemento)
        {
            foreach (ICellMemento elem in boardMemento.GetCellsMemento())
            {
                elem.Restore();
            }
            foreach (IPieceMemento elem in boardMemento.GetPiecesMemento())
            {
                elem.Restore();
            }
        }

        /// <summary>
        /// Saves the current state of the board.
        /// </summary>
        /// <returns>
        /// a <see cref="IBoardMemento"/> describing the current state of this Board.
        /// </returns>
        public IBoardMemento Save()
        {
            return new BoardMementoImpl(this,
                Cells.Values.Select(cell => cell.Save()).ToList(),
                Pieces.Values.Select(piece => piece.Save()).ToList());
        }

        /// <summary>
        /// A class modelling a memento for a Board.
        /// </summary>
        public class BoardMementoImpl : IBoardMemento
        {
            private readonly BoardExample _parent;
            private IList<ICellMemento> CellsMemento { get; }
            private IList<IPieceMemento> PiecesMemento { get; }

            /// <summary>
            /// Builds a new BoardMementoImpl and ensures that Cells and Pieces Memento are stored
            /// as well.
            /// </summary>
            /// <param name="parent">
            /// a reference to the <see cref="BoardExample"/> that created this Memento.
            /// </param>
            /// <param name="cellsMemento">
            /// a collection of <see cref="ICellMemento"/>.
            /// </param>
            /// <param name="piecesMemento">
            /// a collection of <see cref="IPieceMemento"/>.
            /// </param>
            public BoardMementoImpl(BoardExample parent, IList<ICellMemento> cellsMemento, IList<IPieceMemento> piecesMemento)
            {
                _parent = parent;
                CellsMemento = cellsMemento;
                PiecesMemento = piecesMemento;
            }

            /// <inheritdoc/>
            public IList<ICellMemento> GetCellsMemento() => new ReadOnlyCollection<ICellMemento>(CellsMemento);

            /// <inheritdoc/>
            public IList<IPieceMemento> GetPiecesMemento() => new ReadOnlyCollection<IPieceMemento>(PiecesMemento);
            
            /// <inheritdoc/>
            public void Restore() => _parent.Restore(this);
        }
    }
}
