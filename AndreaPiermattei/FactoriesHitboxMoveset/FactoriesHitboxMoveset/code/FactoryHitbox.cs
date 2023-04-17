using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FactoriesHitboxMoveset.api;

namespace FactoriesHitboxMoveset.code
{
    public class FactoryHitbox : IFactoryHitbox
    {
        ISet<Position> CreateBasicHitboxDistance(int distance)
        {
            if (distance < 0)
            {
                throw new ArgumentException("distance not valid");
            }
            ISet<Position> s = new HashSet<>();
            s.Add(new Position(distance, 0));
            s.Add(new Position(-distance, 0));
            s.Add(new Position(0, distance));
            s.Add(new Position(0, -distance));
            return s;
        }
        ISet<Position> CreateBasicHitbox() => CreateBasicHitboxDistance(1);

        ISet<Position> CreateArcherHitbox(int range)
        {
            if (range < 0)
            {
                throw new ArgumentException("range not valid");
            }
            ISet<Position> f = new HashSet<>();
            for (int i = 1;  i <= range; i++) 
            { 
                f.UnionWith(CreateBasicHitboxDistance(i));
            }
            return f;
        }
    }
}
