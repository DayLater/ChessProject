using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public static class Checker
    {
        public static King FingShahKing(Player currentPlayer, IFigure[,] map)
        {
            King king = null;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    IFigure figure = map[i, j];
                    if (figure is King && figure.Player == currentPlayer)
                    {
                        king = (King)figure;
                        break;
                    }
                }
            return king;
        }

        public static bool IsShah(IFigure[,] map, King shahKing)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = map[i, j];
                    if (figure != null && figure.Player != shahKing.Player)
                    {
                        var figuresPositions = figure.FindPosibleWays(map);
                        if (figuresPositions.Contains(shahKing.Position))
                            return true;
                    }
                }
            return false;
        }
    }
}
