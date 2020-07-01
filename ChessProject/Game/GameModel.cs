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
        public List<Position> PosiblePositions { get; set; }
        public Player CurrentPlayer { get; private set; }
        public readonly Map Map = new Map();

        public IFigure PreviousFigure { get; set; }
        public IFigure CurrentFigure { get; set; }

        public bool IsSamePlayer { get { return PreviousFigure.Player == CurrentFigure.Player; } }

        TransformedMaps transformedMaps = new TransformedMaps();

        //запоминаем карту
        public void RememberMap()
        {
            if (CurrentPlayer == white)
                transformedMaps.RememberMap(Map);
        }

        //Даем значение текущей фигуре
        public void SetCurrentFigure(Position figurePosition)
        {
            PreviousFigure = CurrentFigure;
            CurrentFigure = Map[figurePosition.X, figurePosition.Y];
        }

        //начало игры
        public void Start()
        {
            CurrentPlayer = white;
        }

        //поменять игроков местами
        public void SwapPlayer()
        {
            if (CurrentPlayer == white) CurrentPlayer = black;
            else CurrentPlayer = white;
            PreviousFigure = null;
        }

        //конструктор
        public GameModel()
        {
            CreateMap();
        }

        #region Создание модели
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
                Map.Add(new Pawn(new Position(side + dx, i), player));
            Map.Add(new Rook(new Position(side, 0), player));
            Map.Add(new Rook(new Position(side, 7), player));
            Map.Add(new Horse(new Position(side, 1), player));
            Map.Add(new Horse(new Position(side, 6), player));
            Map.Add(new Elephant(new Position(side, 2), player));
            Map.Add(new Elephant(new Position(side, 5), player));
            Map.Add(new Queen(new Position(side, 3), player));
            Map.Add(new King(new Position(side, 4), player));
        }
        #endregion

        //Метод для поиска пути текущей фигуры
        public void FindPosiblePositions()
        {
            PosiblePositions = new List<Position>();
            var king = FindCurrentKing();
            FindCorrectPositions(CurrentFigure, king.Position, PosiblePositions);
        }

        //Отфильтровать только возможные ходы
        void FindCorrectPositions(IFigure figure, Position kingPosition, List<Position> positions)
        {
            var figurePosiblePositions = figure.FindPosibleWays(Map);
            var figurePos = new Position(figure.Position.X, figure.Position.Y);
            foreach (var pos in figurePosiblePositions)
            {
                var tempMap = Map.Clone();
                tempMap[figurePos.X, figurePos.Y].Position = new Position(pos.X, pos.Y);
                tempMap[pos.X, pos.Y] = tempMap[figurePos.X, figurePos.Y];
                tempMap[figurePos.X, figurePos.Y] = null;
                if (!IsShah(tempMap, (King)Map[kingPosition.X, kingPosition.Y], out IFigure shahFigure))
                    positions.Add(pos);
                figure.Position = figurePos;
            }
        }

        //сделать ход для тестов
        public void MakeTurn(Position newPos, IFigure figure)
        {
            Map[figure.Position.X, figure.Position.Y] = null;
            Map[newPos.X, newPos.Y] = figure;
            figure.Move(newPos);
        }

        //сделать ход для формы
        public void MakeTurn(Position newPos)
        {
            MakeTurn(newPos, PreviousFigure);
        }

        //поиск короля текущего игрока
        King FindCurrentKing()
        {
            King king = null;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    IFigure figure = Map[i, j];
                    if (figure != null && figure is King && figure.Player == CurrentPlayer)
                        king = (King)figure;
                }
            return king;
        }

        #region Проверки игры на шах, мат, пат и ничью
        //проверка на шах для формы
        public bool IsShah(out IFigure shahFigure, out King shahKing)
        {
            shahKing = FindCurrentKing();
            return IsShah(Map, shahKing, out shahFigure);
        }

        //проверка на шах внутренняя
        bool IsShah(Map map, King shahKing, out IFigure shahFigure)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var figure = map[i, j];
                    if (figure != null && figure.Player != shahKing.Player)
                    {
                        var figuresPositions = figure.FindPosibleWays(map);
                        if (figuresPositions.Contains(shahKing.Position))
                        {
                            shahFigure = figure;
                            return true;
                        }
                    }
                }
            shahFigure = null;
            return false;
        }

        //Находим состояние игры. Если все норм, то null
        public string FindStateOfGame()
        {
            var king = FindCurrentKing();
            if (IsImposibleMate())
                return "Мат в данных обстоятельствах невозможен. Ничья";
            if (IsShah(Map, king, out IFigure shahFigure))
            {
                if (IsMate(king, shahFigure))
                    return "Шах и мат";
            }
            else if (IsStalemate(king))
                return "Пат. Ничья";
            else if (transformedMaps.IsRepeatedMapDraw())
                return "Карты повторилась 3 раза. Ничья";
            return null;
        }

        //проверка на невозможность мата. Если мат невозможен => ничья
        public bool IsImposibleMate()
        {
            List<IFigure> figures = new List<IFigure>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Map[i, j] != null)
                        figures.Add(Map[i, j]);
                    if (figures.Count > 3)
                        return false;
                }
            if (figures.Count < 2)
                throw new Exception("Фигур не может быть меньше 2. Короли умереть не могут");
            else if (figures.Count == 2)
                return true;
            else
            {
                foreach (var figure in figures)
                    if (figure is Elephant || figure is Horse)
                        return true;
            }
            return false;
        }

        //проверка на пат
        public bool IsStalemate(King king)
        {
            var listPositionsPlayer = new List<Position>();
            foreach (IFigure figure in Map)
            {
                if (figure != null && figure.Player.Equals(king.Player))
                    FindCorrectPositions(figure, king.Position, listPositionsPlayer);
                if (listPositionsPlayer.Count > 0)
                    return false;
            }
            return true;
        }

        //проверка на мат
        public bool IsMate(King king, IFigure shahFigure)
        {
            var listPositionsPlayer = new List<Position>();
            var path = GetPositionsThreateningTheKing(king.Position, shahFigure.Position);
            if (king.FindPosibleWays(Map).Count == 0)
            {
                foreach (IFigure figure in Map)
                    if (figure != null && figure.Player.Equals(king.Player))
                        FindCorrectPositions(figure, king.Position, listPositionsPlayer);
                listPositionsPlayer = listPositionsPlayer.Distinct().ToList();
                foreach (var p in listPositionsPlayer)
                    if (path.Contains(p)) return false;
                return true;
            }
            return false;
        }
        #endregion

        //поиск всеможнных позиций для защиты короля
        List<Position> GetPositionsThreateningTheKing(Position kingPosition, Position figurePosition)
        {
            var result = new List<Position>();
            var figure = Map[figurePosition.X, figurePosition.Y];
            var path = figure.FindPosibleWays(Map);
            if (!(figure is Horse) && !(figure is Pawn))
            {
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X > f.X && k.Y > f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X > f.X && k.Y < f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X > f.X && k.Y == f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                   (k, f) => k.X < f.X && k.Y > f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                   (k, f) => k.X < f.X && k.Y < f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                    (k, f) => k.X < f.X && k.Y == f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                (k, f) => k.X == f.X && k.Y > f.Y);
                SelectAListOfPositionsThreateningTheKing(kingPosition, figurePosition, path, result,
                   (k, f) => k.X == f.X && k.Y < f.Y);
            }
            result.Add(figure.Position);
            return result;
        }

        //частный поиск позиций для защиты короля
        void SelectAListOfPositionsThreateningTheKing(Position kingPosition, Position figurePosition,
                               List<Position> path, List<Position> result,
                               Func<Position, Position, bool> predicate)
        {
            if (predicate(kingPosition, figurePosition))
                foreach (var p in path.Where(p => predicate(p, figurePosition)))
                    result.Add(p);
        }

        //проверка достигла ли пешка края
        public bool IsPawnTransformation()
        {
            if (PreviousFigure != null && PreviousFigure is Pawn && (PreviousFigure.Position.X == 0 || PreviousFigure.Position.X == 7))
                return true;
            return false;
        }

        //трансормация пешки в другую фигуру
        public void PawnTransformation(IFigure figure)
        {
            Map[PreviousFigure.Position.X, PreviousFigure.Position.Y] = figure;
        }
    }
}
