﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject.Figures
{
    public class Pawn : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; }
        private bool IsFirstStep { get; set; } = true;
        public Pawn(Position position, Player player)
        {
            Position = position;
            Player = player;
            IsAlive = true;
        }
        public void Move(Position newPosition)
        {
            Position = newPosition;
            IsFirstStep = false;
        }
        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            if (Player.Color.Equals(Color.Black))
            {
                FindPosiblePositionsInDirection(result, 0, 1, map, false);//может ходить если пусто
                FindPosiblePositionsInDirection(result, -1, 1, map, true);//может есть, но не ходить
                FindPosiblePositionsInDirection(result, 1, 1, map, true);//может есть, но не ходить
                if (IsFirstStep)
                    FindPosiblePositionsInDirection(result, 0, 2, map, false);//может ходить если пусто
            }
            else
            {
                FindPosiblePositionsInDirection(result, 0, -1, map, false);//может ходить если пусто
                FindPosiblePositionsInDirection(result, -1, -1, map, true);//может есть, но не ходить
                FindPosiblePositionsInDirection(result, 1, -1, map, true);//может есть, но не ходить
                if (IsFirstStep)
                    FindPosiblePositionsInDirection(result, 0, -2, map, false);//может ходить если пусто
            }
            return result;
        }
        void FindPosiblePositionsInDirection(List<Position> positions, int dx, int dy, IFigure[,] map, bool eat)
        {
            int x = Position.X + dx;
            int y = Position.Y + dy;

            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (eat)
                {
                    if (map[x, y] != null && map[x, y].Player != Player)
                        positions.Add(new Position(x, y));
                }
                else
                {
                    if (map[x, y] is null)
                        positions.Add(new Position(x, y));
                }
            }
        }

    }
}
