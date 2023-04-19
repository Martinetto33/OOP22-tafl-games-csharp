namespace TaflGames.Code
{
    /// <summary>
    /// This interface models the cells of the Board.
    /// Allows to know the type of the cell,
    /// if a cell is free and if it can accept a certain type of piece.
    /// Let set the status of the cell.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Verify if a Piece is allowed to move on the cell.
        /// </summary>
        /// <param name="piece"> 
        /// the Piece to verify. 
        /// </param>
        /// <returns>
        /// true if the Piece can move on the cell, false otherwise.
        /// </returns>
        bool CanAccept(IPieceMock piece);

        /// <summary>
        /// Set the status of the cell to free or occupied.
        /// </summary>
        /// <param name="cellStatus">
        /// true if the cell is free, false otherwise.
        /// </param>
        void SetFree(bool cellStatus);

        /// <summary>
        /// Return the type of the cell.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Check if a cell is free or not.
        /// </summary>
        bool IsFree { get; }
    }
}
