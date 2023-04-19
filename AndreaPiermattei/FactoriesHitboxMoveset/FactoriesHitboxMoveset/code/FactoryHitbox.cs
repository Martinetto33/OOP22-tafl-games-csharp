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
        public HashSet<Position> CreateBasicHitboxDistance(int distance)
        {
            if ( distance < 0 )
            {
                throw new ArgumentException("distance not valid");
            }
            HashSet<Position> s = new();
            s.Add(new Position(distance, 0));
            s.Add(new Position(-distance, 0));
            s.Add(new Position(0, distance));
            s.Add(new Position(0, -distance));
            return s;
        }
        public HashSet<Position> CreateBasicHitbox() 
        {
            return CreateBasicHitboxDistance(1);
        }

        public HashSet<Position> CreateArcherHitbox(int range)
        {
            if ( range < 0 )
            {
                throw new ArgumentException("range not valid");
            }
            HashSet<Position> f = new();
            for (int i = 1;  i <= range; i++) 
            { 
                f.UnionWith(CreateBasicHitboxDistance(i));
            }
            return f;
        }
    }
}
