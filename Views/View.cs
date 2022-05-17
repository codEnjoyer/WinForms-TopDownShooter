using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Entities;
using GameProject.Physics;
using GameProject.Views;

namespace GameProject
{
    internal class View
    {
        internal static Vector Offset = Vector.Zero;
        internal static void UpdateTextures(Graphics graphics)
        {
            UpdateCamera(graphics);

            graphics.DrawRectangle(new Pen(Color.Red), new Rectangle(Game.GameZone.Location, Game.GameZone.Size)); //GameZone hitbox
            graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle(Game.CameraZone.Location, Game.CameraZone.Size)); //CameraZone hitbox

            UpdateMovement(graphics);
            UpdateRotation(graphics);
        }

        internal static void UpdateCamera(Graphics graphics)
        {
            graphics.TranslateTransform(-(int)Offset.X, -(int)Offset.Y);
        }

        internal static void UpdateMovement(Graphics graphics)
        {
            Game.Player.Move();
            var playerLocation = Game.Player.Location.ToPoint();
            graphics.DrawRectangle(new Pen(Color.Green), new Rectangle(playerLocation, Game.Player.Size)); //Player hitbox
        }

        internal static void UpdateRotation(Graphics graphics)
        {
            Game.Player.PictureBox.Image?.Dispose();
            var bitmap = new Bitmap(Game.Player.Image, Game.Player.PictureBox.Size);
            RotateBitmap(bitmap, Game.Player.RotationAngle, graphics);
        }

        internal static void RotateBitmap(Bitmap bitmap, float angle, Graphics g)
        {
            const float convertToDegree = 180 / (float)Math.PI;
            var rotated = new Bitmap(bitmap.Width, bitmap.Height);

            using (var graphics = Graphics.FromImage(rotated))
            {
                graphics.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                graphics.RotateTransform(angle * convertToDegree);
                graphics.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            }
            g.DrawImage(rotated, Game.Player.Location.ToPoint());
        }
    }
}
