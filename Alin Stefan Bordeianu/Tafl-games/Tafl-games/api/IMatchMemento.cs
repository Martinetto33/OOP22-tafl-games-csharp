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

        string GetActivePlayer();

        IBoardMemento GetBoardMemento();


    }
}
