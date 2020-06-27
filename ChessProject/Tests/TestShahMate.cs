﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;

namespace ChessProject
{
    // В папке ресурсов есть документ с удобным изображением тестов
    [TestFixture]
    class TestShahMate
    {

        [Test]
        public void ChildMate() //детский мат
        {
            GameModel game = new GameModel();
            game.Start();
            game.MakeTurn(new Position(1, 5), game.Map[7, 3]);
            game.MakeTurn(new Position(4, 2), game.Map[7, 5]);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void IdioticMate() //дурацкий мат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            game.MakeTurn(new Position(5, 5), map[6, 5]);
            game.MakeTurn(new Position(4, 6), map[6, 6]);
            game.MakeTurn(new Position(3, 4), map[1, 4]);
            game.MakeTurn(new Position(4, 7), map[0, 3]);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void MirrorMate() //зеркальный мат
        {
            var game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[7, 2] = new Rook(new Position(7, 2), game.CurrentPlayer); 
            map[7, 3] = new King(new Position(7, 3), game.CurrentPlayer);
            map[7, 4] = new Rook(new Position(7, 4), game.CurrentPlayer);
            map[6, 3] = new Queen(new Position(6, 3), game.CurrentPlayer);
            map[6, 0] = new Pawn(new Position(6, 0), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 3] = new King(new Position(3, 3), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void PerfectMate() // идиальный мат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[4, 6] = new King(new Position(4, 6), game.CurrentPlayer);
            map[5, 5] = new Horse(new Position(5, 5), game.CurrentPlayer);
            map[2, 2] = new Queen(new Position(2, 2), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 4] = new King(new Position(3, 4), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate());
        }


        [Test]
        public void LinearMate() // линейный мат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[5, 1] = new Rook(new Position(5,1), game.CurrentPlayer);
            map[4, 0] = new Rook(new Position(4, 0), game.CurrentPlayer);
            game.SwapPlayer();
            map[2, 0] = new King(new Position(2, 0), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void AnastisiaMate() // мат Анастасии
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[1, 4] = new Horse(new Position(1, 4), game.CurrentPlayer);
            map[1, 7] = new Rook(new Position(1, 7), game.CurrentPlayer);
            map[3, 7] = new Rook(new Position(3, 7), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 7] = new King(new Position(0, 7), game.CurrentPlayer);
            map[0, 5] = new Rook(new Position(0, 5), game.CurrentPlayer);
            map[1, 5] = new Pawn(new Position(1, 5), game.CurrentPlayer);
            map[1, 6] = new Pawn(new Position(1, 6), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void AndersonMate() // мат Андерсена
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[0, 3] = new Elephant(new Position(0, 3), game.CurrentPlayer);
            map[2, 6] = new King(new Position(2, 6), game.CurrentPlayer);
            map[5, 5] = new Pawn(new Position(5, 5), game.CurrentPlayer);
            map[6, 7] = new Pawn(new Position(6, 7), game.CurrentPlayer);
            game.SwapPlayer();
            map[4, 7] = new King(new Position(4, 7), game.CurrentPlayer);
            map[2, 7] = new Pawn(new Position(2, 7), game.CurrentPlayer);
            map[5, 7] = new Pawn(new Position(5, 7), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void BodenMate() // мат Бодена
        {
            GameModel game = new GameModel();
            var map = game.Map;
            ClearMap(map);
            game.Start();
            map[6, 0] = new Pawn(new Position(6, 0), game.CurrentPlayer);
            map[5, 2] = new Pawn(new Position(5, 2), game.CurrentPlayer);
            map[7, 2] = new King(new Position(7, 2), game.CurrentPlayer);
            map[7, 3] = new Rook(new Position(7, 3), game.CurrentPlayer);
            map[6, 3] = new Horse(new Position(6, 3), game.CurrentPlayer);
            map[3, 3] = new Elephant(new Position(3, 3), game.CurrentPlayer);
            map[5, 4] = new Elephant(new Position(5, 4), game.CurrentPlayer);
            game.SwapPlayer();
            map[5, 0] = new Elephant(new Position(5, 0), game.CurrentPlayer);
            map[3, 5] = new Elephant(new Position(3, 5), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void LegalMate() // мат Легаля
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            game.MakeTurn(new Position(3, 3), map[7, 1]);
            game.MakeTurn(new Position(3, 4), map[7, 6]);
            game.MakeTurn(new Position(1, 5), map[7, 5]);
            game.SwapPlayer();
            game.MakeTurn(new Position(2, 2), map[0, 1]);
            game.MakeTurn(new Position(7, 3), map[0, 2]);
            game.MakeTurn(new Position(1, 4), map[0, 4]);
            game.MakeTurn(new Position(2, 3), map[1, 3]);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void LastGorizontalMate() // мат на последней горизонтали
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            ClearMap(map);
            map[0, 0] = new Queen(new Position(0, 0), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 6] = new King(new Position(0, 6), game.CurrentPlayer);
            for (int i = 5; i < 8; i++)
                map[1, i] = new Pawn(new Position(1, i), game.CurrentPlayer);
            map[3, 3] = new Pawn(new Position(3, 3), game.CurrentPlayer);
            map[4, 3] = new Rook(new Position(4, 3), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate()); // не проходит

            map[3, 3] = null; //убираем пешку, закрывающую башню 
            Assert.AreEqual(false, game.IsMate()); //Мата не должно быть  - тест проходит
        }

        [Test]
        public void TroickiiMate() // мат Троицкого
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            ClearMap(map);
            map[1, 5] = new King(new Position(1, 5), game.CurrentPlayer);
            map[1, 6] = new Elephant(new Position(1, 6), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 7] = new King(new Position(0, 7), game.CurrentPlayer);
            map[1, 7] = new Pawn(new Position(1, 7), game.CurrentPlayer);
            map[3, 4] = new Pawn(new Position(3, 4), game.CurrentPlayer); //просто для заполнения
            Assert.AreEqual(true, game.IsMate()); 
        }

        [Test]
        public void CorrectMate() // правильный мат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            ClearMap(map);
            map[0, 2] = new Elephant(new Position(0, 2), game.CurrentPlayer);
            map[4, 2] = new Horse(new Position(4, 2), game.CurrentPlayer);
            map[4, 7] = new Queen(new Position(4, 7), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 4] = new King(new Position(3, 4), game.CurrentPlayer);
            map[3, 3] = new Horse(new Position(3, 3), game.CurrentPlayer);
            map[1, 0] = new Queen(new Position(1, 0), game.CurrentPlayer); //просто для заполнения
            Assert.AreEqual(true, game.IsMate());// проходит

            //вторая вариация этого теста
            map[3, 3] = new Elephant(new Position(3, 3), game.CurrentPlayer);
            game.SwapPlayer();
            map[3, 0] = new Rook(new Position(3, 0), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsMate()); //второй вариант этого мата

            //убираем башню, из-за которой в прошлом ассерте был мат. Теперь убить коня можно => мата нет
            map[3, 0] = null;
            Assert.AreEqual(false, game.IsMate());  // проходит 
        }

        [Test]
        public void SmotheredMate1() //Спертый мат. Простейшая версия
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            ClearMap(map);
            map[1, 5] = new Horse(new Position(1, 5), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 7] = new King(new Position(0, 7), game.CurrentPlayer);
            map[1, 6] = new Pawn(new Position(1, 6), game.CurrentPlayer);
            map[1, 7] = new Pawn(new Position(1, 7), game.CurrentPlayer);
            map[0, 6] = new Rook(new Position(0, 6), game.CurrentPlayer); //просто для заполнения
            Assert.AreEqual(true, game.IsMate());// проходит
        }

        [Test]
        public void SmotheredMate2() //Спертый мат - обычная версия, где мат из-за того, что нельзя сьесть вражескую фигуру
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            game.MakeTurn(new Position(7, 5), map[7, 7]);
            game.MakeTurn(new Position(1, 5), map[7, 6]);
            map[6, 6] = null;
            game.SwapPlayer();
            game.MakeTurn(new Position(5, 5), map[0, 1]);
            game.MakeTurn(new Position(4, 4), map[0, 3]);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void SmotheredMate3() //Спертый мат - обычная версия2, где мат из-за того, что нельзя сьесть вражескую фигуру
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            game.MakeTurn(new Position(2, 3), map[7, 1]);
            game.MakeTurn(new Position(6, 4), map[7, 3]);
            game.SwapPlayer();

            game.MakeTurn(new Position(1, 3), map[0, 1]);
            game.MakeTurn(new Position(2, 5), map[0, 6]);
            game.MakeTurn(new Position(2, 2), map[1, 2]);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void SmotheredMate4() //Спертый мат - обычный
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            game.MakeTurn(new Position(2, 3), map[7, 1]);
            map[7, 6] = null; 
            game.MakeTurn(new Position(3, 3), map[6, 3]);
            game.SwapPlayer();
            map[0, 1] = null;
            map[1, 2] = null;
            game.MakeTurn(new Position(1, 3), map[0, 2]); 
            game.MakeTurn(new Position(3, 4), map[1, 4]);
            game.MakeTurn(new Position(1, 4), map[0, 6]);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void ClearMate() //чистый мат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            ClearMap(map);
            game.SwapPlayer();
            map[0, 5] = new King(new Position(0, 5), game.CurrentPlayer);
            map[0, 6] = new Rook(new Position(0, 6), game.CurrentPlayer);
            map[1, 5] = new Pawn(new Position(1, 5), game.CurrentPlayer);
            map[1, 7] = new Pawn(new Position(1, 7), game.CurrentPlayer);
            map[5, 5] = new Queen(new Position(5, 5), game.CurrentPlayer);
            game.SwapPlayer();
            map[2, 5] = new Pawn(new Position(2, 5), game.CurrentPlayer);
            map[1, 3] = new Elephant(new Position(1, 3), game.CurrentPlayer);
            map[1, 4] = new Elephant(new Position(1, 4), game.CurrentPlayer);
            game.SwapPlayer();
            Assert.AreEqual(true, game.IsMate());
        }


        [Test]
        public void EconomicalMate() //экономичный мат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            ClearMap(map);

            map[0, 5] = new Rook(new Position(0, 5), game.CurrentPlayer);
            map[2, 0] = new Rook(new Position(2, 0), game.CurrentPlayer);
            map[4, 2] = new Elephant(new Position(4, 2), game.CurrentPlayer);
            map[5, 4] = new Pawn(new Position(5, 4), game.CurrentPlayer);
            map[5, 5] = new Pawn(new Position(5, 5), game.CurrentPlayer);
            map[5, 3] = new Horse(new Position(5, 3), game.CurrentPlayer);

            game.SwapPlayer();
            map[3, 4] = new King(new Position(3, 4), game.CurrentPlayer);
            map[6, 7] = new Queen(new Position(6, 7), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate());
        }

        [Test]
        public void EpoletalMate() //эполетный мат
        {
            GameModel game = new GameModel();
            var map = game.Map;
            game.Start();
            ClearMap(map);
            map[2, 4] = new Queen(new Position(2, 4), game.CurrentPlayer);
            game.SwapPlayer();
            map[0, 3] = new Rook(new Position(0, 3), game.CurrentPlayer);
            map[0, 4] = new King(new Position(0, 4), game.CurrentPlayer);
            map[0, 5] = new Rook(new Position(0, 5), game.CurrentPlayer);
            Assert.AreEqual(true, game.IsMate());
        }

        public static void ClearMap(IFigure[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = null;
        }

    }
}
