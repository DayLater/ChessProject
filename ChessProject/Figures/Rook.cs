using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject.Figures
{
    public class Rook : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; }
        public string Picture => Player.Color == PlayerColor.Black ? "♜" : "♖";
        public void Move(Position newPosition) => Position = newPosition;
        public Rook(Position position, Player player)
        {
            Position = position;
            Player = player;
            IsAlive = true;
        }
        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            this.FindPosiblePositionsInDirection(result, 0, 1, map);
            this.FindPosiblePositionsInDirection(result, 0, -1, map);
            this.FindPosiblePositionsInDirection(result, 1, 0, map);
            this.FindPosiblePositionsInDirection(result, -1, 0, map);
            return result;
        }
    }
}
