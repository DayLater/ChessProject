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

        public void Move(Position newPosition)
        {
            Position = newPosition;
        }

        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            this.FindPosiblePositionsInDirection(result, 1, 1, map);
            this.FindPosiblePositionsInDirection(result, 1, -1, map);
            this.FindPosiblePositionsInDirection(result, -1, 1, map);
            this.FindPosiblePositionsInDirection(result, -1, -1, map);
            return result;
        }
    }
}
