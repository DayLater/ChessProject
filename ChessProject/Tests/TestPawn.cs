using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject.Figures
{
    [TestFixture]
    public class TestPawn
    {
        readonly Player whitePlayer = new Player(PlayerColor.White);
        readonly Player blackPlayer = new Player(PlayerColor.Black);

        [Test]
        public void FindPosibleWaysWithoutEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Pawn pawn = new Pawn(new Position(0, 6), whitePlayer);
            var expected = new[]
            {
               new Position(0, 5), new Position(0, 4)
            };
            
            map[0, 6] = pawn;
            var result = pawn.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Elephant elephant1 = new Elephant(new Position(1, 2), whitePlayer);
            Elephant elephant2 = new Elephant(new Position(3, 2), whitePlayer);
            Pawn pawn = new Pawn(new Position(2, 1), blackPlayer);
            var expected = new[]
            {
               new Position(1, 2), new Position(3, 2),
               new Position(2, 2), new Position(2, 3)
            };

            map[1, 2] = elephant1;
            map[3, 2] = elephant2;
            map[2, 1] = pawn;
            var result = pawn.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }
    }
}
