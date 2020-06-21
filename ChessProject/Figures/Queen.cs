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
            throw new NotImplementedException();
        }

        public void Move(Position newPosition)
        {
            Position = newPosition;
        }
    }
}
