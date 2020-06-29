using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Map: IEnumerable
    {
        IFigure[,] map = new IFigure[8, 8];

        public void Add(IFigure figure)
        {
            map[figure.Position.X, figure.Position.Y] = figure; 
        }

        public IFigure this[int x, int y]
        {
            get { return map[x, y]; }
            set { map[x, y] = value;  }
        }

        public Map Clone()
        {
            var clone = new Map();
            clone.map = (IFigure[,])map.Clone();
            return clone;
        }

        public void Clear()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    map[i, j] = null;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var figure in map)
                yield return figure;
        }
    }
}
