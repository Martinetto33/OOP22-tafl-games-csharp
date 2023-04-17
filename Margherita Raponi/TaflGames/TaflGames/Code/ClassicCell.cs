
using TaflGames.Code;

namespace TaflGames
{
    public class ClassicCell : AbstractCell
    {
        private const string CellType = "ClassicCell";

        public override bool CanAccept(IPiece piece) => IsFree;

        public override string Type => CellType;
    }
}
