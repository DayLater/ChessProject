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
            return FindPosibleWaysBySelector(map, figure => figure.Player != Player);
        }

        public List<Position> UnacceptablePositionsForKing(IFigure[,] map)
        {
            return FindPosibleWaysBySelector(map, figure => true);
        }

        List<Position> FindPosibleWaysBySelector(IFigure[,] map, Func<IFigure, bool> selector)
        {
            var result = new List<Position>();
            this.FindPosiblePositionsInDirection(result, 0, 1, map, selector);
            this.FindPosiblePositionsInDirection(result, 0, -1, map, selector);
            this.FindPosiblePositionsInDirection(result, 1, 0, map, selector);
            this.FindPosiblePositionsInDirection(result, -1, 0, map, selector);
            return result;
        }
    }
}
