using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tafl_games.api;

namespace Tafl_games.code
{
    /// <summary>
    /// A class modelling a Piece.
    /// </summary>
    public class PieceExample
    {
        /// <summary>
        /// The Position of this Piece.
        /// </summary>
        public Position Pos { get; set; }

        /// <summary>
        /// The lives remaining of this Piece.
        /// </summary>
        public int NumberOfLives { get; set; }


        /// <summary>
        /// Builds a new PieceExample.
        /// </summary>
        /// <param name="pos">
        /// the starting position where this Piece is spawned.
        /// </param>
        /// <param name="numberOfLives">
        /// the starting number of lives of this Piece.
        /// </param>
        public PieceExample(Position pos, int numberOfLives) 
        {
            Pos = pos;
            NumberOfLives = numberOfLives;
        }

        private void Restore(IPieceMemento pm)
        {
            Pos = pm.GetPosition();
            NumberOfLives = pm.GetNumberOfLives();
        }

        /// <summary>
        /// Saves the current state of this Piece.
        /// </summary>
        /// <returns>
        /// a <see cref="IPieceMemento"/> describing the current state of this Piece.
        /// </returns>
        public IPieceMemento Save() => new PieceMementoImpl(this);

        /// <summary>
        /// A class modelling a memento for a Piece.
        /// </summary>
        public class PieceMementoImpl : IPieceMemento
        {
            private readonly PieceExample _parent;
            private Position InnerPosition { get; }
            private int InnerNumberOfLives { get; }

            /// <summary>
            /// Builds a new PieceMementoImpl.
            /// </summary>
            /// <param name="parent">
            /// the <see cref="PieceExample"/> that originated this Memeto.
            /// This way, this memento is able to find itself the element
            /// that generated it and revert it to the state contained in this memento.
            /// </param>
            public PieceMementoImpl(PieceExample parent)
            {
                _parent = parent;
                InnerPosition = parent.Pos;
                InnerNumberOfLives = parent.NumberOfLives;
            }

            /// <inheritdoc/>
            public int GetNumberOfLives() => InnerNumberOfLives;

            /// <inheritdoc/>
            public Position GetPosition() => InnerPosition;

            /// <inheritdoc/>
            public void Restore() => _parent.Restore(this);
        }
    }
}
