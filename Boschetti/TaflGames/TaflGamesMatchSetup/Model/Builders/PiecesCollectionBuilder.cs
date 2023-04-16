using Common;
using Pieces;

namespace Builders
{
    /// <summary>
    /// This class implements a builder to create a collection of pieces.
    /// </summary>
    public class PiecesCollectionBuilder : IPiecesCollectionBuilder
    {
        private readonly IDictionary<Player, IDictionary<IPosition, IPiece>> _pieces;

        /// <summary>
        /// Creates a new builder for a collection of pieces.
        /// </summary>
        public PiecesCollectionBuilder()
        {
            _pieces = new Dictionary<Player, IDictionary<IPosition, IPiece>>
            {
                { Player.Attacker, new Dictionary<IPosition, IPiece>() },
                { Player.Defender, new Dictionary<IPosition, IPiece>() }
            };
        }

        public void AddKing(IPosition position) => _pieces[Player.Defender].Add(position, new King());

        public void AddQueens(IDictionary<Player, ISet<IPosition>> positions)
        {
            foreach (var (player, playerPositions) in positions)
            {
                playerPositions.ToList().ForEach(pos => _pieces[player].Add(pos, new Queen()));
            }
        }

        public void AddArchers(IDictionary<Player, ISet<IPosition>> positions)
        {
            foreach (var (player, playerPositions) in positions)
            {
                playerPositions.ToList().ForEach(pos => _pieces[player].Add(pos, new Archer()));
            }
        }

        public void AddBasicPieces(IDictionary<Player, ISet<IPosition>> positions)
        {
            foreach (var (player, playerPositions) in positions)
            {
                playerPositions.ToList().ForEach(pos => _pieces[player].Add(pos, new BasicPiece()));
            }
        }

        public void AddShields(IDictionary<Player, ISet<IPosition>> positions)
        {
            foreach (var (player, playerPositions) in positions)
            {
                playerPositions.ToList().ForEach(pos => _pieces[player].Add(pos, new Shield()));
            }
        }

        public void AddSwappers(IDictionary<Player, ISet<IPosition>> positions)
        {
            foreach (var (player, playerPositions) in positions)
            {
                playerPositions.ToList().ForEach(pos => _pieces[player].Add(pos, new Swapper()));
            }
        }

        public IDictionary<Player, IDictionary<IPosition, IPiece>> Build()
        {
            return new Dictionary<Player, IDictionary<IPosition, IPiece>>(_pieces);
        }
    }
}
