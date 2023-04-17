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
        }

        public override bool Equals(object? obj) => obj is Vector v &&
                StartPos == v.StartPos &&
                EndPos == v.EndPos;
        public override int GetHashCode() => HashCode.Combine(StartPos,
                                                              EndPos,
                                                              IsUnitVector);

    }
}

