namespace TaflGames.Code
{
    public abstract class AbstractCell : ICell
    {
        private bool _cellStatus;

        public AbstractCell() => _cellStatus = true;

        public bool IsFree => _cellStatus;

        public void SetFree(bool cellStatus) => _cellStatus = cellStatus;

        public abstract bool CanAccept(IPiece piece);

        public abstract string Type { get; }
    }
}
