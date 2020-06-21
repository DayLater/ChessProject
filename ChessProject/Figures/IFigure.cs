using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public interface IFigure
    {
        Position Position { get; set; }
        Player Player { get;}
        bool IsAlive { get; }
        string Picture { get; }
        void Move(Position newPosition);
        List<Position> FindPosibleWays(IFigure[,] map);
    }
}
