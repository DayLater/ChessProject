using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject.Figures
{
    [TestFixture]
    public class TestKing
    {
        readonly Player whitePlayer = new Player(PlayerColor.White);
        readonly Player blackPlayer = new Player(PlayerColor.Black);

        [Test]
        public void FindPosibleWaysWithoutEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            King king = new King(new Position(2, 2), whitePlayer);
            var expected = new[]
            {
               new Position(1, 1), new Position(2, 1), new Position(3, 1), new Position(1, 2),
               new Position(1, 3), new Position(2, 3), new Position(3, 3), new Position(3, 2)
            };

            map[2, 2] = king;
            var result = king.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            King king = new King(new Position(2, 2), whitePlayer);
            Elephant elephant = new Elephant(new Position(3, 1), whitePlayer);
            Pawn pawn = new Pawn(new Position(1, 1), blackPlayer);
            var expected = new[]
            {
               new Position(1, 1), new Position(2, 1), new Position(1, 2), new Position(3, 2),
               new Position(1, 3), new Position(2, 3), new Position(3, 3)
            };

            map[3, 1] = elephant;
            map[1, 1] = pawn;
            map[2, 2] = king;
            var result = king.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }
    }
}
