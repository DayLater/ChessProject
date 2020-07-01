using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class TransformedMaps
    {
        readonly List<int[,]> mapStates = new List<int[,]>();

        //запоминание карты и перевод ее в массив интов
        //сделал так, потому что при копировании карты, сами фигуры остаются те же 
        public void RememberMap(Map map)
        {
            int[,] transformMap = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    IFigure figure = map[i, j];
                    if (figure == null)
                        transformMap[i, j] = 0;
                    else
                    {
                        if (figure.Player.Color == PlayerColor.White)
                            transformMap[i, j] += 10;
                        else transformMap[i, j] += 20;
                        if (figure is Pawn) transformMap[i, j] += 1;
                        else if (figure is Rook) transformMap[i, j] += 2;
                        else if (figure is Horse) transformMap[i, j] += 3;
                        else if (figure is Elephant) transformMap[i, j] += 4;
                        else if (figure is Queen) transformMap[i, j] += 5;
                        else transformMap[i, j] += 6;
                    }
                }
            if (mapStates.Count == 7)
                mapStates.RemoveAt(0);
            mapStates.Add(transformMap);
        }

        //Проверка на ничью
        //если карта за белого 3 раза повторится, то объявлена ничья
        public bool IsRepeatedMapDraw()
        {
            if (mapStates.Count == 7)
            {
                for (int i = 0; i < 6; i += 2)
                    if (!IsEquals(mapStates[i], mapStates[i + 2]))
                        return false;
                return true;
            }
            return false;
        }

        //проверяет две карты на одинаковость
        bool IsEquals(int[,] first, int[,] second)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (first[i, j] != second[i, j])
                        return false;
            return true;
        }
    }
}
