using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Horse : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; }
        public string Picture => Player.Color == PlayerColor.Black? "♞" : "♘";
        public void Move(Position newPosition) => Position = newPosition;
        public Horse(Position position, Player player)
        {
            Position = position;
            Player = player;
            IsAlive = true;
        }
        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            FindPosiblePositionsInDirection(result, -1, -2, map);
            FindPosiblePositionsInDirection(result, -2, -1, map);
            FindPosiblePositionsInDirection(result, 1, -2, map);
            FindPosiblePositionsInDirection(result, 2, -1, map);
            FindPosiblePositionsInDirection(result, -2, 1, map);
            FindPosiblePositionsInDirection(result, -1, 2, map);
            FindPosiblePositionsInDirection(result, 2, 1, map);
            FindPosiblePositionsInDirection(result, 1, 2, map);
            return result;
        }
        public void FindPosiblePositionsInDirection(List<Position> positions, int dx, int dy, IFigure[,] map)
        {
            int x = Position.X + dx;
            int y = Position.Y + dy;
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (map[x, y] is null)
                    positions.Add(new Position(x, y));
                else
                {
                    if (map[x, y].Player != this.Player)
                        positions.Add(new Position(x, y));
                }
            }
        }
    }
}