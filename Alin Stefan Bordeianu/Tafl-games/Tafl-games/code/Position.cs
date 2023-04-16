using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tafl_games.code
{
    public class Position
    {
        public int XPosition { get; }
        public int YPosition { get; }

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
