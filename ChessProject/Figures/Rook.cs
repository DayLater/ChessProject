using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Rook : IFigure
    {
        public Position Position { get;  set; }
        public Player Player { get; }
        public Image Picture => Player.Color == PlayerColor.Black ? Properties.Resources.RookB : Properties.Resources.RookW;
        public bool IsFirstStep { get; private set; } = true;

        public void Move(Position newPosition)
        {
            Position = new Position(newPosition.X, newPosition.Y);
            IsFirstStep = false; 
        }

        public Rook(Position position, Player player)
        {
            Position = position;
            Player = player;
        }

        public List<Position> FindPosibleWays(Map map)
        {
            return FindPosibleWaysBySelector(map, (figure) => figure.Player != Player, figure => true);
        }

        public List<Position> UnacceptablePositionsForKing(Map map)
        {
            return FindPosibleWaysBySelector(map, x => true, figure => !(figure is King && figure.Player != Player));
        }

        List<Position> FindPosibleWaysBySelector(Map map, Func<IFigure, bool> selector, Func<IFigure, bool> breakSelector)
        {
            var result = new List<Position>();
            this.FindPosiblePositionsInDirection(result, 0, 1, map, selector, breakSelector);
            this.FindPosiblePositionsInDirection(result, 0, -1, map, selector, breakSelector);
            this.FindPosiblePositionsInDirection(result, 1, 0, map, selector, breakSelector);
            this.FindPosiblePositionsInDirection(result, -1, 0, map, selector, breakSelector);
            return result;
        }
    }
}
