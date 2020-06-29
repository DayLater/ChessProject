using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class King : IFigure
    {
        public Position Position { get;  set; }
        public Player Player { get; }
        public string Picture => Player.Color == PlayerColor.Black ? "♚" : "♔";

        public void Move(Position newPosition)
        {
            Position = new Position(newPosition.X, newPosition.Y);
        }

        public King(Position position, Player player)
        {
            Position = position;
            Player = player;
        }

        //поиск пути для короля
        public List<Position> FindPosibleWays(Map map)
        {
            var allPosiblePositions = new List<Position>();
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (!(i == j && i == 0))
                        FindPosiblePositionsInDirection(allPosiblePositions, i, j,
                            map, figure => figure.Player != Player); //здесь мы ищем ВСЕ допустимые ходы
           
            var unacceptablePositions = new List<Position>(); //Всевозможные ходы для других фигур
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = map[i, j];
                    if (figure is IFigure && figure.Player != Player)
                        unacceptablePositions.AddRange(figure.UnacceptablePositionsForKing(map)); 
                }
            unacceptablePositions.Distinct(); //убираем повторы

            var result = allPosiblePositions
                .Except(unacceptablePositions)
                .ToList();// убираем все позиции, которые пересекаются в списках
            return result; 
        }

        //ищем позиции в одном направлении
        void FindPosiblePositionsInDirection(List<Position> result, int dx, int dy,
            Map map, Func<IFigure, bool> selector)
        {
            int x = Position.X + dx;
            int y = Position.Y + dy;
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {                    
                if (map[x, y] is null)
                    result.Add(new Position(x, y));
                else if (selector(map[x, y]))
                        result.Add(new Position(x, y));
            }
        }

        public List<Position> UnacceptablePositionsForKing(Map map)
        {
            var allPosiblePositions = new List<Position>();
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (!(i == j && i == 0))
                        FindPosiblePositionsInDirection(allPosiblePositions, i, j,
                            map, figure => true); //здесь мы ищем ВСЕ допустимые ходы
            return allPosiblePositions;
        }
    }
}
