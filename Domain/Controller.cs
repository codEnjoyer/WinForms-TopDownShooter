using System.Windows.Forms;
using GameProject.Physics;

namespace GameProject.Domain
{
    internal class Controller
    {
        internal static void ControlKeys(Keys key, bool isActive)
        {
            switch (key)
            {
                case Keys.W:
                    Game.Player.IsMovingUp = isActive;
                    break;
                case Keys.A:
                    Game.Player.IsMovingLeft = isActive;
                    break;
                case Keys.S:
                    Game.Player.IsMovingDown = isActive;
                    break;
                case Keys.D:
                    Game.Player.IsMovingRight = isActive;
                    break;

                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.R:
                    Application.Restart();
                    break;
                //case Keys.F:
                //    View.GoFullscreen();
                //    break;
            }
        }

        internal static void ControlMouse(MouseEventArgs e)
        {

            var cursorLocationOnScreen = new Vector(e.Location.X, e.Location.Y);
            var pressedButton = e.Button;

            var cursorLocationWithOffset = cursorLocationOnScreen + View.Offset;
            var angleToCursor = Game.Player.AngleToTarget(cursorLocationWithOffset); //in radians

            Game.Player.RotationAngle = angleToCursor;

            switch (pressedButton)
            {
                case MouseButtons.Left:
                    Game.Player.GetSpeed(5);
                    break;
                case MouseButtons.Right:
                    Game.Player.GetHealth(50);
                    break;
            }
        }
    }
}
