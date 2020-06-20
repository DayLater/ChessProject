using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    public class Player
    {
        public string Name { get; }

        public Player(string name)
        {
            Name = name;
        }

        public static bool operator ==(Player player1, Player player2)
        {
            return player1.Name == player2.Name;
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
