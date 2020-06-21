using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    class GameForm: Form
    {
        private readonly GameModel game = new GameModel();
        Button[,] buttons = new Button[8, 8]; 

        public GameForm()
        {
            Size = new Size(400, 400);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    buttons[i,j] = MakeButton(i, j);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (game.Map[i, j] is IFigure)
                    {
                        buttons[i, j].Text = "Здесь фигурка";
                        buttons[i, j].Enabled = true;
                    }
        }

        Button MakeButton(int i, int j)
        {
            Button button = new Button();
            if ((i + j) % 2 == 0) button.BackColor = System.Drawing.Color.Brown;
            else button.BackColor = System.Drawing.Color.OldLace;
            var size = new Size(40, 40);
            button.Size = size;
            button.Location = new Point(j * 40, i * 40);
            button.Enabled = false;
            Controls.Add(button);
            return button;
        }
    }
}
