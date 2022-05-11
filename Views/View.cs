using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Domain;
using GameProject.Entities;

namespace GameProject
{
    internal static class View
    {
        internal static void UpdateTextures(Graphics graphics)
        {
            Game.Player.Move();
        }
    }
}
