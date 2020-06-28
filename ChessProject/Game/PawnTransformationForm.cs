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

        public PawnTransformationForm(IFigure figure)
        {
            InitializeComponent();
            Figure = new Queen(figure.Position, figure.Player);
            queenButton.Click += (s, a) => { Close(); };
            horseButton.Click += (s, a) => { Figure = new Horse(figure.Position, figure.Player); Close(); };
            rookButton.Click += (s, a) => { Figure = new Rook(figure.Position, figure.Player); Close(); };
            elephantButton.Click += (s, a) => { Figure = new Elephant(figure.Position, figure.Player); Close(); };
        }

        private void InitializeComponent()
        {
            this.queenButton = new Button();
            this.horseButton = new Button();
            this.rookButton = new Button();
            this.elephantButton = new Button();
            this.label1 = new Label();
            this.SuspendLayout();

            this.queenButton.Location = new Point(25, 70);
            this.queenButton.Name = "queenButton";
            this.queenButton.Size = new Size(80, 80);
            this.queenButton.TabIndex = 0;
            this.queenButton.Text = "Ферзь";
            this.queenButton.UseVisualStyleBackColor = true;

            this.horseButton.Location = new Point(111, 70);
            this.horseButton.Name = "horseButton";
            this.horseButton.Size = new Size(80, 80);
            this.horseButton.TabIndex = 1;
            this.horseButton.Text = "Конь";
            this.horseButton.UseVisualStyleBackColor = true;
            
            this.rookButton.Location = new Point(25, 156);
            this.rookButton.Name = "rookButton";
            this.rookButton.Size = new Size(80, 80);
            this.rookButton.TabIndex = 2;
            this.rookButton.Text = "Ладья";
            this.rookButton.UseVisualStyleBackColor = true;

            this.elephantButton.Location = new Point(111, 156);
            this.elephantButton.Name = "elephantButton";
            this.elephantButton.Size = new Size(80, 80);
            this.elephantButton.TabIndex = 3;
            this.elephantButton.Text = "Слон";
            this.elephantButton.UseVisualStyleBackColor = true;

            this.label1.AutoSize = true;
            this.label1.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new Point(21, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(156, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выберите фигуру:";

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(224, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.elephantButton);
            this.Controls.Add(this.rookButton);
            this.Controls.Add(this.horseButton);
            this.Controls.Add(this.queenButton);
            this.Name = "PawnTransformationForm";
            this.Text = "Замена пешки";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button queenButton;
        private Button horseButton;
        private Button rookButton;
        private Button elephantButton;
        private Label label1;
    }
}
