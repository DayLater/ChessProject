using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject
{
    [TestFixture]
    public class TestElephant
    {
        Player whitePlayer = new Player(PlayerColor.White);
        Player blackPlayer = new Player(PlayerColor.Black);
        IFigure[,] map = new IFigure[8, 8];

        [Test]
        public void FindPosibleWaysWithoutEnemies()
        {
            Elephant elephant = new Elephant(new Position(5, 1), whitePlayer);
            var expected = new[]
            {
                new Position(4,0), new Position(6, 0), new Position(4,2),
                new Position(6,2), new Position(3,3), new Position(7,3),
                new Position(2,4), new Position(1,5),  new Position(0,6)
            };
            map = new IFigure[8, 8];
            map[5, 1] = elephant;
            var result = elephant.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            Elephant elephant = new Elephant(new Position(5, 1), whitePlayer);
            var elephantEnemy = new Elephant(new Position(4, 2), blackPlayer);
            map[4, 2] = elephantEnemy;
            var result = elephant.FindPosibleWays(map);
            var expected = new[]
            {
                new Position(4,0), new Position(6, 0), new Position(4,2),
                new Position(6,2), new Position(7,3),
            };
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }
    }
}
