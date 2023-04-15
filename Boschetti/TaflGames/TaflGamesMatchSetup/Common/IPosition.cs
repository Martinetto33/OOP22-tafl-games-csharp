namespace Common
{
	/// <summary>
	/// This interface represents a couple of coordinates on the board.
	/// </summary>
	public interface IPosition
	{
		/// <summary>
		/// The row of the position.
		/// </summary>
		int X { get; }

		/// <summary>
		/// The column of the position.
		/// </summary>
		int Y { get; }
	}
}
