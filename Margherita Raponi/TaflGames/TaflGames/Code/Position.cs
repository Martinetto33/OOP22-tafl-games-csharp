
namespace TaflGames.Code
{
    // This class was taken from the ones implemented by Alin Stefan Bordeianu. 
    /// <summary>
    /// A class modelling a 2D position.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Builds a new Position.
        /// </summary>
        /// <param name="xPosition">
        /// the X coordinate.
        /// </param>
        /// <param name="yPosition">
        /// the Y coordinate.
        /// </param>
        public Position(int xPosition, int yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }

        /// <summary>
        /// The X coordinate.
        /// </summary>
        public int XPosition { get; }

        /// <summary>
        /// The Y coordinate.
        /// </summary>
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
