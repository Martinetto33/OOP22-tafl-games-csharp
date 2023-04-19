using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tafl_games.api;

namespace Tafl_games.code
{
    /// <summary>
    /// A mock implementation of the Match.
    /// </summary>
    public class MatchExample
    {
        public int TurnNumber { get; set; }
        public string ActivePlayer { get; set; }
        private BoardExample _board;

        /// <summary>
        /// Builds a new Match.
        /// </summary>
        /// <param name="playerInTurn">
        /// the name of the starting player.
        /// </param>
        /// <param name="board">
        /// the <see cref="BoardExample"/> related to this match.
        /// </param>
        public MatchExample(string playerInTurn, BoardExample board)
        {
            TurnNumber = 0;
            ActivePlayer = playerInTurn;
            _board = board;
        }

        /// <summary>
        /// Makes a move.
        /// </summary>
        /// <param name="startPos">
        /// the starting <see cref="Position"/>
        /// </param>
        /// <param name="endPos">
        /// the ending <see cref="Position"/>
        /// </param>
        public void Move(Position startPos, Position endPos) => _board.Move(startPos, endPos);

        /// <summary>
        /// Attacks a piece at a certain position, subtracting
        /// 1 from its lives.
        /// </summary>
        /// <param name="attackedPosition">
        /// the <see cref="Position"/> of the <see cref="PieceExample"/>.
        /// </param>
        public void Eat(Position attackedPosition) => _board.Eat(attackedPosition);

        /// <summary>
        /// Passes the turn.
        /// </summary>
        /// <param name="nextPlayerInTurn">
        /// the name of the player to be set as the active player of the next turn.
        /// </param>
        public void PassTurn(string nextPlayerInTurn)
        {
            TurnNumber++;
            ActivePlayer = nextPlayerInTurn;
        }

        /// <summary>
        /// Saves the current state of the Match.
        /// </summary>
        /// <returns>
        /// a <see cref="IMatchMemento"/> describing the current state of this Match.
        /// </returns>
        public IMatchMemento Save() => new MatchMementoImpl(this.TurnNumber, this.ActivePlayer, _board.Save());

        /// <summary>
        /// Restores the state of this Match to the one
        /// specified by the parameter <see cref="IMatchMemento"/>.
        /// </summary>
        /// <param name="m">
        /// the <see cref="IMatchMemento"/> that contains the snapshot
        /// to revert to.
        /// </param>
        public void Restore(IMatchMemento m)
        {
            this.TurnNumber = m.GetTurnNumber();
            this.ActivePlayer = m.GetActivePlayer();
            m.GetBoardMemento().Restore();
        }

        /// <summary>
        /// A class representing the state of a Match.
        /// </summary>
        public class MatchMementoImpl : IMatchMemento
        {
            private int InnerTurnNumber { get; }
            private string InnerActivePlayer { get; }
            private IBoardMemento InnerBoardMemento { get; }

            /// <summary>
            /// Builds a new MatchMementoImpl.
            /// </summary>
            /// <param name="innerTurnNumber">
            /// the turn number.
            /// </param>
            /// <param name="innerActivePlayer">
            /// the active player.
            /// </param>
            /// <param name="innerBoardMemento">
            /// the <see cref="IBoardMemento"/> containing
            /// the state of the <see cref="BoardExample"/> related to this
            /// match at a given turn.
            /// </param>
            public MatchMementoImpl(int innerTurnNumber, string innerActivePlayer, IBoardMemento innerBoardMemento)
            {
                InnerTurnNumber = innerTurnNumber;
                InnerActivePlayer = innerActivePlayer;
                InnerBoardMemento = innerBoardMemento;
            }

            /// <inheritdoc/>
            public string GetActivePlayer() => InnerActivePlayer;

            /// <inheritdoc/>
            public IBoardMemento GetBoardMemento() => InnerBoardMemento;

            /// <inheritdoc/>
            public int GetTurnNumber() => InnerTurnNumber;
        }

    }
}
