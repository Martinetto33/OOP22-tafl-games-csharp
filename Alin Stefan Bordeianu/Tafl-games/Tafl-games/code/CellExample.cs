using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tafl_games.api;

namespace Tafl_games.code
{
    public class CellExample
    {
        public bool CellStatus {  get; set; }

        public CellExample() => CellStatus = true;

        public ICellMemento Save() => new CellMementoImpl(this);

        private void Restore(ICellMemento cm) => CellStatus = cm.GetCellStatus();

        public class CellMementoImpl : ICellMemento
        {
            private bool InnerCellStatus { get; }
            private readonly CellExample _parent;

            public CellMementoImpl(CellExample parent) 
            {
                /* In C# inner classes do not store a reference to their
                 parent automatically, so the parent is directly passed. */
                _parent = parent;
                InnerCellStatus = parent.CellStatus;
            }
            public bool GetCellStatus() => InnerCellStatus;

            public void Restore() => _parent.Restore(this);
        }
    }
}
