﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject.Figures
{
    [TestFixture]
    public class TestRook
    {
        Player whitePlayer = new Player(PlayerColor.White);
        Player blackPlayer = new Player(PlayerColor.Black);

        [Test]
        public void FindPosibleWaysWithoutEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Rook rook = new Rook(new Position(2, 1), whitePlayer);
            var expected = new[]
            {
              new Position(2,0), new Position(0,1), new Position(1,1), new Position(3,1),
              new Position(4,1), new Position(2,2), new Position(2,3), new Position(2,4),
              new Position(2,5), new Position(2,6), new Position(2,7), new Position(5,1),
              new Position(6,1), new Position(7,1)
            };

            map[2, 1] = rook;
            var result = rook.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            IFigure[,] map = new IFigure[8, 8];
            Elephant elephantWhite = new Elephant(new Position(5, 1), whitePlayer);
            Elephant elephantBlack = new Elephant(new Position(2, 4), blackPlayer);
            Rook rookWhite = new Rook(new Position(2, 1), whitePlayer);
            var expected = new[]
            {
              new Position(2, 0), new Position(0, 1), new Position(1, 1), new Position(3, 1),
              new Position(4, 1), new Position(2, 2), new Position(2, 3), new Position(2, 4)
            };

            map[5, 1] = elephantWhite;
            map[2, 4] = elephantBlack;
            map[2, 1] = rookWhite;
            var result = rookWhite.FindPosibleWays(map);
            Assert.That(result.ToArray(),
                Is.EquivalentTo(expected));
        }
    }
}
