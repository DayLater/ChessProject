using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject.Figures
{
    [TestFixture]
    public class TestElephant
    {
        Player player = new Player("White");
        IFigure[,] map = new IFigure[8, 8];
        [Test]
        public void FindPosibleWays()
        {
            var expected = new[]
            { 
                new Position(4,0), new Position(6, 0), new Position(4,2),
                new Position(6,2), new Position(3,3), new Position(7,3),
                new Position(2,4), new Position(1,5),  new Position(0,6)
            };
            var elephant = new Elephant(new Position(5, 1), player);
            map[5, 1] = elephant;
            var result = elephant.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }
    }
}
