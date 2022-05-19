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
        internal static Rectangle ViewedZone { get; set; }
        internal static void UpdateTextures(Graphics graphics)
        {
            UpdateCamera(graphics);

            graphics.DrawRectangle(new Pen(Color.Red), Game.GameZone); //GameZone hitbox
            graphics.DrawRectangle(new Pen(Color.Blue), Game.CameraZone); //CameraZone hitbox
            graphics.DrawRectangle(new Pen(Color.Yellow), ViewedZone); //Rectangle covering the observed area (and slightly larger)

            UpdateMovement(graphics);
            UpdateRotation(graphics);
            
        }

        internal static void UpdateCamera(Graphics graphics)
        {
            //if(ViewedZone == default)
            //    ViewedZone = new Rectangle(
            //        new Point(
            //            Screen.PrimaryScreen.WorkingArea.Location.X - Game.Player.Hitbox.Size.Width * 2,
            //            Screen.PrimaryScreen.WorkingArea.Location.Y - Game.Player.Hitbox.Size.Height * 2),
            //        new Size(
            //            Screen.PrimaryScreen.WorkingArea.Width + 4 * Game.Player.Hitbox.Size.Width,
            //            Screen.PrimaryScreen.WorkingArea.Height + 4 * Game.Player.Hitbox.Size.Height));

            graphics.TranslateTransform(-(int)Offset.X, -(int)Offset.Y);
        }

        internal static void UpdateMovement(Graphics graphics)
        {
            Game.Player.Move();
            ViewedZone = new Rectangle(
                new Point(
                    Game.Player.Hitbox.Location.X - Game.Player.Hitbox.Size.Width * 2,
                    Game.Player.Hitbox.Location.Y - Game.Player.Hitbox.Size.Height * 2),
                ViewedZone.Size);

            graphics.DrawRectangle(new Pen(Color.Green), Game.Player.Hitbox);

            
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
            g.DrawImage(rotated, Game.Player.Hitbox.Location);
        }
    }
}
