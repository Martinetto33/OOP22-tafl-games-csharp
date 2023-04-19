namespace TaflGames.Code
{
    /// <summary>
    /// This class implements the state of a cell regardless of its type
    /// and the basic functionalities which are implemented in the same way
    /// for all the cells.
    /// </summary>
    public abstract class AbstractCell : ICell
    {
        private bool _cellStatus;

        /// <summary>
        /// Builds a new AbstractCell and sets its status to 'free' (true).
        /// </summary>
        public AbstractCell() => _cellStatus = true;

        /// <inheritdoc/>
        public bool IsFree => _cellStatus;

        /// <inheritdoc/>
        public void SetFree(bool cellStatus) => _cellStatus = cellStatus;

        /// <inheritdoc/>
        public abstract bool CanAccept(IPieceMock piece);

        /// <inheritdoc/>
        public abstract string Type { get; }
    }
}
