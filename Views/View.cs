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

            UpdateBoosters(graphics);
            UpdateMovement(graphics);
            UpdateHealth(graphics);
            UpdateRotation(graphics);
            
        }

        private static void UpdateCamera(Graphics graphics)
        {
            graphics.TranslateTransform(-(int)Offset.X, -(int)Offset.Y);
        }

        private static void UpdateMovement(Graphics graphics)
        {
            UpdateEnemiesMovement(graphics);
            UpdatePlayerMovement(graphics);
            UpdateViewedZone();
        }
        private static void UpdateHealth(Graphics graphics)
        {
            UpdatePlayerHealth(graphics);
            UpdateEnemiesHealth(graphics);
        }

        private static void UpdatePlayerHealth(Graphics graphics)
        {
            var playerHealthBarPosition = new Point(
                Game.Player.Hitbox.Location.X + (int)(0.25 * Game.Player.Hitbox.Width),
                Game.Player.Hitbox.Location.Y - 10);

            graphics.DrawRectangle(Pens.Black,
                playerHealthBarPosition.X,
                playerHealthBarPosition.Y,
                Game.Player.HealthBar.Width,
                Game.Player.HealthBar.Height);

            graphics.FillRectangle(Brushes.Green,
                playerHealthBarPosition.X,
                playerHealthBarPosition.Y,
                Game.Player.HealthBar.Width * Game.Player.GetHPPercent(),
                Game.Player.HealthBar.Height);
        }
        private static void UpdateEnemiesHealth(Graphics graphics)
        {
            if(Game.SpawnedEnemies.Count == 0) return;
            foreach (var enemy in Game.SpawnedEnemies)
            {
                var enemyHealthBarPosition = new Point(
                    enemy.Hitbox.Location.X + (int)(0.25 * enemy.Hitbox.Width),
                    enemy.Hitbox.Location.Y - 10);

            graphics.DrawRectangle(Pens.Black,
                enemyHealthBarPosition.X,
                enemyHealthBarPosition.Y,
                enemy.HealthBar.Width,
                enemy.HealthBar.Height);

            graphics.FillRectangle(Brushes.Red,
                enemyHealthBarPosition.X,
                enemyHealthBarPosition.Y,
                enemy.HealthBar.Width * enemy.GetHPPercent(),
                enemy.HealthBar.Height);
            }
            
        }
        private static void UpdateRotation(Graphics graphics)
        {
            UpdateEnemiesRotation(graphics);
            UpdatePlayerRotation(graphics);
        }

        private static void UpdateBoosters(Graphics graphics)
        {
            if (Game.SpawnedBoosters.Count == 0) return;

            foreach (var booster in Game.SpawnedBoosters)
            {
                var image = new Bitmap(booster.Hitbox.Width, booster.Hitbox.Height);
                using (var g = Graphics.FromImage(image))
                {
                    g.DrawImage(booster.Image, 0, 0, booster.Hitbox.Width, booster.Hitbox.Height);
                }
                graphics.DrawImage(image, booster.Hitbox.Location);

                //graphics.DrawRectangle(new Pen(Color.MediumPurple), booster.Hitbox);
            }
        }

        private static void UpdatePlayerMovement(Graphics graphics)
        {
            Game.Player.Move();
            //graphics.DrawRectangle(new Pen(Color.Green), Game.Player.Hitbox);
            Game.Player.PictureBox.BringToFront();
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
                //graphics.DrawRectangle(new Pen(Color.Red), enemy.Hitbox);
            }
        }
        private static void UpdatePlayerRotation(Graphics graphics)
        {
            Game.Player.PictureBox.Image?.Dispose();
            var playerBitmap = new Bitmap(Game.Player.Image, Game.Player.PictureBox.Size);
            RotateBitmap(playerBitmap, Game.Player.RotationAngle, graphics, Game.Player.Hitbox.Location);
            Game.Player.PictureBox.BringToFront();
        }

        private static void UpdateEnemiesRotation(Graphics graphics)
        {
            if (Game.SpawnedEnemies.Count == 0) return;

            foreach (var enemy in Game.SpawnedEnemies)
            {
                enemy.RotationAngle = enemy.AngleToPlayer();
                enemy.PictureBox.Image?.Dispose();
                var enemyBitmap = new Bitmap(enemy.Image, enemy.PictureBox.Size);
                RotateBitmap(enemyBitmap, enemy.RotationAngle, graphics, enemy.Hitbox.Location);
            }
        }
        
        private static void RotateBitmap(Bitmap bitmap, float angle, Graphics graphics, Point location)
        {
            const float convertToDegree = 180 / (float)Math.PI;
            var rotated = new Bitmap(bitmap.Width, bitmap.Height);

            using (var g = Graphics.FromImage(rotated))
            {
                g.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                g.RotateTransform(angle * convertToDegree);
                g.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            }
            graphics.DrawImage(rotated, location);
        }
    }
}
