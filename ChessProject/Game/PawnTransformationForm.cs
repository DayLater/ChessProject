using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject.Game
{
    public partial class PawnTransformationForm : Form
    {
        public IFigure Figure { get; private set; }
        Button queenButton = new Button();
        Button horseButton = new Button();
        Button rookButton = new Button();
        Button elephantButton = new Button();
        Label label1 = new Label();

        public PawnTransformationForm(IFigure figure)
        {
            Figure = new Queen(figure.Position, figure.Player);
            var texts = new[] { new[] { "Ферзь", "Конь" } , new[] { "Ладья", "Слон" } };
            var buttons = new[] { new[] { queenButton, horseButton }, new[] { rookButton, elephantButton } };
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    CreateButton(buttons[i][j], texts[i][j], i, j);
            ClientSize = new Size(160, 140);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Text = "Замена пешки";
            BackColor = Color.GhostWhite;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(160, 20);
            label1.TabIndex = 4;
            label1.Text = "Выберите фигуру:";
            Controls.Add(label1);
            queenButton.Click += (s, a) => { Close(); };
            horseButton.Click += (s, a) => { Figure = new Horse(figure.Position, figure.Player); Close(); };
            rookButton.Click += (s, a) => { Figure = new Rook(figure.Position, figure.Player); Close(); };
            elephantButton.Click += (s, a) => { Figure = new Elephant(figure.Position, figure.Player); Close(); };
        }

        void CreateButton(Button button, string text, int i, int j)
        {
            button.Location = new Point(80 * j, 20 + 60 * i);
            button.Size = new Size(80, 60);
            button.Text = text;
            button.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            Controls.Add(button);
        }
    }
}
