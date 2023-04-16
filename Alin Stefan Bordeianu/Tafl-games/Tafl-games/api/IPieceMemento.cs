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
    /// An interface modelling the behaviour that the Inner Class of the Piece implementation
    /// should provide in order to save a snapshot of the current state of a Piece. Said Inner
    /// Class should implement this interface.
    /// This interface is part of the pattern Memento.
    /// </summary>
    public interface IPieceMemento
    {
        /// <summary>
        /// This method should allow each instance of the Inner Classes reference their Outer
        /// classes and call their "restore()" method, by passing themselves as a parameter[i.e. by passing "this"].
        /// This way, the Inner Classes themselves will be in charge of finding the Piece instance
        /// that had generated them, simplifying the duty of the Caretaker and the other external
        /// classes which shouldn't bother with these associations.
        /// </summary>
        void Restore();
    }
}
