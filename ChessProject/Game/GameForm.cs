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
        Button prevButton;
        Color prevColor;
        IFigure prevFigure;
        bool IsClicked; 

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
                        buttons[i, j].Text = game.Map[i,j].Picture;
                }
        }

        void SwapBlocks(object sender, EventArgs e)
        {
            var pressedButton = sender as Button;
            IFigure currentFigure = game.Map[pressedButton.Location.Y / 80, pressedButton.Location.X / 80];
            if (currentFigure != null)
            {
                var list = currentFigure.FindPosibleWays(game.Map);
                foreach (var pos in list)
                {
                    buttons[pos.X, pos.Y].BackColor = Color.Yellow;
                }
                prevColor = pressedButton.BackColor;
                pressedButton.BackColor = Color.Green;
                IsClicked = true;
            }
            else
            {
                if (IsClicked)
                {
                    game.Map[pressedButton.Location.Y / 80, pressedButton.Location.X / 80] =
                        game.Map[prevButton.Location.Y / 80, prevButton.Location.X / 80];
                    game.Map[prevButton.Location.Y / 80, prevButton.Location.X / 80] = currentFigure;
                    pressedButton.Text = prevButton.Text;
                    prevButton.Text = "";
                    prevButton.BackColor = prevColor;
                    IsClicked = false;
                }
            }
            prevButton = pressedButton;
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
            button.Click += SwapBlocks;
            Controls.Add(button);
            return button;
        }
    }
}
