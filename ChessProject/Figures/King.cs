using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class King : IFigure
    {
        public Position Position { get; set; }
        public Player Player { get; }
        public bool IsAlive { get; set; }
        public string Picture => Player.Color == PlayerColor.Black ? "♚" : "♔";
        public void Move(Position newPosition) => Position = newPosition;
        public King(Position position, Player player)
        {
            Position = position;
            Player = player;
        }
        public List<Position> FindPosibleWays(IFigure[,] map)
        {
            var result = new List<Position>();
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (!(i == j && i == 0))
                        FindPosiblePositionsInDirection(result, i, j, map);

            var unacceptablePositions = new List<Position>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = map[i, j];
                    if (figure is IFigure && figure.Player != Player && !(figure is King))
                        unacceptablePositions.AddRange(figure.UnacceptablePositionsForKing(map)); 
                }
            unacceptablePositions.Distinct();
            var list = result.Except(unacceptablePositions).ToList();
            return list;
        }


        //ищем позиции в одном направлении
        void FindPosiblePositionsInDirection(List<Position> result, int dx, int dy, IFigure[,] map)
        {
            int x = Position.X + dx;
            int y = Position.Y + dy;
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {                    
                if (map[x, y] is null)
                    result.Add(new Position(x, y));
                else if (map[x, y].Player != Player)
                        result.Add(new Position(x, y));
            }
        }

        public List<Position> UnacceptablePositionsForKing(IFigure[,] map)
        {
           return new List<Position>();
        }
    }
}
