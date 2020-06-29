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
            map.Clear();
            game.Start();
            map.Add(new Horse(new Position(0, 4), game.CurrentPlayer));
            map.Add(new Elephant(new Position(1, 0), game.CurrentPlayer));
            map.Add(new Elephant(new Position(1, 7), game.CurrentPlayer));
            map.Add(new Pawn(new Position(3, 0), game.CurrentPlayer));
            map.Add(new Rook(new Position(7, 2), game.CurrentPlayer));
            map.Add(new King(new Position(7, 3), game.CurrentPlayer));
            map.Add(new Rook(new Position(7, 4), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(3, 3), game.CurrentPlayer));
            map.Add(new Pawn(new Position(2, 0), game.CurrentPlayer));
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void EconomicalStalemate() //экономичный пат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new Rook(new Position(0, 0), game.CurrentPlayer));
            map.Add(new King(new Position(2, 6), game.CurrentPlayer));
            map.Add(new Pawn(new Position(1, 7), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(0, 7), game.CurrentPlayer));
            map.Add(new Elephant(new Position(0, 3), game.CurrentPlayer));
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void AnandVersusKramnikStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(3, 7), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 7), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(3, 5), game.CurrentPlayer));
            map.Add(new Pawn(new Position(1, 6), game.CurrentPlayer));
            map.Add(new Pawn(new Position(2, 5), game.CurrentPlayer));
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void KorchnoiVersusKarpovStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(1, 5), game.CurrentPlayer));
            map.Add(new Elephant(new Position(1, 6), game.CurrentPlayer));
            map.Add(new Pawn(new Position(5, 0), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(1, 7), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 0), game.CurrentPlayer));
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void BernsteinVersusSmyslovStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(5, 5), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(3, 5), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 5), game.CurrentPlayer));
            map.Add(new Rook(new Position(6, 1), game.CurrentPlayer));
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }
        
        [Test]
        public void MatulovicVersusMinevStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(5, 7), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 5), game.CurrentPlayer));
            map.Add(new Rook(new Position(2, 0), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(3, 7), game.CurrentPlayer));
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void WilliamsVersusHarrwitzStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(7, 0), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(4, 2), game.CurrentPlayer));
            map.Add(new Pawn(new Position(6, 0), game.CurrentPlayer));
            map.Add(new Rook(new Position(5, 1), game.CurrentPlayer));
            map.Add(new Horse(new Position(5, 2), game.CurrentPlayer));
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void CarlsenVersusVanWelyStalemate() 
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(7, 3), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(5, 3), game.CurrentPlayer));
            map.Add(new Elephant(new Position(6, 3), game.CurrentPlayer));
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void EvansVersusReshevskyStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(7, 7), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 4), game.CurrentPlayer));
            map.Add(new Pawn(new Position(5, 5), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 7), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(0, 6), game.CurrentPlayer));
            map.Add(new Horse(new Position(4, 5), game.CurrentPlayer));
            map.Add(new Pawn(new Position(3, 4), game.CurrentPlayer));
            map.Add(new Pawn(new Position(3, 7), game.CurrentPlayer));
            map.Add(new Rook(new Position(6, 4), game.CurrentPlayer));
            map.Add(new Queen(new Position(1, 6), game.CurrentPlayer));
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }

        [Test]
        public void TroitskyVersusVogtStalemate()
        {
            GameModel game = new GameModel();
            var map = game.Map;
            map.Clear();
            game.Start();
            map.Add(new King(new Position(7, 6), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 0), game.CurrentPlayer));
            map.Add(new Pawn(new Position(3, 1), game.CurrentPlayer));
            map.Add(new Pawn(new Position(4, 4), game.CurrentPlayer));
            map.Add(new Pawn(new Position(6, 5), game.CurrentPlayer));
            map.Add(new Pawn(new Position(6, 7), game.CurrentPlayer));
            map.Add(new Horse(new Position(5, 6), game.CurrentPlayer));
            map.Add(new Rook(new Position(7, 7), game.CurrentPlayer));
            map.Add(new Elephant(new Position(7, 4), game.CurrentPlayer));
            game.SwapPlayer();
            map.Add(new King(new Position(0, 3), game.CurrentPlayer));
            map.Add(new Pawn(new Position(3, 0), game.CurrentPlayer));
            map.Add(new Pawn(new Position(1, 1), game.CurrentPlayer));
            map.Add(new Pawn(new Position(1, 2), game.CurrentPlayer));
            map.Add(new Pawn(new Position(3, 4), game.CurrentPlayer));
            map.Add(new Pawn(new Position(1, 5), game.CurrentPlayer));
            map.Add(new Pawn(new Position(1, 6), game.CurrentPlayer));
            map.Add(new Rook(new Position(2, 6), game.CurrentPlayer));
            map.Add(new Queen(new Position(7, 3), game.CurrentPlayer));
            map.Add(new Elephant(new Position(2, 1), game.CurrentPlayer));
            map.Add(new Elephant(new Position(5, 7), game.CurrentPlayer));
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsStalemate());
        }
    }
}
