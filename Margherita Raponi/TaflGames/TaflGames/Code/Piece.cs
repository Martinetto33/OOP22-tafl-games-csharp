
namespace TaflGames.Code
{
    public class Piece:IPiece
    {
        private Position _pos;
        private Player _player;

        public Piece(Position pos, Player p)
        {
            _pos = pos;
            _player = p;
        }

        public bool IsAlive => true;

        public override bool Equals(object? obj)
        {
            return obj is Piece piece &&
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
