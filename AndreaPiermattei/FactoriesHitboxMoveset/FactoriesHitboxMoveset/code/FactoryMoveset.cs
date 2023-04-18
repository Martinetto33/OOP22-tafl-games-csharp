using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FactoriesHitboxMoveset.api;

namespace FactoriesHitboxMoveset.code
{
    public class FactoryMoveset : IFactoryMoveset
    {
        public HashSet<Vector> CreateBasicMoveSet()
        {
            HashSet<Vector> s = new();
            s.Add(new Vector(new Position(0, 0), new Position(1,0), true));
            s.Add(new Vector(new Position(0, 0), new Position(0, 1), true));
            s.Add(new Vector(new Position(0, 0), new Position(-1, 0), true));
            s.Add(new Vector(new Position(0, 0), new Position(0, -1), true));
            return s;
        }
        public HashSet<Vector> CreateSwapperMoveSet(HashSet<Position> enemyPositions)
        {
            if (enemyPositions is null)
            {
                throw new ArgumentException("enemyPosition not valid");
            }
            HashSet<Vector> s = new();
            s.UnionWith(enemyPositions.Select(p => new Vector(new Position(0, 0), p, false))
                .ToHashSet());
            s.UnionWith(CreateBasicMoveSet());
            return s;
        }
    }
}
