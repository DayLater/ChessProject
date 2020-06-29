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
        int x; 
        public int X 
        { 
            get { return x; }
            set 
            {
                if (value >= 0 && value < 8)
                    x = value;
                else throw new ArgumentException();
            } 
        }

        int y; 
        public int Y 
        {
            get { return y; }
            set
            {
                if (value >= 0 && value < 8)
                    y = value;
                else throw new ArgumentException();
            }
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        //Переопределил для возможности тестов
        public override bool Equals(object obj)
        {
            Position pos = obj as Position;
            if (pos is null) return false;
            return X == pos.X && Y == pos.Y;
        }

        public override int GetHashCode()
        {
            return ((X + 1) * 30549 + (Y + 1) * 101) % 9154981;
        }

        public override string ToString()
        {
            return String.Format("X = {0}, Y = {1}", X, Y);
        }
    }
}
