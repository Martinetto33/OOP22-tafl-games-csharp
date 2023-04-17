
namespace TaflGames.Code
{
    public class Position
    {

        public Position(int xPosition, int yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }

        public int XPosition { get; }

        public int YPosition { get; }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   XPosition == position.XPosition &&
                   YPosition == position.YPosition;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(XPosition, YPosition);
        }
    }
}
