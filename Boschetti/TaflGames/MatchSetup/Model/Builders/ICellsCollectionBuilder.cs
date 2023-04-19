using MatchSetup.Model.Cells;
using MatchSetup.Common;

namespace MatchSetup.Model.Builders
{
    /// <summary>
    /// This interface allows to intercat with a builder that creates
    /// a collection of cells.
    /// </summary>
    public interface ICellsCollectionBuilder
    {
        /// <summary>
        /// Registers the size of the board.
        /// </summary>
        /// <param name="boardSize">the size of the board</param>
        void AddBoardSize(int boardSize);

        /// <summary>
        /// Adds the throne cell to the cells collection being built.
        /// </summary>
        /// <param name="position">the position where the throne must be placed</param>
        void AddThrone(IPosition position);

        /// <summary>
        /// Adds the exit cells to the cells collection being built.
        /// </summary>
        /// <param name="positions">the position where the exits must be placed</param>
        void AddExits(ISet<IPosition> positions);

        /// <summary>
        /// Adds the slider cells to the cells collection being built.
        /// </summary>
        /// <param name="positions">the position where the sliders must be placed</param>
        void AddSliders(ISet<IPosition> positions);

        /// <summary>
        /// Adds the basic cells to the cells collection being built.
        /// </summary>
        void AddClassicCells();

        /// <returns>the collection of cells that has been set up</returns>
        IDictionary<IPosition, ICell> Build();
    }
}
