using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using GameProject.Entities;

namespace GameProject.Domain
{
    internal class Game
    {
        internal Player Player{ get; }

        internal Game(Player player)
        {
            Player = new Player();
        }
    }
}
