using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tafl_games.api
{
    /// <summary>
    /// This interface models a Caretaker, which is part of the memento pattern.
    /// The Caretaker stores trace of any relevant data such as turn number, positions
    /// etc. at a given turn, and is able to provide the Memento objects required
    /// to restore a previous state.
    /// </summary>
    public interface ICaretaker
    {
        /// <summary>
        /// Registers a new MatchMemento, by pushing it onto the history stack.
        /// This method should be called by a handler at the beginning of a turn,
        /// in order to save the starting state of the match before any changes.
        /// The call to this method causes the history to expand, and also
        /// locks it, so that no caller can undo moves unless the history
        /// is explicitly unlocked.
        /// </summary>
        void UpdateHistory();

        /// <summary>
        /// Returns to the previous saved state.
        /// </summary>
        void Undo();

        /// <summary>
        /// Must be called before any <see cref="Undo"/> operation
        /// can be performed.This additional step ensures that the state
        /// of the observed Match has actually changed before calling undo.
        /// </summary>
        void UnlockHistory();

        /// <summary>
        /// Returns whether the history is locked. If yes, {@link #undo()}
        /// method cannot be called until the history is unlocked.
        /// </summary>
        /// <returns>
        /// True if the history is locked.
        /// </returns>
        bool IsLocked();

    }
}
