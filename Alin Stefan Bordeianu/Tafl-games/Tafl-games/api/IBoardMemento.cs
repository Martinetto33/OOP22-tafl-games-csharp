using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tafl_games.api
{
    /// <summary>
    /// An interface modelling the behaviour that the Inner Class of the Board implementation
    /// should provide in order to save a snapshot of the current state of a Board. Said Inner
    /// Class should implement this interface.
    /// This interface is part of the pattern Memento.
    /// </summary>
    public interface IBoardMemento
    {

        /// <summary>
        /// Restores the state contained in this IBoardMemento.
        /// </summary>
        void Restore();

        /// <summary>
        /// Returns the saved states of all the Pieces, including the dead ones.
        /// The "restore()" method in the Board class should call the method
        /// "restore()" on each of these IPieceMemento, allowing them to go back
        /// to their previous state as well.
        /// </summary>
        /// <returns>
        /// a List of IPieceMemento.
        /// </returns>
        IList<IPieceMemento> GetPiecesMemento();

        /// <summary>
        /// Returns the saved states of all the Cells, including the inactive ones.
        /// The "restore()" method in the Board class should call the method
        /// "restore()" on each of these ICellMemento, allowing them to go back
        /// to their previous state as well.
        /// </summary>
        /// <returns>
        /// a List of ICellMemento.
        /// </returns>
        IList<ICellMemento> GetCellsMemento();
    }
}
