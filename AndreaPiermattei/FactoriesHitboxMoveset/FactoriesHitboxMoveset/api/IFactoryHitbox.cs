using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FactoriesHitboxMoveset.code;

namespace FactoriesHitboxMoveset.api
{
    public interface IFactoryHitbox
    {
        ISet<Position> CreateBasicHitboxDistance(int distance);
        ISet<Position> CreateBasicHitbox();
        ISet<Position> CreateArcherHitbox();
    }
}
