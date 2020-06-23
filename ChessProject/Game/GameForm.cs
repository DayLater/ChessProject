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
        IFigure prevFigure;

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
            SwapPlayers();
        }


        //метод меняющий местами блоки
        void SwapBlocks(object sender, EventArgs e)
        {
            var pressedButton = sender as CellButton; //получили кпопку
            IFigure currentFigure = game.Map[pressedButton.Position.X, pressedButton.Position.Y]; //получили фигуру
            //если фигура не пустая и при этом прошлой фигуры нет или прошлая фигура того же игрока, то
            if (currentFigure != null && (prevFigure == null || prevFigure.Player == currentFigure.Player))
            {
                //если прошлая фигура все же есть и она того же игрока
                if (prevFigure != null && prevFigure.Player == currentFigure.Player)
                    UpdateMap(); //обновляем карту, чтобы убрать зеленые возможные ходы прошлой фигуры
                //если это первое нажатие на фигуру или выбрали фигуру того жи игрока
                if (prevFigure == null || prevFigure.Player == currentFigure.Player)
                {
                    game.FindPosibleWays(currentFigure); //ищем возможные ходы
                    foreach (var pos in game.PosiblePositions)
                    {
                        buttons[pos.X, pos.Y].BackColor = Color.Green; //помечаем их зеленым
                        buttons[pos.X, pos.Y].Enabled = true; //даем возмонжость нажать на эти клетки
                    }
                    prevFigure = currentFigure; //запомнили фигуру
                    pressedButton.BackColor = Color.YellowGreen; //нажатую кнопку выделели 
                }
            }
            // Если нажали на пустую клетку или на фигуру противника 
            else if (currentFigure == null || currentFigure.Player != prevFigure.Player)
            {
                //нашли новоую позицию
                var newPos = new Position(pressedButton.Position.X, pressedButton.Position.Y);
                game.MakeTurn(newPos, prevFigure); //сделали туда ход
                pressedButton.Text = prevButton.Text; //отразили это на экране
                prevButton.Text = ""; //прошлую клетку очистили
                UpdateMap(); //обновили карту 
                prevFigure = null; //очистили прошую фигуру
                SwapPlayers(); //Поменяли игроков местами
            }
            prevButton = pressedButton;
        }

        //метод для активации кнопок конкретного игрока
        void SwapPlayers()
        {
            game.SwapPlayer(); //поменяли игроков местами
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

        //метод обновления карты до нормального состояния
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

        //метод, создающий кнопку
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
