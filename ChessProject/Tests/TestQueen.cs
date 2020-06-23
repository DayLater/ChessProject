using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject.Figures
{
    [TestFixture]
    public class TestQueen
    {
        readonly Player whitePlayer = new Player(PlayerColor.White);
        readonly Player blackPlayer = new Player(PlayerColor.Black);

        [Test]
        public void FindPosibleWaysWithoutEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Queen queen = new Queen(new Position(5, 1), whitePlayer);
            var expected = new List<Position>()
            {
                new Position(4,0), new Position(6, 0), new Position(4,2),
                new Position(6,2), new Position(3,3), new Position(7,3),
                new Position(2,4), new Position(1,5),  new Position(0,6)
            };

            for (int i = 0; i < 8; i++)
                if (i != 1)
                    expected.Add(new Position(5, i));
            for (int i = 0; i < 8; i++)
                if (i != 5)
                    expected.Add(new Position(i, 1));

            map[5, 1] = queen;
            var result = queen.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Queen queen = new Queen(new Position(5, 1), whitePlayer);
            var elephantEnemy = new Elephant(new Position(4, 2), blackPlayer);
            map[4, 2] = elephantEnemy;
            var result = queen.FindPosibleWays(map);
            var expected = new List<Position>()
            {
                new Position(4,0), new Position(6, 0), new Position(4,2),
                new Position(6,2), new Position(7,3),
            };

            for (int i = 0; i < 8; i++)
                if (i != 1)
                    expected.Add(new Position(5, i));
            for (int i = 0; i < 8; i++)
                if (i != 5)
                    expected.Add(new Position(i, 1));
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithAnyEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Queen queen = new Queen(new Position(5, 1), whitePlayer);
            var elephantEnemy = new Elephant(new Position(4, 2), blackPlayer);
            var elephant2Enemy = new Elephant(new Position(5, 2), blackPlayer);
            map[4, 2] = elephantEnemy;
            map[5, 2] = elephant2Enemy;
            var result = queen.FindPosibleWays(map);
            var expected = new List<Position>()
            {
                new Position(4, 0), new Position(6, 0), new Position(4, 2), new Position(5, 2),
                new Position(6, 2), new Position(7, 3), new Position(5, 0)
            };
            for (int i = 0; i < 8; i++)
                if (i != 5)
                    expected.Add(new Position(i, 1));

            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }
    }
}