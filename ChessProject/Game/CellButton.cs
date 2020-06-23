using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    public class CellButton : Button
    {
        public Position Position { get;  }
        public CellButton(Position position)
        {
            Position = position;
        }
    }
}
