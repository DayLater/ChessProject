using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public static class FinderWays
    {
        public static void FindPosiblePositionsInDirection(this IFigure figure, List<Position> positions, int dx, int dy, IFigure[,] map)
        {
            int x = figure.Position.X + dx;
            int y = figure.Position.Y + dy;
            while (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (map[x, y] is null)
                    positions.Add(new Position(x, y));
                else
                {
                    if (map[x, y].Player != figure.Player)
                        positions.Add(new Position(x, y));
                    break;
                }
                x += dx;
                y += dy;
            }
        }
    }
}
