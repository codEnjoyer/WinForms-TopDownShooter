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

            //graphics.DrawRectangle(new Pen(Color.Red), Game.GameZone); //GameZone hitbox
            //graphics.DrawRectangle(new Pen(Color.Blue), Game.CameraZone); //CameraZone hitbox
            //graphics.DrawRectangle(new Pen(Color.Yellow), ViewedZone); //Rectangle covering the observed area (and slightly larger)

            UpdateMovement(graphics);
            UpdateRotation(graphics);
            UpdateBoosters(graphics);
        }

        private static void UpdateCamera(Graphics graphics)
        {
            graphics.TranslateTransform(-(int)Offset.X, -(int)Offset.Y);
        }

        private static void UpdateMovement(Graphics graphics)
        {
            UpdatePlayerMovement(graphics);
            UpdateViewedZone();
            UpdateEnemiesMovement(graphics);
        }

        private static void UpdateRotation(Graphics graphics)
        {
            UpdatePlayerRotation(graphics);
            UpdateEnemiesRotation(graphics);
        }

        private static void UpdateBoosters(Graphics graphics)
        {
            if (Game.SpawnedBoosters.Count == 0) return;

            foreach (var booster in Game.SpawnedBoosters)
            {
                graphics.DrawRectangle(new Pen(Color.BlueViolet), booster.Hitbox);
            }
        }

        private static void UpdatePlayerMovement(Graphics graphics)
        {
            Game.Player.Move();
            graphics.DrawRectangle(new Pen(Color.Green), Game.Player.Hitbox);
        }
        private static void UpdateViewedZone()
        {
            var viewedZoneLocation = new Point(
                Game.Player.Hitbox.Location.X - Screen.PrimaryScreen.WorkingArea.Width,
                Game.Player.Hitbox.Location.Y - Screen.PrimaryScreen.WorkingArea.Height);
            var viewedZoneSize = new Size((int)(2 * Screen.PrimaryScreen.WorkingArea.Width),
                (int)(2 * Screen.PrimaryScreen.WorkingArea.Height));

            ViewedZone = new Rectangle(viewedZoneLocation,
                viewedZoneSize);
        }

        private static void UpdateEnemiesMovement(Graphics graphics)
        {
            if (Game.SpawnedEnemies.Count == 0) return;

            foreach (var enemy in Game.SpawnedEnemies)
            {
                enemy.Move();
                graphics.DrawRectangle(new Pen(Color.Red), enemy.Hitbox);
            }
        }
        private static void UpdatePlayerRotation(Graphics graphics)
        {
            Game.Player.PictureBox.Image?.Dispose();
            var playerBitmap = new Bitmap(Game.Player.Image, Game.Player.PictureBox.Size);
            RotateBitmap(playerBitmap, Game.Player.RotationAngle, graphics, Game.Player.Hitbox.Location);
        }

        private static void UpdateEnemiesRotation(Graphics graphics)
        {
            if (Game.SpawnedEnemies.Count == 0) return;

            foreach (var enemy in Game.SpawnedEnemies)
            {
                enemy.PictureBox.Location = enemy.Hitbox.Location;
                enemy.RotationAngle = enemy.AngleToPlayer();
                enemy.PictureBox.Image?.Dispose();
                var enemyBitmap = new Bitmap(enemy.Image, enemy.PictureBox.Size);
                RotateBitmap(enemyBitmap, enemy.RotationAngle, graphics, enemy.Hitbox.Location);
            }
        }
        
        private static void RotateBitmap(Bitmap bitmap, float angle, Graphics g, Point location)
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
            g.DrawImage(rotated, location);
        }
    }
}
