using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Queen : IFigure
    {
        public Position Position { get; private set; }
        public Player Player { get;  }
        public string Picture => Player.Color == PlayerColor.Black ? "♛" : "♕";
        public void Move(Position newPosition) => Position = newPosition;

        public Queen(Position position, Player player)
        {
            Position = position;
            Player = player; 
        }

        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            return FindPosibleWaysBySelector(map, (figure) => figure.Player != Player, figure => true);
        }

        public List<Position> UnacceptablePositionsForKing(IFigure[,] map)
        {
            return FindPosibleWaysBySelector(map, x => true, figure => !(figure is King && figure.Player != Player));
        }

        //вспомогательный метод для того, чтобы не писать один и тот же код два раза
        List<Position> FindPosibleWaysBySelector(IFigure[,] map, Func<IFigure, bool> selector, Func<IFigure, bool> breakSelector)
        {
            var result = new List<Position>();
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (!(i == j && i == 0))
                        this.FindPosiblePositionsInDirection(result, i, j, map, selector, breakSelector);
            return result;
        }
    }
}
