using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    class GameForm : Form
    {
        private readonly GameModel game = new GameModel();
        Button[,] buttons = new Button[8, 8];

        public GameForm()
        {
            Size = new Size(700, 700);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    buttons[i, j] = MakeButton(i, j);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (game.Map[i, j] is IFigure)
                    {
                        buttons[i, j].Text = game.Map[i,j].Picture;
                        buttons[i, j].Enabled = true;
                    }
                }
        }

        Button MakeButton(int i, int j)
        {
            Button button = new Button();
            if ((i + j) % 2 == 1) button.BackColor = System.Drawing.Color.Brown;
            else button.BackColor = System.Drawing.Color.OldLace;
            var size = new Size(80, 80);
            button.Size = size;
            button.Font = new Font("Times New Roman", 28F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.Location = new Point(j * 80, i * 80);
            button.Enabled = false;
            Controls.Add(button);
            return button;
        }
    }
}
