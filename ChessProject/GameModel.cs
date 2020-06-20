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
        Player white = new Player("White");
        Player black = new Player("Black");

        //Карта
        public readonly IFigure[,] Map = new IFigure[8, 8];

        public GameModel()
        {
            CreateMap();
        }

        //Метод для заполнения карты фигурами
        void CreateMap() 
        {
            AddElephantsToMap();
        }

        //Метод для добавляния слонов на карту
        void AddElephantsToMap()
        {
            Map[2, 0] = new Elephant(new Position(2, 0), black);
            Map[5, 0] = new Elephant(new Position(5, 0), black);
            Map[2, 7] = new Elephant(new Position(2, 7), white);
            Map[5, 7] = new Elephant(new Position(5, 7), white);
        }

        //Метод для поиска пути конкретной фигуры
        List<Position> FindPosibleWays(IFigure figure)
        {
            return figure.FindPosibleWays(Map);
        }

        void MakeTurn(Position newPos, IFigure figure)
        {
            Map[figure.Position.X, figure.Position.Y] = null;
            Map[newPos.X, newPos.Y] = figure;
            figure.Position = newPos;
        }
    }
}
