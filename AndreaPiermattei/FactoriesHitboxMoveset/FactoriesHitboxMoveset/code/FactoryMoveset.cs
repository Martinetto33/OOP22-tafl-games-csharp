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
        public ISet<Vector> CreateBasicMoveSet()
        {
            ISet<Vector> s = new HashSet<>();
            s.Add(new Vector(1, 0, true));
            s.Add(new Vector(0, 1, true));
            s.Add(new Vector(-1, 0, true));
            s.Add(new Vector(0, -1, true));
            return s;
        }
        public ISet<Vector> CreateSwapperMoveSet(ISet<Position> enemyPositions)
        {
            ISet<Position> s = new HashSet<>();
            s.UnionWith(enemyPositions.Select(p => new Vector(new Position(0, 0), p, false)));
            s.UnionWith(CreateBasicMoveSet());
            return s;
        }
    }
}
