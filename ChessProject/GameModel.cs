using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    //Класс игровой модели
    public class GameModel
    {
        //Игроки
        Player white = new Player("white");
        Player black = new Player("black");

        //Карта
        public readonly IFigure[,] Map = new IFigure[8, 8];

        //Метод для заполнения карты фигурами
        void CreateMap() 
        {
            AddElephantsToMap();
        }

        //Метод для добавляния слонов на карту
        void AddElephantsToMap()
        {
            Map[0, 2] = new Elephant(new Position(0, 2), black);
            Map[0, 5] = new Elephant(new Position(0, 5), black);
            Map[7, 2] = new Elephant(new Position(7, 2), white);
            Map[7, 5] = new Elephant(new Position(7, 5), white);
        }

        //Метод для поиска пути конкретной фигуры
        List<Position> FindPosibleWays(IFigure figure)
        {
            return figure.FindPosibleWays(Map);
        }
    }
}
