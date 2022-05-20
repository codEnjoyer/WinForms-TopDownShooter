using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameProject.Entities;
using GameProject.Physics;

namespace GameProject.Domain
{
    internal class Game
    {
        internal static Player Player { get; set; }
        internal static Rectangle GameZone { get; private set; }
        internal static Rectangle CameraZone { get; private set; }
        internal static GameStage Stage { get; private set; } = GameStage.NotStarted;
        internal event Action<GameStage> StageChanged;
        internal static SpawnManager SpawnManager;
        internal static List<Enemy> SpawnedEnemies { get; set; }
        internal static List<Booster> SpawnedBoosters { get; set; }
        //internal static bool KeyPressed = false;

        internal Game()
        {
            
        }
        internal Game(Player player, Rectangle gameZone) : this()
        {
            Player = player;
            GameZone = gameZone;
            ChangeStage(Stage);

            var cameraZoneLocation = new Point(
                GameZone.Location.X + (Screen.PrimaryScreen.WorkingArea.Width - Player.Hitbox.Size.Width) / 2,
                GameZone.Location.Y + (Screen.PrimaryScreen.WorkingArea.Height - Player.Hitbox.Size.Height) / 2);

            var cameraZoneSize = new Size(
                GameZone.Width - Screen.PrimaryScreen.WorkingArea.Width,
                GameZone.Height - Screen.PrimaryScreen.WorkingArea.Height);

            CameraZone = new Rectangle(cameraZoneLocation, cameraZoneSize);

            SpawnedEnemies = new List<Enemy>();
            SpawnedBoosters = new List<Booster>();

            SpawnManager = new SpawnManager();
        }

        private void ChangeStage(GameStage stage)
        {
            Stage = stage;
            StageChanged?.Invoke(stage);
        }
        internal static bool InBounds(Vector location)
        {
            return GameZone.Contains(location.ToPoint());
        }
        internal static bool InBounds(Rectangle hitbox)
        {
            return GameZone.Contains(hitbox);
        }

        internal static bool InCameraBoundsX(Vector location)
        {
            return location.X > CameraZone.Left && location.X < CameraZone.Right;
        }
        internal static bool InCameraBoundsX(Rectangle hitbox)
        {
            return hitbox.Location.X > CameraZone.Left && hitbox.Location.X < CameraZone.Right;
        }

        internal static bool InCameraBoundsY(Vector location)
        {
            return location.Y > CameraZone.Top && location.Y < CameraZone.Bottom;
        }
        internal static bool InCameraBoundsY(Rectangle hitbox)
        {
            return hitbox.Location.Y > CameraZone.Top && hitbox.Location.Y < CameraZone.Bottom;
        }

        internal static void CheckIntersections()
        {
            var spawnedEnemies = new List<Enemy>(SpawnedEnemies);
            var spawnedBoosters = new List<Booster>(SpawnedBoosters);

            foreach (var enemy in spawnedEnemies)
            {
                foreach (var booster in spawnedBoosters)
                {
                    if (Player.Hitbox.IntersectsWith(booster.Hitbox))
                    {
                        if(!Player.GetBoost(booster)) continue;
                        SpawnedBoosters.Remove(booster);
                    }

                    if (enemy.Hitbox.IntersectsWith(booster.Hitbox))
                    {
                        if(!enemy.GetBoost(booster)) continue;
                        SpawnedBoosters.Remove(booster);
                    }

                    if (Player.Hitbox.IntersectsWith(enemy.Hitbox))
                    {
                        Player.DealDamage(enemy);
                        enemy.DealDamage(Player);
                    }
                }
            }
        }
    }
}
