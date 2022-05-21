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
        internal static event Action<GameStage> StageChanged;
        internal static SpawnManager SpawnManager;
        internal static KillManager KillManager;
        internal static List<Enemy> SpawnedEnemies { get; set; }
        internal static List<Booster> SpawnedBoosters { get; set; }
        internal static int Scores { get; set; }

        internal Game(Player player, Rectangle gameZone)
        {
            Player = player;
            GameZone = gameZone;
            
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
            KillManager = new KillManager();

            ChangeStage(GameStage.Battle);
            StageChanged += CheckGameStage;
        }

        internal static void ChangeStage(GameStage stage)
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

        internal static bool InCameraBoundsX(Rectangle hitbox)
        {
            return hitbox.Location.X > CameraZone.Left && hitbox.Location.X < CameraZone.Right;
        }

        internal static bool InCameraBoundsY(Rectangle hitbox)
        {
            return hitbox.Location.Y > CameraZone.Top && hitbox.Location.Y < CameraZone.Bottom;
        }

        internal static void CheckBoostersIntersections()
        {
            if(SpawnedBoosters.Count == 0) return;
            var spawnedBoosters = new List<Booster>(SpawnedBoosters);

            CheckPlayerBoosterIntersections(spawnedBoosters);

            if(SpawnedEnemies.Count == 0) return;
            var spawnedEnemies = new List<Enemy>(SpawnedEnemies);
            CheckEnemyBoosterIntersections(spawnedEnemies, spawnedBoosters);
        }

        private static void CheckEnemyBoosterIntersections(List<Enemy> spawnedEnemies, List<Booster> spawnedBoosters)
        {
            foreach (var booster in spawnedEnemies.SelectMany(enemy => spawnedBoosters
                         .Where(booster => enemy.Hitbox.IntersectsWith(booster.Hitbox))
                         .Where(enemy.GetBoost)))
            {
                SpawnedBoosters.Remove(booster);
            }
        }

        private static void CheckPlayerBoosterIntersections(List<Booster> spawnedBoosters)
        {
            foreach (var booster in spawnedBoosters
                         .Where(booster => Player.Hitbox.IntersectsWith(booster.Hitbox))
                         .Where(booster => Player.GetBoost(booster)))
            {
                SpawnedBoosters.Remove(booster);
            }
        }

        private static void CheckGameStage(GameStage gameStage)
        {
            switch (gameStage)
            {
                case GameStage.Finished:
                    
                    break;
            }
        }
    }
}
