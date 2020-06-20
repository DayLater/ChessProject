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
        Point Position { get; set; }
        Player Player { get; set; }
        bool IsAlive { get; set; }

        void Move(Point newPosition);

        List<Point> FindPosibleWays();
    }
}
