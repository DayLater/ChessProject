using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Elephant : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; }

        public Elephant(Position position, Player player)
        {
            Position = position;
            Player = player;
            IsAlive = true;
        }

        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            FindPosiblePositionsInDirection(result, 1, 1, map);
            FindPosiblePositionsInDirection(result, 1, -1, map);
            FindPosiblePositionsInDirection(result, -1, 1, map);
            FindPosiblePositionsInDirection(result, -1, -1, map);
            return result;
        }

        void FindPosiblePositionsInDirection(List<Position> positions, int dx, int dy, IFigure[,] map)
        {
            int x = Position.X + dx;
            int y = Position.Y + dy;
            while (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (map[x, y] is null)
                    positions.Add(new Position(x, y));
                else
                {
                    if (map[x, y].Player != Player)
                        positions.Add(new Position(x, y));
                    break;
                }
                x += dx;
                y += dy;
            }
        }

        public void Move(Point newPosition)
        {
            throw new NotImplementedException();
        }
    }
}
