using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FactoriesHitboxMoveset.code;

namespace FactoriesHitboxMoveset.api
{
    public interface IFactoryMoveset
    {
        ISet<Vector> CreateBasicMoveSet();
        ISet<Vector> CreateSwapperMoveSet(ISet<Position> enemyPositions);
    }
}
