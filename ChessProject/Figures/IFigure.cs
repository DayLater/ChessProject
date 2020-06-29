using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public interface IFigure
    {
        Position Position { get; set; }
        Player Player { get;}
        string Picture { get; }

        /// <summary>
        /// Метод для передвижения фигуры на новую позицию
        /// </summary>
        /// <param name="newPosition"></param>
        void Move(Position newPosition);

        /// <summary>
        /// метод возвращающий всевозможные ходы для фигуры
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        List<Position> FindPosibleWays(Map map);

        /// <summary>
        /// Метод возвращающий лист неподходящих позиций для вражеского короля от данной фигуры
        /// Нужен лишь как вспомогательный в методе короля
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        List<Position> UnacceptablePositionsForKing(Map map); 
    }
}
