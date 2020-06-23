using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessProject
{
    class GameForm : Form
    {
        private readonly GameModel game = new GameModel();
        CellButton[,] buttons = new CellButton[8, 8];
        CellButton prevButton;
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
                    if (game.Map[i, j] is IFigure)
                        buttons[i, j].Text = game.Map[i, j].Picture;

            game.Start();
            MakeButtonsEnabled();
        }


        void SwapBlocks(object sender, EventArgs e)
        {
            var pressedButton = sender as CellButton;
            IFigure currentFigure = game.Map[pressedButton.Location.Y / 80, pressedButton.Location.X / 80];
            if (currentFigure != null && (prevFigure == null || prevFigure.Player == currentFigure.Player))
            {
                if (prevFigure != null && prevFigure.Player == currentFigure.Player) 
                    UpdateMap();
                if (!IsClicked || prevFigure.Player == currentFigure.Player)
                {
                    game.FindPosibleWays(currentFigure);
                    foreach (var pos in game.PosiblePositions)
                    {
                        buttons[pos.X, pos.Y].BackColor = Color.Green;
                        buttons[pos.X, pos.Y].Enabled = true;
                    }
                    prevColor = pressedButton.BackColor;
                    prevFigure = currentFigure;
                    pressedButton.BackColor = Color.YellowGreen;
                    IsClicked = true;
                }
            }
            else if (currentFigure == null || currentFigure.Player != prevFigure.Player)
            {
                if (IsClicked)
                {
                    var newPos = new Position(pressedButton.Position.X, pressedButton.Position.Y);
                    game.MakeTurn(newPos, prevFigure);
                    pressedButton.Text = prevButton.Text;
                    prevButton.Text = "";
                    prevButton.BackColor = prevColor;
                    IsClicked = false;
                    UpdateMap();
                    prevFigure = null;
                    game.SwapPlayer();
                    MakeButtonsEnabled();
                }
            }
            prevButton = pressedButton;
        }

        void MakeButtonsEnabled()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = game.Map[i, j];
                    if (figure != null)
                    {
                        if (figure.Player == game.CurrentPlayer)
                            buttons[i, j].Enabled = true;
                        else buttons[i, j].Enabled = false;
                    }
                }
        }

        void UpdateMap()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var button = buttons[i, j];
                    if ((i + j) % 2 == 1) button.BackColor = Color.Brown;
                    else button.BackColor = Color.OldLace;
                    if (button.Text == "") button.Enabled = false;
                }
        }

        CellButton MakeButton(int i, int j)
        {
            CellButton button = new CellButton(new Position( i, j));
            if ((i + j) % 2 == 1) button.BackColor = Color.Brown;
            else button.BackColor = Color.OldLace;
            var size = new Size(80, 80);
            button.Size = size;
            button.Font = new Font("Times New Roman", 28F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.Location = new Point(j * 80, i * 80);
            button.Click += SwapBlocks;
            button.Enabled = false;
            Controls.Add(button);
            return button;
        }
    }
}
