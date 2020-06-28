using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessProject
{
    [TestFixture]
    public class TestStalemate
    {
        [Test]
        public void MirrorStalemate() //зеркальный пат
        {
            var game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[0, 4] = new Horse(new Position(0, 4), game.CurrentPlayer);
            map[1, 0] = new Elephant(new Position(1, 0), game.CurrentPlayer);
            map[1, 7] = new Elephant(new Position(1, 7), game.CurrentPlayer);
            map[3, 0] = new Pawn(new Position(3, 0), game.CurrentPlayer);
            map[7, 2] = new Rook(new Position(7, 2), game.CurrentPlayer);
            map[7, 3] = new King(new Position(7, 3), game.CurrentPlayer);
            map[7, 4] = new Rook(new Position(7, 4), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 3] = new King(new Position(3, 3), game.CurrentPlayer);
            map[2, 0] = new Pawn(new Position(2, 0), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void EconomicalStalemate() //экономичный пат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[0, 0] = new Rook(new Position(0, 0), game.CurrentPlayer);
            map[2, 6] = new King(new Position(2, 6), game.CurrentPlayer);
            map[1, 7] = new Pawn(new Position(1, 7), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 7] = new King(new Position(0, 7), game.CurrentPlayer);
            map[0, 3] = new Elephant(new Position(0, 3), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void AnandVersusKramnikStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[3, 7] = new King(new Position(3, 7), game.CurrentPlayer);
            map[4, 7] = new Pawn(new Position(4, 7), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 5] = new King(new Position(3, 5), game.CurrentPlayer);
            map[1, 6] = new Pawn(new Position(1, 6), game.CurrentPlayer);
            map[2, 5] = new Pawn(new Position(2, 5), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void KorchnoiVersusKarpovStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[1, 5] = new King(new Position(1, 5), game.CurrentPlayer);
            map[1, 6] = new Elephant(new Position(1, 6), game.CurrentPlayer);
            map[5, 0] = new Pawn(new Position(5, 0), game.CurrentPlayer);
            game.SwapPlayer();
            map[1, 7] = new King(new Position(1, 7), game.CurrentPlayer);
            map[4, 0] = new Pawn(new Position(4, 0), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void BernsteinVersusSmyslovStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[5, 5] = new King(new Position(5, 5), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 5] = new King(new Position(3, 5), game.CurrentPlayer);
            map[4, 5] = new Pawn(new Position(4, 5), game.CurrentPlayer);
            map[6, 1] = new Rook(new Position(6, 1), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }
        
        [Test]
        public void MatulovicVersusMinevStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[5, 7] = new King(new Position(5, 7), game.CurrentPlayer);
            map[4, 5] = new Pawn(new Position(4, 5), game.CurrentPlayer);
            map[2, 0] = new Rook(new Position(2, 0), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 7] = new King(new Position(3, 7), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void WilliamsVersusHarrwitzStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[7, 0] = new King(new Position(7, 0), game.CurrentPlayer);
            game.SwapPlayer();
            map[4, 2] = new King(new Position(4, 2), game.CurrentPlayer);
            map[6, 0] = new Pawn(new Position(6, 0), game.CurrentPlayer);
            map[5, 1] = new Rook(new Position(5, 1), game.CurrentPlayer);
            map[5, 2] = new Horse(new Position(5, 2), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void CarlsenVersusVanWelyStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[7, 3] = new King(new Position(7, 3), game.CurrentPlayer);
            game.SwapPlayer();
            map[5, 3] = new King(new Position(5, 3), game.CurrentPlayer);
            map[6, 3] = new Elephant(new Position(6, 3), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void EvansVersusReshevskyStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[7, 7] = new King(new Position(7, 7), game.CurrentPlayer);
            map[4, 4] = new Pawn(new Position(4, 4), game.CurrentPlayer);
            map[5, 5] = new Pawn(new Position(5, 5), game.CurrentPlayer);
            map[4, 7] = new Pawn(new Position(4, 7), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 6] = new King(new Position(0, 6), game.CurrentPlayer);
            map[4, 5] = new Horse(new Position(4, 5), game.CurrentPlayer);
            map[3, 4] = new Pawn(new Position(3, 4), game.CurrentPlayer);
            map[3, 7] = new Pawn(new Position(3, 7), game.CurrentPlayer);
            map[6, 4] = new Rook(new Position(6, 4), game.CurrentPlayer);
            map[1, 6] = new Queen(new Position(1, 6), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void TroitskyVersusVogtStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[7, 6] = new King(new Position(7, 6), game.CurrentPlayer);
            map[4, 0] = new Pawn(new Position(4, 0), game.CurrentPlayer);
            map[3, 1] = new Pawn(new Position(3, 1), game.CurrentPlayer);
            map[4, 4] = new Pawn(new Position(4, 4), game.CurrentPlayer);
            map[6, 5] = new Pawn(new Position(6, 5), game.CurrentPlayer);
            map[6, 7] = new Pawn(new Position(6, 7), game.CurrentPlayer);
            map[5, 6] = new Horse(new Position(5, 6), game.CurrentPlayer);
            map[7, 7] = new Rook(new Position(7, 7), game.CurrentPlayer);
            map[7, 4] = new Elephant(new Position(7, 4), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 3] = new King(new Position(0, 3), game.CurrentPlayer);
            map[3, 0] = new Pawn(new Position(3, 0), game.CurrentPlayer);
            map[1, 1] = new Pawn(new Position(1, 1), game.CurrentPlayer);
            map[1, 2] = new Pawn(new Position(1, 2), game.CurrentPlayer);
            map[3, 4] = new Pawn(new Position(3, 4), game.CurrentPlayer);
            map[1, 5] = new Pawn(new Position(1, 5), game.CurrentPlayer);
            map[1, 6] = new Pawn(new Position(1, 6), game.CurrentPlayer);
            map[2, 6] = new Rook(new Position(2, 6), game.CurrentPlayer);
            map[7, 3] = new Queen(new Position(7, 3), game.CurrentPlayer);
            map[2, 1] = new Elephant(new Position(2, 1), game.CurrentPlayer);
            map[5, 7] = new Elephant(new Position(5, 7), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }
        public static void ClearMap(IFigure[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = null;
        }
    }
}
