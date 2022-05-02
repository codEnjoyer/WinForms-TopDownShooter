using System.Collections.Generic;
using System.Linq;
using GameProject.Entities;

namespace GameProject.Domain
{
    internal class Game
    {
        internal Player Player{ get; }

        internal Game(Player player)
        {
            Player = player;
        }
    }
}
