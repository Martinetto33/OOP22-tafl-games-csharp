using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tafl_games.api;

namespace Tafl_games.code
{
    /// <summary>
    /// A class modelling a simple Cell, that can be free or occupied.
    /// </summary>
    public class CellExample
    {
        /// <summary>
        /// The status of the cell (free or occupied).
        /// </summary>
        public bool CellStatus {  get; set; }

        /// <summary>
        /// Builds a new CellExample.
        /// </summary>
        public CellExample() => CellStatus = true;

        /// <summary>
        /// Saves the state of this CellExample.
        /// </summary>
        /// <returns>
        /// a <see cref="ICellMemento"/> describing the current state
        /// of this Cell.
        /// </returns>
        public ICellMemento Save() => new CellMementoImpl(this);

        /// <summary>
        /// Restores the state of this cell to the one contained in
        /// the <see cref="ICellMemento"/> parameter.
        /// </summary>
        /// <param name="cm">
        /// a <see cref="ICellMemento"/> containing the state
        /// to revert to.
        /// </param>
        private void Restore(ICellMemento cm) => CellStatus = cm.GetCellStatus();

        /// <summary>
        /// A class modelling a memento for a Cell.
        /// </summary>
        public class CellMementoImpl : ICellMemento
        {
            private bool InnerCellStatus { get; }
            private readonly CellExample _parent;

            /// <summary>
            /// Builds a new CellMementoImpl.
            /// </summary>
            /// <param name="parent">
            /// the Cell that originated this Memento. This way,
            /// restoration to previous states is made easier.
            /// </param>
            public CellMementoImpl(CellExample parent) 
            {
                /* In C# inner classes do not store a reference to their
                 parent automatically, so the parent is directly passed. */
                _parent = parent;
                InnerCellStatus = parent.CellStatus;
            }

            /// <inheritdoc/>
            public bool GetCellStatus() => InnerCellStatus;

            /// <inheritdoc/>
            public void Restore() => _parent.Restore(this);
        }
    }
}
