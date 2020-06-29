using ChessProject.Game;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessProject
{
    class GameForm : Form
    {
        GameModel game = new GameModel();
        readonly CellButton[,] buttons = new CellButton[8, 8];
        Label currentPositionLabel;
        Label currentPlayerLabel;
        Button restart;
        Timer timer = new Timer(); 
        TextBox passedTime;
        DateTime time;

        public GameForm()
        {
            Icon = Properties.Resources.Icon;
            Text = "Chess";
            BackColor = Color.GhostWhite;
            CreateFormAndButtons();
            CreateLabels(new string[] { "A", "B", "C", "D", "E", "F", "G", "H" }, true);
            CreateLabels(new string[] { "8", "7", "6", "5", "4", "3", "2", "1" }, false);
            CreateLabelCurrentPosition();
            CreateCurrentPlayerLabel();
            CreateRestart();
            CreateTimer();
            game.Start();
            game.SwapPlayer();
            SwapPlayers();
            UpdateMap();
        }

        /// <summary>
        /// Основной метод игры. Используется, когда игрок делает ход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SwapBlocks(object sender, EventArgs e)
        {
            var pressedCell = sender as CellButton; //получили кпопку
            game.GetCurrentFigure(pressedCell.Position); //получили фигуру
            //если фигура не пустая и при этом прошлой фигуры нет или прошлая фигура того же игрока, то
            if (game.CurrentFigure != null && (game.PreviousFigure == null || game.IsSamePlayer))
            {
                //если прошлая фигура все же есть и она того же игрока. При этом game.PosiblePositions не пуст
                if (game.PreviousFigure != null && game.IsSamePlayer && game.PosiblePositions != null)
                    UpdateMap();
                //если это первое нажатие на фигуру или выбрали фигуру того же игрока
                if (game.PreviousFigure == null || game.IsSamePlayer)
                    ShowPosiblePositions(pressedCell);
            }
            // Если нажали на пустую клетку или на фигуру противника 
            else if (game.CurrentFigure == null || !game.IsSamePlayer)
            {
                game.MakeTurn(pressedCell.Position); //сделали туда ход
                SwapPlayers(); //Поменяли игроков местами
            }
        }

        void ShowPosiblePositions(CellButton pressedCell)
        {
            game.FindPosibleWays(); //ищем возможные ходы
            foreach (var pos in game.PosiblePositions)
            {
                buttons[pos.X, pos.Y].BackColor = Color.YellowGreen; //помечаем их зеленым
                buttons[pos.X, pos.Y].Enabled = true; //даем возмонжость нажать на эти клетки
            }
            pressedCell.BackColor = Color.Green; //нажатую кнопку выделели 
        }

        void RefreshShahColor()
        {
            if (game.IsShah(out IFigure shahFigure, out King shahKing))
            {
                buttons[shahFigure.Position.X, shahFigure.Position.Y].BackColor = Color.DeepSkyBlue; //помечаем фигуру, сделавшую шах
                buttons[shahKing.Position.X, shahKing.Position.Y].BackColor = Color.Red; //помечаем красным
            }
        }

        #region Создание кнопок, label-текста и прочее, не влияющее на игру

        void CreateTimer()
        {
            passedTime = new TextBox()
            {
                Location = new Point(680, 50),
                Size = new Size(120, 50),
                Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 204),
                TextAlign = HorizontalAlignment.Center,
                ReadOnly = true
            };
            Controls.Add(passedTime);
            timer.Start();
            timer.Interval = 1000;
            time = new DateTime();
            timer.Tick += (s, e) =>
            {
                time = time.AddSeconds(1);
                passedTime.Text = time.ToString("HH:mm:ss");
            };
        }

        void CreateRestart()
        {
            restart = new Button()
            {
                Location = new Point(680, 0),
                Text = "Начать заново",
                Size = new Size(120, 50),
                Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 204),
                BackColor = Color.LightSkyBlue
            };
            restart.Click += Restart;
            Controls.Add(restart);
        }
        void Restart(object sender, EventArgs e)
        {
            passedTime.Text = "";
            game = new GameModel();
            game.Start();
            game.SwapPlayer();
            UpdateMap();
            SwapPlayers();
            time = new DateTime();
        }

        /// <summary>
        /// Метод создания Label для выведения информации о текущей позиции мыши
        /// </summary>
        void CreateLabelCurrentPosition()
        {
            currentPositionLabel = new Label()
            {
                Location = new Point(685, 645),
                Size = new Size(120, 25),
                Text = "Позиция: A1",
                Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 204),
                BackColor = Color.GhostWhite
                
            };
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
            currentPlayerLabel = new Label()
            {
                Location = new Point(680, 80),
                Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Size = new Size(140, 50),
            };
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
            if (game.IsPawnTransformation())
            {
                var form = new PawnTransformationForm(game.PreviousFigure);
                form.ShowDialog();
                game.PawnTransformation(form.Figure);
            }
            game.SwapPlayer(); //поменяли игроков местами                
            UpdateMap(); //обновили карту 
            if (game.IsMate())
            {
                MessageBox.Show(
                  "Шах и мат!",
                  "Игра окончена",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information,
                  MessageBoxDefaultButton.Button1,
                  MessageBoxOptions.DefaultDesktopOnly);
                timer.Stop();
            }
            else if (game.IsStalemate())
            {
                MessageBox.Show(
                "Ничья, пат!",
                "Игра окончена",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                timer.Stop();
            }
        }

        /// <summary>
        /// Метод для обновления карты
        /// Используется только после хода игрока
        /// </summary>
        void UpdateMap()
        {
            if (game.CurrentPlayer.Color == PlayerColor.White)
                currentPlayerLabel.Text = "Ходят белые";
            else currentPlayerLabel.Text = "Ходят черные";

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var button = buttons[i, j];                    
                    if ((i + j) % 2 == 1) button.BackColor = Color.SandyBrown;
                    else button.BackColor = Color.AntiqueWhite;

                    var figure = game.Map[i, j];
                    if (figure != null)
                    {
                        button.BackgroundImage = game.Map[i, j].Picture;
                        if (figure.Player == game.CurrentPlayer)
                            button.Enabled = true;
                        else button.Enabled = false;
                    }
                    else
                    {
                        button.BackgroundImage = null;
                        button.Enabled = false;
                    }
                }
            RefreshShahColor();
        }

        /// <summary>
        /// Метод для возврата цвета клеток к нормальному от зеленого
        /// </summary>
        void UpdateColorPosition()
        {
            game.FindPosibleWays();
            var list = game.PosiblePositions;
            list.Add(game.PreviousFigure.Position);
            foreach (var cell in list)
            {
                if ((cell.X + cell.Y) % 2 == 1) buttons[cell.X, cell.Y].BackColor = Color.Brown;
                else buttons[cell.X, cell.Y].BackColor = Color.OldLace;
                if (cell != game.PreviousFigure.Position) buttons[cell.X, cell.Y].Enabled = false;
            }
        }
    }
}

