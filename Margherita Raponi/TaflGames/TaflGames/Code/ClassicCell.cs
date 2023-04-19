
using TaflGames.Code;

namespace TaflGames
{
    /// <summary>
    /// This class models a classic cell.
    /// </summary>
    public class ClassicCell : AbstractCell
    {
        private const string CellType = "ClassicCell";

        /// <inheritdoc/>
        public override bool CanAccept(IPieceMock piece) => IsFree;

        /// <inheritdoc/>
        public override string Type => CellType;
    }
}
