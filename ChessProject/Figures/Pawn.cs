using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Pawn : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; }
        public string Picture => Player.Color == PlayerColor.Black ? "♟" : "♙";
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
            var dx = -1; 
            if (Player.Color.Equals(PlayerColor.Black)) dx = 1;
            FindPosiblePositionsInDirection(result, dx, 0, map, false);//может ходить если пусто
            FindPosiblePositionsInDirection(result, dx, -1, map, true);//может есть, но не ходить
            FindPosiblePositionsInDirection(result, dx, 1, map, true);//может есть, но не ходить
            if (IsFirstStep)
                FindPosiblePositionsInDirection(result, dx * 2, 0, map, false);//может ходить если пусто
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
                    if (dx == -2)
                    {
                        if (map[x, y] is null && map[x + 1, y] is null)
                            positions.Add(new Position(x, y));

                    }
                    else if (dx == 2)
                    {
                        if (map[x, y] is null && map[x - 1, y] is null)
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

        public List<Position> UnacceptablePositionsForKing(IFigure[,] map)
        {
            var result = new List<Position>();
            var dx = -1; 
            if (Player.Color.Equals(PlayerColor.Black)) dx = 1;
            FindUnacceptablePositionsInDirection(result, dx, -1);//может есть, но не ходить
            FindUnacceptablePositionsInDirection(result, dx, 1);//может есть, но не ходить
            return result;
        }

        void FindUnacceptablePositionsInDirection(List<Position> positions, int dx, int dy)
        {
            int x = Position.X + dx;
            int y = Position.Y + dy;
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
                positions.Add(new Position(x, y));
        }
    }
}
