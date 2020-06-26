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
        public string Picture => Player.Color == PlayerColor.Black? "♞" : "♘";
        public void Move(Position newPosition) => Position = newPosition;
        public Horse(Position position, Player player)
        {
            Position = position;
            Player = player;
        }

        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            return FindPosibleWaysBySelector(map, figure => figure.Player != Player);
        }

        public List<Position> UnacceptablePositionsForKing(IFigure[,] map)
        {
            return FindPosibleWaysBySelector(map, figure => true);
        }

        public void FindPosiblePositionsInDirection(List<Position> positions, int dx, int dy, IFigure[,] map, Func<IFigure, bool> selector)
        {
            int x = Position.X + dx;
            int y = Position.Y + dy;
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (map[x, y] is null)
                    positions.Add(new Position(x, y));
                else if (selector(map[x, y]))
                    positions.Add(new Position(x, y));
            }
        }

        public List<Position> FindPosibleWaysBySelector(IFigure[,] map, Func<IFigure, bool> selector)
        {
            var result = new List<Position>();
            (int, int)[] positions = { (-1, -2), (-2, -1), (1, -2), (2, -1), (-2, 1), (-1, 2), (2, 1), (1, 2) };
            for (int i = 0; i < positions.Length; i++)
                FindPosiblePositionsInDirection(result, positions[i].Item1, positions[i].Item2, map, selector);
            return result;
        }
    }
}