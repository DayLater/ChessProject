using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ChessProject
{
    class GameForm : Form
    {
        private readonly GameModel game = new GameModel();
        readonly CellButton[,] buttons = new CellButton[8, 8];
        Label currentPositionLabel;
        Label currentPlayerLabel;
        IFigure prevFigure;

        public GameForm()
        {
            CreateFormAndButtons();
            CreateLabels(new string[] { "A", "B", "C", "D", "E", "F", "G", "H" }, true);
            CreateLabels(new string[] { "8","7", "6", "5", "4", "3", "2",  "1" }, false);
            CreateLabelCurrentPosition();
            CreateCurrentPlayerLabel();
            game.Start();
            SwapPlayers();
        }

        /// <summary>
        /// Основной метод игры. Используется, когда игрок делает ход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SwapBlocks(object sender, EventArgs e)
        {
            var pressedCell = sender as CellButton; //получили кпопку
            IFigure currentFigure = game.Map[pressedCell.Position.X, pressedCell.Position.Y]; //получили фигуру
            //если фигура не пустая и при этом прошлой фигуры нет или прошлая фигура того же игрока, то
            if (currentFigure != null && (prevFigure == null || prevFigure.Player == currentFigure.Player))
            {
                //если прошлая фигура все же есть и она того же игрока. При этом game.PosiblePositions не пуст
                if (prevFigure != null && prevFigure.Player == currentFigure.Player && game.PosiblePositions != null)
                    UpdateColorPosition(prevFigure.Position);
                //если это первое нажатие на фигуру или выбрали фигуру того же игрока
                if (prevFigure == null || prevFigure.Player == currentFigure.Player)
                {
                    game.FindPosibleWays(currentFigure); //ищем возможные ходы
                    foreach (var pos in game.PosiblePositions)
                    {
                        buttons[pos.X, pos.Y].BackColor = Color.Green; //помечаем их зеленым
                        buttons[pos.X, pos.Y].Enabled = true; //даем возмонжость нажать на эти клетки
                    }
                    prevFigure = currentFigure; //запомнили фигуру
                    pressedCell.BackColor = Color.YellowGreen; //нажатую кнопку выделели 
                }
            }
            // Если нажали на пустую клетку или на фигуру противника 
            else if (currentFigure == null || currentFigure.Player != prevFigure.Player)
            {
                var newPos = new Position(pressedCell.Position.X, pressedCell.Position.Y);//нашли новоую позицию
                game.MakeTurn(newPos, prevFigure); //сделали туда ход
                UpdateMap(); //обновили карту 
                game.FindPosibleWays(prevFigure); //ищем возможные ходы
                var previosFigure = prevFigure;
                foreach (var pos in game.PosiblePositions)
                {
                    if (game.Map[pos.X, pos.Y] is King) //если фигура король
                    {
                        buttons[pos.X, pos.Y].BackColor = Color.Red; //помечаем красным
                        buttons[pos.X, pos.Y].Enabled = false; //нельзя убитьМОже
                        if (game.IsMate((King)game.Map[pos.X, pos.Y], previosFigure)) 
                        {
                            MessageBox.Show(
                            "Шах и мат, аметисты!",
                            "Игра окончена",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                        }
                    }
                }
                SwapPlayers(); //Поменяли игроков местами
            }
        }

        #region Создание кнопок, label-текста и прочее, не влияющее на игру
        /// <summary>
        /// Метод создания Label для выведения информации о текущей позиции мыши
        /// </summary>
        void CreateLabelCurrentPosition()
        {
            currentPositionLabel = new Label();
            currentPositionLabel.Location = new Point(680, 0);
            currentPositionLabel.Size = new Size(150, 50);
            currentPositionLabel.Text = "Позиция: "; 
            currentPositionLabel.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Controls.Add(currentPositionLabel);
        }

        /// <summary>
        /// Метод показывающий текущую позицию на экране. Используется в событии наведения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowCurrentPosition(object sender, EventArgs e)
        {
            var btn = sender as CellButton;
            var pos = "";
            switch (btn.Position.Y)
            {
                case 0:
                    pos += "A";
                    break;
                case 1:
                    pos += "B";
                    break;
                case 2:
                    pos += "C";
                    break;
                case 3:
                    pos += "D";
                    break;
                case 4:
                    pos += "E";
                    break;
                case 5:
                    pos += "F";
                    break;
                case 6:
                    pos += "G";
                    break;
                case 7:
                    pos += "H";
                    break;
            }
            currentPositionLabel.Text = "Позиция: ";
            currentPositionLabel.Text += pos + (7 - btn.Position.X + 1);

        }

        /// <summary>
        /// Метод создающий кнопку на форме
        /// </summary>
        /// <param name="i">кордината</param>
        /// <param name="j">координата</param>
        /// <returns></returns>
        CellButton MakeButton(int i, int j)
        {
            CellButton button = new CellButton(new Position(i, j));
            if ((i + j) % 2 == 1) button.BackColor = Color.Brown;
            else button.BackColor = Color.OldLace;
            var size = new Size(80, 80);
            button.Size = size;
            button.Font = new Font("Times New Roman", 28F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.Location = new Point(j * 80 + 40, i * 80);
            button.Click += SwapBlocks;
            button.MouseEnter += ShowCurrentPosition;
            button.Enabled = false;
            Controls.Add(button);
            return button;
        }

        /// <summary>
        /// Создание лейбла для вывода информации о текущем игроке
        /// </summary>
        void CreateCurrentPlayerLabel()
        {
            currentPlayerLabel = new Label();
            currentPlayerLabel.Location = new Point(680, 50);
            currentPlayerLabel.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            currentPlayerLabel.Size = new Size(150, 50);
            Controls.Add(currentPlayerLabel);
        }

        /// <summary>
        /// Метод создания формы и карты
        /// </summary>
        void CreateFormAndButtons()
        {
            Size = new Size(820, 720);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    buttons[i, j] = MakeButton(i, j);
            UpdateMap();
        }

        /// <summary>
        /// Метод для создания боковых обозначений позиции
        /// </summary>
        /// <param name="words"></param>
        /// <param name="isWords"></param>
        void CreateLabels(string[] words, bool isWords)
        {
            if (words.Length != 8) throw new Exception();
            for (int i = 0; i < 8; i++)
            {
                var label = new Label();
                if (isWords)
                    label.Location = new Point(60 + i * 80, 620);
                else label.Location = new Point(0, i * 80);
                label.Size = new Size(40, 80);
                label.Text = words[i];
                label.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
                label.TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(label);
            }
        }
        #endregion

        /// <summary>
        /// Метод, менящий действующего игрока
        /// </summary>
        void SwapPlayers()
        {
            prevFigure = null; //очистили прошую фигуру
            game.SwapPlayer(); //поменяли игроков местами
            if (game.CurrentPlayer.Color == PlayerColor.White)
                currentPlayerLabel.Text = "Ход белого игрока";
            else currentPlayerLabel.Text = "Ход черного игрока";
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

        /// <summary>
        /// Метод для обновления карты
        /// Используется только после хода игрока
        /// </summary>
        void UpdateMap()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (game.Map[i, j] is IFigure)
                        buttons[i, j].Text = game.Map[i, j].Picture;
                    else buttons[i, j].Text = "";

                    var button = buttons[i, j];
                    if ((i + j) % 2 == 1) button.BackColor = Color.Brown;
                    else button.BackColor = Color.OldLace;
                    if (button.Text == "") button.Enabled = false;
                }
        }

        /// <summary>
        /// Метод для возврата цвета клеток к нормальному от зеленого
        /// </summary>
        /// <param name="p"></param>
        void UpdateColorPosition(Position p) 
        {
            var list = game.PosiblePositions;
            list.Add(p);
            foreach (var cell in list)
            {
                if ((cell.X + cell.Y) % 2 == 1) buttons[cell.X, cell.Y].BackColor = Color.Brown;
                else buttons[cell.X, cell.Y].BackColor = Color.OldLace;
                if (buttons[cell.X, cell.Y].Text == "") buttons[cell.X, cell.Y].Enabled = false;
            }
        }

    }
}

