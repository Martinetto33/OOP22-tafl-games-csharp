using MatchSetup.Model.Cells;
using MatchSetup.Common;

namespace MatchSetup.Model.Builders
{
    /// <summary>
    /// This class implements a builder to create a collection of cells.
    /// </summary>
    public class CellsCollectionBuilder : ICellsCollectionBuilder
    {
        private readonly IDictionary<IPosition, ICell> _cells;
        private int _boardSize;

        /// <summary>
        /// Creates a new builder for a collection of cells.
        /// </summary>
        public CellsCollectionBuilder() => _cells = new Dictionary<IPosition, ICell>();

        public void AddBoardSize(int boardSize) => _boardSize = boardSize;

        public void AddThrone(IPosition position) => _cells.Add(position, new Throne());

        public void AddExits(ISet<IPosition> positions)
        {
            positions.ToList().ForEach(pos => _cells.Add(pos, new Exit()));
        }

        public void AddSliders(ISet<IPosition> positions)
        {
            positions.ToList().ForEach(pos => _cells.Add(pos, new Slider()));
        }

        public void AddClassicCells()
        {
            foreach (int row in Enumerable.Range(0, _boardSize))
            {
                foreach (int column in Enumerable.Range(0, _boardSize))
                {
                    IPosition position = new Position(row, column);
                    if (!_cells.ContainsKey(position))
                    {
                        _cells.Add(position, new ClassicCell());
                    }
                }
            }
        }

        public IDictionary<IPosition, ICell> Build() => new Dictionary<IPosition, ICell>(_cells);

    }
}
