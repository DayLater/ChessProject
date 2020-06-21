using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Player
    {
        public PlayerColor Color { get; }

        public Player(PlayerColor color)
        {
            Color = color;
        }

        public static bool operator ==(Player player1, Player player2)
        {
            return player1.Color == player2.Color;
        }

        public static bool operator !=(Player player1, Player player2)
        {
            return !(player1 == player2);
        }

        public override bool Equals(object obj)
        {
            Player player = obj as Player;
            if (player == null) return false;
            return this == player;
        }
    }
}
