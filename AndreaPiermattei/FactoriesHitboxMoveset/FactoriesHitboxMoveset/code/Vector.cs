using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//simpler version just to test
namespace FactoriesHitboxMoveset.code
{
    public class Vector
    {
       
        public Position StartPos { get; }
        public Position EndPos { get; }
        public bool IsUnitVector { get; }

        public Vector(Position startPos, Position endPos, bool isUnitVector)
        {
            StartPos = startPos;
            EndPos = endPos;
            IsUnitVector = isUnitVector;
        }
        public override bool Equals(object? obj)
        {
            return obj is Vector
                && StartPos.Equals(((Vector)obj).StartPos)
                && EndPos.Equals(((Vector)obj).EndPos)
                && IsUnitVector == ((Vector)obj).IsUnitVector;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(StartPos, EndPos, IsUnitVector);
        }
    }
}

