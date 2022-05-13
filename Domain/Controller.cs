using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Physics;

namespace GameProject.Domain
{
    internal class Controller
    {
        internal static void ControlKeys(Keys pressed, bool isActive)
        {
            switch (pressed)
            {
                case Keys.W:
                    Game.Player.Forward = isActive;
                    break;
                case Keys.A:
                    Game.Player.Left = isActive;
                    break;
                case Keys.S:
                    Game.Player.Back = isActive;
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
            var cursorLocation = new Vector(e.Location.X, e.Location.Y);
            var angleToCursor = Game.Player.AngleToTarget(cursorLocation); //in radians

            Game.Player.GetMouseRotation(angleToCursor);
        }
    }
}
