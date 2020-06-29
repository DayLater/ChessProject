﻿using ChessProject.Figures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    //Класс игровой модели
    public class GameModel
    {
        //Игроки
        Player white = new Player(PlayerColor.White);
        Player black = new Player(PlayerColor.Black);
        public List<Position> PosiblePositions { get; set; }
        public Player CurrentPlayer { get; private set; }
        //Карта
        public readonly Map Map = new Map();

        public King CurrentKing { get { return FindCurrentKing(); } }
        public IFigure PreviousFigure { get; set; }
        public IFigure CurrentFigure { get; set; }

        public bool IsSamePlayer { get { return PreviousFigure.Player == CurrentFigure.Player; } }

        public void GetCurrentFigure(Position figurePosition)
        {
            PreviousFigure = CurrentFigure;
            CurrentFigure = Map[figurePosition.X, figurePosition.Y];
        }

        public void Start()
        {
            CurrentPlayer = white;
        }

        public void SwapPlayer()
        {
            if (CurrentPlayer == white) CurrentPlayer = black;
            else CurrentPlayer = white;
            PreviousFigure = null;
        }

        public GameModel()
        {
            CreateMap();
        }

        //Метод для заполнения карты фигурами
        void CreateMap()
        {
            AddOneSide(white);
            AddOneSide(black);
        }

        //Добавляет играющую сторону
        void AddOneSide(Player player)
        {
            var side = 0;
            var dx = 1;
            if (player == white)
            {
                side = 7;
                dx = -1;
            }
            for (int i = 0; i < 8; i++)
                Map.Add(new Pawn(new Position(side + dx, i), player));
            Map.Add(new Rook(new Position(side, 0), player));
            Map.Add(new Rook(new Position(side, 7), player));
            Map.Add(new Horse(new Position(side, 1), player));
            Map.Add(new Horse(new Position(side, 6), player));
            Map.Add(new Elephant(new Position(side, 2), player));
            Map.Add(new Elephant(new Position(side, 5), player));
            Map.Add(new Queen(new Position(side, 3), player));
            Map.Add(new King(new Position(side, 4), player));
        }

        //Метод для поиска пути конкретной фигуры
        public void FindPosibleWays()
        {
            PosiblePositions = new List<Position>();
            var king = FindCurrentKing();
            AddCorrectMoves(CurrentFigure, king.Position, PosiblePositions);
        }

        void AddCorrectMoves(IFigure figure, Position kingPosition, List<Position> positions)
        {
            var figurePosiblePositions = figure.FindPosibleWays(Map);
            var figurePos = new Position(figure.Position.X, figure.Position.Y);
            foreach (var pos in figurePosiblePositions)
            {
                var tempMap = Map.Clone();
                tempMap[figurePos.X, figurePos.Y].Position = new Position(pos.X, pos.Y);
                tempMap[pos.X, pos.Y] = tempMap[figurePos.X, figurePos.Y];
                tempMap[figurePos.X, figurePos.Y] = null;
                if (!IsShah(tempMap, (King)Map[kingPosition.X, kingPosition.Y], out IFigure shahFigure))
                    positions.Add(pos);
                figure.Position = figurePos;
            }
        }

        public void MakeTurn(Position newPos, IFigure figure)
        {
            Map[figure.Position.X, figure.Position.Y] = null;
            Map[newPos.X, newPos.Y] = figure;
            figure.Move(newPos);
        }


        public void MakeTurn(Position newPos)
        {
            MakeTurn(newPos, PreviousFigure);
        }

        public bool IsShah(out IFigure shahFigure, out King shahKing)
        {
            shahKing = FindCurrentKing();
            return IsShah(Map, shahKing, out shahFigure);
        }

        bool IsShah(Map map, King shahKing, out IFigure shahFigure)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = map[i, j];
                    if (figure != null && figure.Player != shahKing.Player)
                    {
                        var figuresPositions = figure.FindPosibleWays(map);
                        if (figuresPositions.Contains(shahKing.Position))
                        {
                            shahFigure = figure;
                            return true;
                        }
                    }
                }
            shahFigure = null;
            return false;
        }

        King FindCurrentKing()
        {
            King king = null;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    IFigure figure = Map[i, j];
                    if (figure != null && figure is King && figure.Player == CurrentPlayer)
                        king = (King)figure;
                }
            return king;
        }

        public bool IsStalemate()
        {
            var king = FindCurrentKing();
            if (IsShah(Map, king, out IFigure shahFigure))
                return false;
            var listPositionsPlayer = new List<Position>();
            foreach (IFigure figure in Map)
            {
                if (figure != null && figure.Player.Equals(king.Player))
                    AddCorrectMoves(figure, king.Position, listPositionsPlayer);
                if (listPositionsPlayer.Count > 0)
                    return false;
            }
            return true;
        }

        //Есть ли мат
        public bool IsMate()
        {
            var king = FindCurrentKing();
            if (!IsShah(Map, king, out IFigure shahFigure))
                return false;
            var listPositionsPlayer = new List<Position>();
            var path = GetPositionsThreateningTheKing(king.Position, shahFigure.Position);
            if (king.FindPosibleWays(Map).Count == 0)
            {
                foreach (IFigure figure in Map)
                    if (figure != null && figure.Player.Equals(king.Player))
                        AddCorrectMoves(figure, king.Position, listPositionsPlayer);
                listPositionsPlayer = listPositionsPlayer.Distinct().ToList();
                foreach (var p in listPositionsPlayer)
                    if (path.Contains(p)) return false;
                return true;
            }
            return false;
        }

        List<Position> GetPositionsThreateningTheKing(Position kingPosition, Position figurePosition)
        {
            var result = new List<Position>();
            var figure = Map[figurePosition.X, figurePosition.Y];
            var path = figure.FindPosibleWays(Map);
            if (!(figure is Horse) && !(figure is Pawn))
            {
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X > f.X && k.Y > f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X > f.X && k.Y < f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X > f.X && k.Y == f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                   (k, f) => k.X < f.X && k.Y > f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                   (k, f) => k.X < f.X && k.Y < f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X < f.X && k.Y == f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                (k, f) => k.X == f.X && k.Y > f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                   (k, f) => k.X == f.X && k.Y < f.Y);
            }
            result.Add(figure.Position);
            return result;
        }

        void SelectAListOfPositionsThreateningTheKing(Position kingPosition, Position figurePosition,
                               List<Position> path, List<Position> result,
                               Func<Position, Position, bool> predicate)
        {
            if (predicate(kingPosition, figurePosition))
                foreach (var p in path.Where(p => predicate(p, figurePosition)))
                    result.Add(p);
        }

        public bool IsPawnTransformation()
        {
            if (PreviousFigure != null && PreviousFigure is Pawn && (PreviousFigure.Position.X == 0 || PreviousFigure.Position.X == 7))
                return true;
            return false;
        }

        public void PawnTransformation(IFigure figure)
        {
            Map[PreviousFigure.Position.X, PreviousFigure.Position.Y] = figure;
        }
    }
}
