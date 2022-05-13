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

namespace GameProject
{
    internal class View
    {
        internal static Vector Offset = Vector.Zero;
        internal static void UpdateTextures(Graphics graphics)
        {
            UpdateMovement(graphics);
            UpdateRotation(graphics);
        }

        internal static void UpdateMovement(Graphics graphics)
        {
            Game.Player.Move();
        }

        internal static void UpdateRotation(Graphics graphics)
        {
            Game.Player.PictureBox.Image?.Dispose();
            var bitmap = new Bitmap(Game.Player.Image, Game.Player.PictureBox.Size);

            Game.Player.PictureBox.Image = RotateBitmap(bitmap, Game.Player.RotationAngle);
            graphics.DrawImage(Game.Player.PictureBox.Image, Game.Player.Location.ToPoint());
        }

        internal static Bitmap RotateBitmap(Bitmap bitmap, float angle)
        {
            const float convertToDegree = 180 / (float)Math.PI;

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                graphics.RotateTransform(angle * convertToDegree);
                graphics.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                graphics.DrawImage(bitmap, new PointF(0, 0));
            }
            return bitmap;
        }
    }
}
