using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoriesHitboxMoveset.code
{
    /// <summary> by Alin 
    /// A class modelling a 2D position.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// The X coordinate.
        /// </summary>
        public int XPosition { get; }

        /// <summary>
        /// The Y coordinate.
        /// </summary>
        public int YPosition { get; }

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
