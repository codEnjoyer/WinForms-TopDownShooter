using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject.Domain
{
    internal class Controller
    {
        internal static void ControlKeys(Keys pressed, bool isActive)
        {
            switch (pressed)
            {
                case Keys.W:
                    Game.Player.Up = isActive;
                    break;
                case Keys.A:
                    Game.Player.Left = isActive;
                    break;
                case Keys.S:
                    Game.Player.Down = isActive;
                    break;
                case Keys.D:
                    Game.Player.Right = isActive;
                    break;

                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.R:
                    Application.Restart();
                    break;
            }
        }

        internal static void ControlMouse(MouseEventArgs e)
        {
            var cursorLocation = e.Location;
        }
    }
}
