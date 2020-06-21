﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject.Figures
{
    public class Horse : IFIgure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; }

        public Pawn(Position position, Player player)
        {
            Position = position;
            Player = player;
            IsAlive = true;
        }

        public void Move(Position newPosition)
        {
            Position = newPosition;
        }

        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            result.Add(new Position(1,1));
            return result;
        }
    }
}
