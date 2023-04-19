using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tafl_games.api;

namespace Tafl_games.code
{
    /// <summary>
    /// This class will model a Caretaker, a class which is part of the pattern Memento
    /// and that will be in charge of managing the history of a single Match, which here
    /// takes the name of "originator".
    /// The updateHistory() method should be called each time a new turn begins, while
    /// the undo() method could be called at any given moment in a turn.
    /// 
    /// This class will be implemented with a Stack, in order to allow methods
    /// "pop" and "push" on the history
    ///
    /// In this version of the implementation, the Caretaker will dump
    /// any existing state each time a new state is saved.
    /// </summary>
    public class CaretakerImpl : ICaretaker
    {
        private readonly MatchExample _originator;
        private readonly Stack<IMatchMemento> _history;
        private bool _locked;

        /// <summary>
        /// Builds a new CaretakerImpl.
        /// </summary>
        /// <param name="originator">
        /// the Match to save the state of.
        /// </param>
        public CaretakerImpl(MatchExample originator)
        {
            _originator = originator;
            _history = new Stack<IMatchMemento>();
            _locked = false;
        }

        /// <inheritdoc/>
        public void UpdateHistory()
        {
            if (_history.Count > 0)
            {
                _history.Clear();
            }
            _history.Push(_originator.Save());
            _locked = true;
        }

        /// <inheritdoc/>
        public void Undo()
        {
            if (_history.Count == 0)
            {
                return;
            } else if (_locked) 
            {
                throw new InvalidOperationException("History is locked!");
            }
            _originator.Restore(_history.Pop());
            this.UpdateHistory();
        }

        /// <inheritdoc/>
        public bool IsLocked() => _locked;

        /// <inheritdoc/>
        public void UnlockHistory() => _locked = false;

    }
}
