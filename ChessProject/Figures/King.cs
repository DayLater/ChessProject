using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class King : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public Image Picture => Player.Color == PlayerColor.Black ? Properties.Resources.KingB : Properties.Resources.KingW;
        public bool IsFirstStep { get; private set; } = true;

        public bool IsCastlingPosible(Rook rook, Map map)
        {
            if (rook == null || !IsFirstStep || !rook.IsFirstStep)
                return false;
            var dy = -1;
            if (Position.Y < rook.Position.Y) dy = 1;
            var minIndex = Math.Min(rook.Position.Y, Position.Y);
            var maxIndex = Math.Max(rook.Position.Y, Position.Y);
            for (int i = minIndex + 1; i < maxIndex; i++)
                if (map[Position.X, i] != null)
                    return false;
            for (int i = 0; i < 2; i++)
                if (IsShah(map, new Position(Position.X, Position.Y + i * dy))) 
                    return false;
            return true;
        }

        bool IsShah(Map map, Position position)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = map[i, j];
                    if (figure != null && figure.Player != Player
                        && figure.FindPosibleWays(map).Contains(position))
                        return true;
                }
            return false;
        }

        public void Move(Position newPosition)
        {
            Position = new Position(newPosition.X, newPosition.Y);
            IsFirstStep = false; 
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

            var x = 0;
            if (Player.Color == PlayerColor.White)
                x = 7;
            var rookPositions = new[] { new Position(x, 0), new Position(x, 7) };
            foreach (var rookPos in rookPositions)
            {
                var rook = map[rookPos.X, rookPos.Y] as Rook;
                if (IsCastlingPosible(rook, map))
                    result.Add(rookPos);
            }
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
