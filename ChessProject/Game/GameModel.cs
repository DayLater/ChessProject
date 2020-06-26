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
        public List<Position> PosiblePositions;
        public Player CurrentPlayer { get; private set; }
        //Карта
        public readonly IFigure[,] Map = new IFigure[8, 8];
        Stack<IFigure[,]> stackOfMaps = new Stack<IFigure[,]>();

        readonly King blackKing;
        readonly King whiteKing;

        public void Start()
        {
            CurrentPlayer = white;
        }

        public void SwapPlayer()
        {
            if (CurrentPlayer == white) CurrentPlayer = black;
            else CurrentPlayer = white;
        }

        public GameModel()
        {
            CreateMap();
            blackKing = (King)Map[0, 4];
            whiteKing = (King)Map[7, 4];
            stackOfMaps.Push((IFigure[,])Map.Clone());
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
                Map[side + dx, i] = new Pawn(new Position(side + dx, i), player);
            Map[side, 0] = new Rook(new Position(side, 0), player);
            Map[side, 7] = new Rook(new Position(side, 7), player);
            Map[side, 1] = new Horse(new Position(side, 1), player);
            Map[side, 6] = new Horse(new Position(side, 6), player);
            Map[side, 2] = new Elephant(new Position(side, 2), player);
            Map[side, 5] = new Elephant(new Position(side, 5), player);
            Map[side, 3] = new Queen(new Position(side, 3), player);
            Map[side, 4] = new King(new Position(side, 4), player);
        }

        //Метод для поиска пути конкретной фигуры
        public void FindPosibleWays(IFigure figure)
        {
            PosiblePositions = figure.FindPosibleWays(Map);
        }

        public void MakeTurn(Position newPos, IFigure figure)
        {
            
            Map[figure.Position.X, figure.Position.Y] = null;
            Map[newPos.X, newPos.Y] = figure;
            figure.Move(newPos);
            stackOfMaps.Push((IFigure[,])Map.Clone());
        }

        //Хранит позицию короля, которому сделали шах
        public Position KingPositionAtStake { get;private set; }

        //Есть ли шах
        public bool IsShah(Position figurePosition) 
        {
            var figure = Map[figurePosition.X, figurePosition.Y];
            var positions = figure.FindPosibleWays(Map);
            if (figure is Pawn)
                positions = positions.Where(p => p.Y != figure.Position.Y).ToList();
            foreach (var pos in positions)
                if (Map[pos.X, pos.Y] is King && figure.Player != Map[pos.X, pos.Y].Player) //если  фигура чужой король
                {
                    KingPositionAtStake = pos;
                    return true;
                }
            return false;
        }

        bool IsShah(IFigure[,] map, King shahKing)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = map[i, j];
                    if (figure != null && figure.Player != shahKing.Player)
                    {
                        var figuresPositions = figure.FindPosibleWays(map);
                        if (figuresPositions.Contains(shahKing.Position))
                            return true;
                    }
                }
            return false;
        }

        //Есть ли мат
        public bool IsMate(Position kingPosition, Position figurePosition)
        {
            if (!IsShah(figurePosition))
                return false;
            var posiblePositions = Map[kingPosition.X, kingPosition.Y].FindPosibleWays(Map);
            var player = Map[kingPosition.X, kingPosition.Y].Player;
            var listPositionsPlayer = new List<Position>();
            var path = GetPositionsThreateningTheKing(kingPosition, figurePosition);
            
            if (posiblePositions.Count == 0)
            {
                foreach (var figure in Map)
                {
                    if (figure != null && figure.Player.Equals(player))
                    {
                        var figurePosiblePositions = figure.FindPosibleWays(Map);
                        var figurePos = new Position(figure.Position.X, figure.Position.Y);
                        foreach (var pos in figurePosiblePositions)
                        {
                            var tempMap = (IFigure[,])Map.Clone();
                            tempMap[figurePos.X, figurePos.Y].Move(new Position(pos.X, pos.Y));
                            tempMap[pos.X, pos.Y] = tempMap[figurePos.X, figurePos.Y];
                            tempMap[figurePos.X, figurePos.Y] = null;
                            if (!IsShah(tempMap, (King)Map[kingPosition.X, kingPosition.Y]))
                                listPositionsPlayer.Add(pos);
                            figure.Move(figurePos);
                        }
                    }
                }
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
    }
}
