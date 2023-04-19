namespace MatchSetup.Common
{
    /// <summary>
    /// This class models a position on the board.
    /// </summary>
    public class Position : IPosition
    {
        /// <summary>
        /// Creates a new couple of coordinates given a row and a column as parameters.
        /// </summary>
        /// <param name="x">the row</param>
        /// <param name="y">the column</param>
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Creates a copy of the position passed as parameter.
        /// </summary>
        /// <param name="position">the position to copy</param>
        public Position(IPosition position)
        {
            X = position.X;
            Y = position.Y;
        }

        public int X { get; }
        public int Y { get; }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString() => $"Position ({X}, {Y})";
    }
}
