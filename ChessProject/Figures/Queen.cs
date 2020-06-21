using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject.Figures
{
    public class Queen : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get;  }

        public bool IsAlive { get; set; }

        public Queen(Position position, Player player)
        {
            Position = position;
            Player = player; 
        }

        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (!(i == j && i == 0))
                        this.FindPosiblePositionsInDirection(result, i, j, map);
            return result;
        }

        public void Move(Position newPosition)
        {
            Position = newPosition;
        }
    }
}
