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
            var map = new Map();
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
            var map = new Map();
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

        [Test]
        public void IsCastlingPosible()
        {
            var game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            var king = new King(new Position(7, 4), game.CurrentPlayer);
            var rook1 = new Rook(new Position(7, 0), game.CurrentPlayer);
            map.Add(king);
            map.Add(rook1);
            //проверяем возможность на дальнюю рокировку
            Assert.AreEqual(true, king.IsCastlingPosible(rook1, map));

            //проверяем возможность на ближнюю рокировку
            var rook2 = new Rook(new Position(7, 7), game.CurrentPlayer);
            Assert.AreEqual(true, king.IsCastlingPosible(rook2, map));

            //если между королем и башней стоит какая-то другая фигура
            map.Add(new Horse(new Position(7, 1), game.CurrentPlayer));
            Assert.AreEqual(false, king.IsCastlingPosible(rook1, map));

            //если между королем и башней стоит какая-то другая фигура
            map.Add(new Horse(new Position(7, 5), game.CurrentPlayer));
            Assert.AreEqual(false, king.IsCastlingPosible(rook2, map));

            //если шах королю
            map[7, 5] = null;
            game.SwapPlayer();
            map.Add(new Rook(new Position(5, 4), game.CurrentPlayer));
            Assert.AreEqual(false, king.IsCastlingPosible(rook2, map));

            //если шаха нет, но поле пробивается чужой фигурой
            map[5, 5] = map[5, 4];
            map[5, 4] = null;
            Assert.AreEqual(false, king.IsCastlingPosible(rook2, map));


            //если король сделал ход
            map.Clear();
            game.SwapPlayer();
            map.Add(king);
            map.Add(rook1);
            game.MakeTurn(new Position(6, 4), king);
            Assert.AreEqual(false, king.IsCastlingPosible(rook1, map));
            Assert.AreEqual(false, king.IsCastlingPosible(rook2, map));
        }
    }
}
