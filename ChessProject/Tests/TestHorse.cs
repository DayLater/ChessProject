using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject.Figures
{

    [TestFixture]
    public class TestHorse
    {
        Player whitePlayer = new Player(PlayerColor.White);
        Player blackPlayer = new Player(PlayerColor.Black);

        [Test]
        public void FindPosibleWaysWithoutEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Horse horse = new Horse(new Position(2, 2), whitePlayer);
            var expected = new[]
            {
               new Position(1, 0),new Position(0, 1),new Position(3, 0),new Position(4, 1),
               new Position(0, 3),new Position(1, 4),new Position(3, 4),new Position(4, 3 )
            };

            map[2, 2] = horse;
            var result = horse.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Elephant elephantWhite = new Elephant(new Position(3, 4), whitePlayer);
            Elephant elephantBlack = new Elephant(new Position(1, 0), blackPlayer);
            Horse horseWhite = new Horse(new Position(2, 2), whitePlayer);
            var expected = new[]
            {
               new Position(1, 0), new Position(0, 1), new Position(3, 0), new Position(4, 1),
               new Position(0, 3), new Position(1, 4), new Position(4, 3 )
            };

            map[3, 4] = elephantWhite;
            map[1, 0] = elephantBlack;
            map[2, 2] = horseWhite;
            var result = horseWhite.FindPosibleWays(map);
            Assert.That(result.ToArray(), 
                Is.EquivalentTo(expected));
        }
    }
}
