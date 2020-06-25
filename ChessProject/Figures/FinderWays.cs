using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public static class FinderWays
    {
        public static void FindPosiblePositionsInDirection(this IFigure figure, List<Position> positions, 
            int dx, int dy, IFigure[,] map, Func<IFigure, bool> selector, Func<IFigure, bool> breakSelector)
        {
            int x = figure.Position.X + dx;
            int y = figure.Position.Y + dy;
            while (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (map[x, y] is null)
                    positions.Add(new Position(x, y));
                else
                {
                    if (selector(map[x,y]))
                        positions.Add(new Position(x, y));
                    if (breakSelector(map[x, y])) break;
                }
                x += dx;
                y += dy;
            }
        }
    }
}
