using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject.Tests
{
    [TestFixture]
    public class TestDraw
    {
        [Test]
        public void ImposibleMate()
        {
            var game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(0, 0), game.CurrentPlayer));
            map.Add(new Elephant(new Position(1, 1), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(7, 7), game.CurrentPlayer));
            //проверка на 2 короля и одного слона
            Assert.AreEqual(true, game.isImposibleMate());

            //проверка на 2-х королей
            map[1, 1] = null;
            Assert.AreEqual(true, game.isImposibleMate());

            //проверка на 2 короля и башню
            map.Add(new Rook(new Position(1, 1), game.CurrentPlayer));
            Assert.AreEqual(false, game.isImposibleMate());

            //проверка на коня и двух королей
            map[1, 1] = new Horse(new Position(1, 1), game.CurrentPlayer);
            Assert.AreEqual(true, game.isImposibleMate());
        }
    }
}
