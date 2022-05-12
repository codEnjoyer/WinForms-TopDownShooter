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
            //direction of the mouse by looking at the position of the player in relation to the mouse pointer

            var cursorLocation = new Vector(e.X - (Game.Player.PictureBox.Location.X + Game.Player.Size.Width),
                e.Y - (Game.Player.PictureBox.Location.Y + Game.Player.Size.Height));
            var angleToCursor = Game.Player.AngleToTarget(cursorLocation); //in degrees
            Game.Player.RotationAngle = angleToCursor;
            
            Game.Player.PictureBox.Image?.Dispose();
            var bitmap = new Bitmap(Game.Player.Image, Game.Player.PictureBox.Size);
            
            Game.Player.PictureBox.Image = View.RotateImageMatrix(bitmap, angleToCursor);
        }
    }
}
