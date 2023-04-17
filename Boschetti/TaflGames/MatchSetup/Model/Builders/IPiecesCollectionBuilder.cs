using Common;
using Pieces;

namespace Builders
{
    /// <summary>
    /// This interface allows to interact with a builder to create a collection of pieces.
    /// </summary>
    public interface IPiecesCollectionBuilder
    {
        /// <summary>
        /// Adds the king piece to the pieces collection being built.
        /// </summary>
        /// <param name="position">the position where the king must be placed</param>
        void AddKing(IPosition position);
        
        /// <summary>
        /// Adds the queen pieces to the pieces collection being built.
        /// </summary>
        /// <param name="positions">the position where the queens must be placed</param>
        void AddQueens(IDictionary<Player, ISet<IPosition>> positions);

        /// <summary>
        /// Adds the archer pieces to the pieces collection being built.
        /// </summary>
        /// <param name="positions">the position where the archers must be placed</param>
        void AddArchers(IDictionary<Player, ISet<IPosition>> positions);

        /// <summary>
        /// Adds the shield pieces to the pieces collection being built.
        /// </summary>
        /// <param name="positions">the position where the shields must be placed</param>
        void AddShields(IDictionary<Player, ISet<IPosition>> positions);

        /// <summary>
        /// Adds the swapper pieces to the pieces collection being built.
        /// </summary>
        /// <param name="positions">the position where the swappers must be placed</param>
        void AddSwappers(IDictionary<Player, ISet<IPosition>> positions);

        /// <summary>
        /// Adds the basic pieces to the pieces collection being built.
        /// </summary>
        /// <param name="positions">the position where the basic pieces must be placed</param>
        void AddBasicPieces(IDictionary<Player, ISet<IPosition>> positions);

        /// <returns>the collection of pieces that has been set up</returns>
        IDictionary<Player, IDictionary<IPosition, IPiece>> Build();
    }
}
