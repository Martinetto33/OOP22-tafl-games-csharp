
namespace TaflGames.Code
{
    /// <summary>
    /// A mock class that simulate a Piece of the game.
    /// </summary>
    public class PieceMock:IPieceMock
    {
        private Position _pos;
        private Player _player;

        /// <summary>
        /// Create a new MockPiece.
        /// </summary>
        /// <param name="pos"> 
        /// the current Posiotion of the Piece 
        /// </param>
        /// <param name="p"> 
        /// the player to whom the Piece belongs 
        /// </param>
        public PieceMock(Position pos, Player p)
        {
            _pos = pos;
            _player = p;
        }

        /// <inheritdoc/>
        public bool IsAlive => true;

        public override bool Equals(object? obj)
        {
            return obj is PieceMock piece &&
                   EqualityComparer<Position>.Default.Equals(_pos, piece._pos) &&
                   _player == piece._player &&
                   IsAlive == piece.IsAlive;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_pos, _player, IsAlive);
        }
    }
}
