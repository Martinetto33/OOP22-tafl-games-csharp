using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tafl_games.api
{
    /// <summary>
    /// An interface modelling the behaviour that the Inner Class of the Cell implementation
    /// should provide in order to save a snapshot of the current state of a Cell. Said Inner
    /// Class should implement this interface.
    /// This interface is part of the pattern Memento.
    /// </summary>
    public interface ICellMemento
    {
        /// <summary>
        /// This method should allow each instance of the Inner Classes reference their Outer
        /// classes and call their "restore()" method, by passing themselves as a parameter [i.e. by passing "this"].
        /// This way, the Inner Classes themselves will be in charge of finding the Cell instance
        /// that had generated them, simplifying the duty of the Caretaker and the other external
        /// classes which shouldn't bother with these associations.
        /// </summary>
        void Restore();

        /// <summary>
        /// Returns the cell status.
        /// </summary>
        /// <returns>
        /// the cell status.
        /// </returns>
        bool GetCellStatus();
    }
}
