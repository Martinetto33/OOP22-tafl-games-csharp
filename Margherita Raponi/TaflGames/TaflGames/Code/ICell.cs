namespace TaflGames.Code
{
    public interface ICell
    {
        bool CanAccept(IPiece piece);

        void SetFree(bool cellStatus);

        string Type { get; }

        bool IsFree { get; }
    }
}
