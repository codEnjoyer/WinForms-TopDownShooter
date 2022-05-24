using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Entities;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject
{
    internal class View
    {
        private static bool Initialized;
        internal static Vector Offset = Vector.Zero;
        internal static Rectangle ViewedZone { get; set; }
        internal static Form Form { get; set; }
        internal static bool IsFullscreen { get; set; }
        internal static Label testLabel;
        internal static Label ScoresLabel { get; set; }
        internal static ProgressBar speedTimeBar { get; set; }
        internal static ProgressBar damageTimeBar { get; set; }
        internal static ProgressBar healthTimeBar { get; set; }

        internal static void UpdateTextures(Graphics graphics, Form form)
        {
            Form = form;
            Form.Cursor = Cursors.Cross;
            if (!Initialized)
            {
                GoFullscreen(true);
                ShowUserInterface();
                InitializeUserInterface();
            }

            var gameStage = Game.Stage;

            switch (gameStage)
            {
                case GameStage.Battle:
                    UpdateCamera(graphics);

                    //graphics.DrawRectangle(new Pen(Color.Red), Game.GameZone); //GameZone hitbox
                    //graphics.DrawRectangle(new Pen(Color.Blue), Game.CameraZone); //CameraZone hitbox
                    //graphics.DrawRectangle(new Pen(Color.Yellow), ViewedZone); //Rectangle covering the observed area (and slightly larger)

                    UpdateBoosters(graphics);
                    UpdateMovement(graphics);
                    UpdateHealth(graphics);
                    UpdateRotation(graphics);
                    break;

                case GameStage.Finished:
                    ShowFinishWindow();
                    MainForm.MainTimer.Stop();
                    return;
            }
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
                Game.Player.HealthBar.Width * Game.Player.GetHpPercent(),
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
                enemy.HealthBar.Width * enemy.GetHpPercent(),
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
            UpdateSpawnedBoosters(graphics);
            UpdateBoostersUserInterface();
            UpdateEnemiesBoostersVisual(graphics);
        }

        private static void UpdateSpawnedBoosters(Graphics graphics)
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

        private static void UpdateBoostersUserInterface()
        {
            if (Game.Player.ActiveBoosters[BoosterTypes.HealthBoost] != 0)
                healthTimeBar.Value = (int)(Game.Player.ActiveBoosters[BoosterTypes.HealthBoost] / (double)(10 * 1000) * 10 * 1000);

            if (Game.Player.ActiveBoosters[BoosterTypes.DamageBoost] != 0)
                damageTimeBar.Value = (int)(Game.Player.ActiveBoosters[BoosterTypes.DamageBoost] / (double)(10 * 1000) * 10 * 1000);

            if (Game.Player.ActiveBoosters[BoosterTypes.SpeedBoost] != 0)
                speedTimeBar.Value =(int)(Game.Player.ActiveBoosters[BoosterTypes.SpeedBoost] / (double)(10 * 1000) * 10 * 1000);
        }

        private static void UpdateEnemiesBoostersVisual(Graphics graphics)
        {
            if (Game.SpawnedEnemies.Count == 0) return;
            foreach (var enemy in Game.SpawnedEnemies)
            {
                if (enemy.ActiveBoosters[BoosterTypes.HealthBoost] != 0)
                {
                    var enemyHealthTimeBarLocation = new Point(enemy.Hitbox.Left + (int)(0.25 * enemy.Hitbox.Width), enemy.Hitbox.Bottom + 5);
                    graphics.DrawRectangle(Pens.Black, enemyHealthTimeBarLocation.X, enemyHealthTimeBarLocation.Y, (int)(0.5 * enemy.Hitbox.Width), 10);
                    graphics.FillRectangle(Brushes.LightGreen, enemyHealthTimeBarLocation.X, enemyHealthTimeBarLocation.Y,
                        (int)(enemy.ActiveBoosters[BoosterTypes.HealthBoost] / (double)(10 * 1000) * (int)(0.5 * enemy.Hitbox.Width)), 10);
                }

                if (enemy.ActiveBoosters[BoosterTypes.DamageBoost] != 0)
                {
                    var enemyDamageTimeBarLocation = new Point(enemy.Hitbox.Left + (int)(0.25 * enemy.Hitbox.Width), enemy.Hitbox.Bottom + 20);
                    graphics.DrawRectangle(Pens.Black, enemyDamageTimeBarLocation.X, enemyDamageTimeBarLocation.Y, (int)(0.5 * enemy.Hitbox.Width), 10);
                    graphics.FillRectangle(Brushes.DarkOrange, enemyDamageTimeBarLocation.X, enemyDamageTimeBarLocation.Y,
                        (int)(enemy.ActiveBoosters[BoosterTypes.DamageBoost] / (double)(10 * 1000) * (int)(0.5 * enemy.Hitbox.Width)), 10);
                }

                if (enemy.ActiveBoosters[BoosterTypes.SpeedBoost] != 0)
                {
                    var enemySpeedTimeBarLocation = new Point(enemy.Hitbox.Left + (int)(0.25 * enemy.Hitbox.Width), enemy.Hitbox.Bottom + 35);
                    graphics.DrawRectangle(Pens.Black, enemySpeedTimeBarLocation.X, enemySpeedTimeBarLocation.Y, (int)(0.5 * enemy.Hitbox.Width), 10);
                    graphics.FillRectangle(Brushes.DodgerBlue, enemySpeedTimeBarLocation.X, enemySpeedTimeBarLocation.Y,
                        (int)(enemy.ActiveBoosters[BoosterTypes.SpeedBoost] / (double)(10 * 1000) * (int)(0.5 * enemy.Hitbox.Width)), 10);
                }
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
                Game.Player.Hitbox.Location.X - Form.ClientSize.Width,
                Game.Player.Hitbox.Location.Y - Form.ClientSize.Height);

            var viewedZoneSize = new Size(2 * Form.ClientSize.Width, 2 * Form.ClientSize.Height);

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

        private static void ShowFinishWindow()
        {
            Form.Controls.Remove(ScoresLabel);

            var restart = new Button
            {
                Text = "Restart",
                Location = new Point(500, 400),
                Size = new Size(500, 100),
                Font = new Font(FontFamily.GenericMonospace, 40, FontStyle.Bold)
            };

            var result = new Label()
            {
                Text = "Your result: " + Game.Scores,
                Location = new Point(restart.Left + restart.Width / 4 + 10, restart.Top - 30),
                Size = new Size(restart.Width / 2, 20),
                Font = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold)
            };
            restart.Click += (s, e) => Application.Restart();

            Form.Controls.Add(restart);
            Form.Controls.Add(result);

        }
        internal static void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                //TopMost = true;
                Form.FormBorderStyle = FormBorderStyle.None;
                Form.WindowState = FormWindowState.Maximized;
                IsFullscreen = true;
            }
            else
            {
                Form.FormBorderStyle = FormBorderStyle.Sizable;
                Form.WindowState = FormWindowState.Normal;
                Form.Bounds = Screen.PrimaryScreen.Bounds;
                IsFullscreen = false;
            }
        }

        private static void ShowUserInterface()
        {
            testLabel = new Label
            {
                Location = new Point(50, 50),
                Size = new Size(90, 70),
            };
            Form.Controls.Add(testLabel);

            ScoresLabel = new Label
            {
                Location = new Point(Form.Right - 200, 50),
                Size = new Size(200, 20),
                Font = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold)
            };
            Form.Controls.Add(ScoresLabel);

            speedTimeBar = new ProgressBar
            {
                Location = new Point(Form.Left + 30, Form.Bottom - 70),
                Size = new Size(150, 30),
                Minimum = 0,
                Maximum = 10 * 1000,
            };
            Form.Controls.Add(speedTimeBar);

            damageTimeBar = new ProgressBar
            {
                Location = new Point(speedTimeBar.Left, speedTimeBar.Top - 40),
                Size = new Size(150, 30),
                Minimum = 0,
                Maximum = 10 * 1000,
            };
            Form.Controls.Add(damageTimeBar);

            healthTimeBar = new ProgressBar
            {
                Location = new Point(damageTimeBar.Left, damageTimeBar.Top - 40),
                Size = new Size(150, 30),
                Minimum = 0,
                Maximum = 10 * 1000,
            };
            Form.Controls.Add(healthTimeBar);
        }

        private static void InitializeUserInterface()
        {
            Form.MouseMove += (s, e) =>
            {
                testLabel.Text = "Camera offset: " + Offset + "\nPlayer location: " + Game.Player.Hitbox.Location;
                ScoresLabel.Text = "Score:" + Game.Scores;
            };
            Form.KeyDown += (s, e) =>
            {
                testLabel.Text = "Camera offset: " + Offset + "\nPlayer location: " + Game.Player.Hitbox.Location;
            };
            Initialized = true;
        }
    }
}
