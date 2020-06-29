﻿using System;
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
        readonly Player whitePlayer = new Player(PlayerColor.White);
        readonly Player blackPlayer = new Player(PlayerColor.Black);
        
        [Test]
        public void FindPosibleWaysWithoutEnemies()
        {
            var map = new Map();
            Elephant elephant = new Elephant(new Position(5, 1), whitePlayer);
            var expected = new[]
            {
                new Position(4, 0), new Position(6, 0), new Position(4, 2),
                new Position(6, 2), new Position(3, 3), new Position(7, 3),
                new Position(2, 4), new Position(1, 5), new Position(0, 6)
            };

            map[5, 1] = elephant;
            var result = elephant.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }

        [Test]
        public void FindPosibleWaysWithEnemies()
        {
            var map = new Map();
            Elephant elephant = new Elephant(new Position(5, 1), whitePlayer);
            var elephantEnemy = new Elephant(new Position(4, 2), blackPlayer);
            var expected = new[]
            {
                new Position(4, 0), new Position(6, 0), new Position(4, 2),
                new Position(6, 2), new Position(7, 3),
            };

            map[4, 2] = elephantEnemy;
            var result = elephant.FindPosibleWays(map);
            Assert.That(result.ToArray(), Is.EquivalentTo(expected));
        }
    }
}
