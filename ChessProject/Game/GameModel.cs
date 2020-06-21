using ChessProject.Figures;
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
        Player white = new Player(PlayerColor.White);
        Player black = new Player(PlayerColor.Black);

        //Карта
        public readonly IFigure[,] Map = new IFigure[8, 8];

        public GameModel()
        {
            CreateMap();
        }

        //Метод для заполнения карты фигурами
        void CreateMap() 
        {
            AddOneSide(white);
            AddOneSide(black);
        }

        //Добавляет играющую сторону
        void AddOneSide(Player player)
        {
            var side = 0;
            var dx = 1;
            if (player == white)
            {
                side = 7;
                dx = -1;
            }
            for (int i = 0; i < 8; i++)
                Map[side + dx, i] = new Pawn(new Position(side + dx, i), player);
            Map[side, 0] = new Rook(new Position(side, 0), player);
            Map[side, 7] = new Rook(new Position(side, 7), player);
            Map[side, 1] = new Horse(new Position(side, 1), player);
            Map[side, 6] = new Horse(new Position(side, 6), player);
            Map[side, 2] = new Elephant(new Position(side, 2), player);
            Map[side, 5] = new Elephant(new Position(side, 5), player);
            Map[side, 3] = new Queen(new Position(side, 3), player);
            Map[side, 4] = new King(new Position(side, 4), player);
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
            figure.Move(newPos);
        }
    }
}
