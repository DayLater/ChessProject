using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Rook : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; }

        public Rook(Position position, Player player)
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
            
            return result;
        }
    }
}
