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
        public Position Position { get;  set; }
        public Player Player { get; }
        public string Picture => Player.Color == PlayerColor.Black ? "♟" : "♙";
        public bool IsFirstStep { get; set; } = true;

        public Pawn(Position position, Player player)
        {
            Position = position;
            Player = player;
        }

        public void Move(Position newPosition)
        {
            Position = newPosition;
            IsFirstStep = false;
        }

        public List<Position> FindPosibleWays(Map map)
        {
            var result = new List<Position>();
            var dx = -1; 
            if (Player.Color == PlayerColor.Black) dx = 1;
            FindPosiblePositionsInDirection(result, dx, map);//может ходить если пусто
            return result;
        }

        void FindPosiblePositionsInDirection(List<Position> positions, int dx, Map map)
        {
            int x = Position.X + dx;
            for (int i = -1; i < 2; i++)
            {
                int y = Position.Y + i;
                if (x >= 0 && x < 8 && y >= 0 && y < 8)
                {
                    if (i != 0 && map[x, y] != null && map[x, y].Player != Player)
                        positions.Add(new Position(x, y));
                    else if (i == 0 && map[x, y] is null)
                    {
                        if (IsFirstStep && map[x + dx, y] is null)
                            positions.Add(new Position(x + dx, y));
                        positions.Add(new Position(x , y));
                    }
                }
            }
        }

        #region Методы необходимые для поиска неподходящий позиция для короля
        public List<Position> UnacceptablePositionsForKing(Map map)
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
        #endregion
    }
}
