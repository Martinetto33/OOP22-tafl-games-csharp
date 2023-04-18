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
        public HashSet<Position> CreateBasicHitboxDistance(int distance);
        public HashSet<Position> CreateBasicHitbox();
        public HashSet<Position> CreateArcherHitbox(int range);
    }
}
