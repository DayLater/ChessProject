using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    // класс позиции
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        //Переопределил для возможности тестов
        public override bool Equals(object obj)
        {
            var pos = obj as Position;
            if (pos is null) return false;
            return X == pos.X && Y == pos.Y;
        }

        public override string ToString()
        {
            return String.Format("X = {0}, Y = {1}", X, Y);
        }
    }
}
