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
            var map = new Map();
            Pawn pawn = new Pawn(new Position(6, 0), whitePlayer);
            var expected = new[]
            {
               new Position(5, 0), new Position(4, 0)
            };
            
            map[6, 0] = pawn;
            var result = pawn.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            var map = new Map();
            Elephant elephant1 = new Elephant(new Position(2, 1), whitePlayer);
            Elephant elephant2 = new Elephant(new Position(2, 3), whitePlayer);
            Pawn pawn = new Pawn(new Position(1, 2), blackPlayer);
            var expected = new[]
            {
               new Position(2, 1), new Position(2, 3),
               new Position(2, 2), new Position(3, 2)
            };

            map[2, 1] = elephant1;
            map[2, 3] = elephant2;
            map[1, 2] = pawn;
            var result = pawn.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }
    }
}
