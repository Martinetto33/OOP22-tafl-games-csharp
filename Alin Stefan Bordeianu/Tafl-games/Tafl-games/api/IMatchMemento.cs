using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tafl_games.api
{
    /// <summary>
    /// An interface modelling the behaviour that the Inner Class of the Match implementation
    /// should provide in order to save a snapshot of the current state of a Match. Said Inner
    /// Class should implement this interface.
    /// This interface is part of the pattern Memento.
    /// </summary>
    public interface IMatchMemento
    {
        /// <summary>
        /// Returns the turn number of the registered snapshot.
        /// </summary>
        /// <returns>
        /// the turn number of the snapshot of the match stored by this MatchMemento.
        /// </returns>
        int GetTurnNumber();

        /// <summary>
        /// Returns the current active player.
        /// </summary>
        /// <returns>
        /// the name of the active player.
        /// </returns>
        string GetActivePlayer();

        /// <summary>
        /// Returns the saved state of the Board.
        /// </summary>
        /// <returns>
        /// a <see cref="IBoardMemento"/> describing the state of the board
        /// implied in this Match.
        /// </returns>
        IBoardMemento GetBoardMemento();


    }
}
