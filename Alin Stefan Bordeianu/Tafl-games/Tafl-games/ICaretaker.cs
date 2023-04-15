using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tafl_games
{
    /* This interface models a Caretaker, which is part of the memento pattern.
     The Caretaker stores trace of any relevant data such as turn number, positions
     etc. at a given turn, and is able to provide the Memento objects required
     to restore a previous state. */
    public interface ICaretaker
    {
        void UpdateHistory();

        void Undo();

        void UnlockHistory();

        bool IsLocked();

    }
}
