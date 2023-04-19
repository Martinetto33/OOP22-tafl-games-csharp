
namespace TaflGames.Code
{
    /// <summary>
    /// A mock interface that models the pieces of the game.
    /// </summary>
    public interface IPieceMock
    {
        /// <summary>
        /// Checks if the piece is alive or not.
        /// Return true if current number of lives is greater than 0, false otherwise
        /// </summary>
        bool IsAlive { get; }
    }
}
