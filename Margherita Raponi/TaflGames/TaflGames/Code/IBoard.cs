
using System.Numerics;
using System;

namespace TaflGames.Code
{
    /// <summary>
    /// This interface models the board of the game.
    /// </summary>
    internal interface IBoard
    {
        /// <summary>
        /// Verify if a certain position is allowed for a certain player to start it's movement.
        /// </summary>
        /// <param name="start"> the starting position that must be controlled </param>
        /// <param name="player"> the player of wich starting position is controlled </param>
        /// <returns> true if the starting position is allowed, false if it's not allowed </returns>
        bool IsStartingPointValid(Position start, Player player);
    }
}
