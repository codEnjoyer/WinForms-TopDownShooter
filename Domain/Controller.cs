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
                    Game.Player.ActiveBoosters[BoosterTypes.SpeedBoost] = 10000;
                    break;
                case MouseButtons.Right:
                    Game.Player.GetHealthBoost(50 * 1000);
                    break;
            }
        }
    }
}
