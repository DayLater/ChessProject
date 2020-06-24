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
        public List<Position> PosiblePositions;
        public Player CurrentPlayer { get; private set; }
        public bool IsShah { get; private set; }

        //Карта
        public readonly IFigure[,] Map = new IFigure[8, 8];

        public void Start()
        {
            CurrentPlayer = black; 
        }

        public void SwapPlayer()
        {
            if (CurrentPlayer == white) CurrentPlayer = black;
            else CurrentPlayer = white; 
        }

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
        public void FindPosibleWays(IFigure figure)
        {
            PosiblePositions = figure.FindPosibleWays(Map);
        }

        public void MakeTurn(Position newPos, IFigure figure)
        {
            Map[figure.Position.X, figure.Position.Y] = null;
            Map[newPos.X, newPos.Y] = figure;
            figure.Move(newPos);
        }

        public bool IsMate(King king, Position previosPosition) 
        {
            var posiblePositions = king.FindPosibleWays(Map);
            var player = king.Player;
            var enemy = player.Equals(white) ? black : white;
            var listPositionsPlayer = new List<Position>();
            var listPositionEnemy = new List<Position>();
            var path = new List<Position>();
            //foreach (var item in collection)
            //{
            //    path.Add(previosPosition);
            //}
            //PosiblePositions.CopyTo(path);//путь того кто сделал шах
            if (posiblePositions.Count == 0)
            {
                foreach (var cell in Map)
                {
                    if (cell.Player.Equals(player)) 
                    {
                        if (cell is Pawn)
                        {

                        }
                        listPositionsPlayer.AddRange(cell.FindPosibleWays(Map));
                    }
                    if (cell.Player.Equals(enemy)) 
                    {
                        if (cell is Horse)
                        { 
                            
                        }
                        else
                        listPositionEnemy.AddRange(cell.FindPosibleWays(Map));
                    }
                }
                listPositionsPlayer = listPositionsPlayer.Distinct().ToList();
                listPositionEnemy = listPositionEnemy.Distinct().ToList();
                foreach (var p in listPositionsPlayer)
                {
                    if (path.Contains(p))
                        return false;
                }

                return true;
            }
            return false;
        }
    }
}
