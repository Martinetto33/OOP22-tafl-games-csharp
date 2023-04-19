using FactoriesHitboxMoveset.code;
using System.Collections.Generic;

namespace TestFactoryUff
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFactoryHitbox()
        {
            FactoryHitbox f = new();
            int distance2 = 2;
            HashSet<Position> s2 = new();
            s2.Add(new Position(distance2, 0));
            s2.Add(new Position(-distance2, 0));
            s2.Add(new Position(0, distance2));
            s2.Add(new Position(0, -distance2));
            Assert.IsTrue(f.CreateBasicHitboxDistance(distance2).SetEquals(s2));
            
            HashSet<Position> s = new();
            int distance = 1;
            s.Add(new Position(distance, 0));
            s.Add(new Position(-distance, 0));
            s.Add(new Position(0, distance));
            s.Add(new Position(0, -distance));
            Assert.IsTrue(f.CreateBasicHitbox().SetEquals(s));

            s.UnionWith(s2);
            Assert.IsTrue(f.CreateArcherHitbox(distance2).SetEquals(s));
        }
        [TestMethod]
        public void TestFactoryMoveset()
        {
            Vector a = new Vector(new Position(0, 0), new Position(9, 3), false);
            Vector a2 = new Vector(new Position(0, 0), new Position(8, 4), false);
            //just testing vector to be sure
            Assert.IsFalse(a.Equals(a2));
            FactoryMoveset f = new FactoryMoveset();
            HashSet<Vector> s = new()
            {
                new Vector(new Position(0, 0), new Position(1, 0), true),
                new Vector(new Position(0, 0), new Position(0, 1), true),
                new Vector(new Position(0, 0), new Position(0, -1), true),
                new Vector(new Position(0, 0), new Position(-1, 0), true)
            };
            Assert.IsTrue(f.CreateBasicMoveSet().SetEquals(s));
            s.Add(a);
            s.Add(a2);
            HashSet<Position> p = new()
            {
                a.EndPos,
                a2.EndPos
            };
            Assert.IsTrue(f.CreateSwapperMoveSet(p).SetEquals(s));
        }
    }
}