using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GameProject.Entities;

namespace GameProject.Domain
{
    internal class BoosterManager
    {
        private static Timer boosterTimer;

        internal BoosterManager()
        {

            boosterTimer = new Timer();
            boosterTimer.Interval = 50;
            boosterTimer.Tick += (sender, args) => CheckBoostersIntersections();
            boosterTimer.Start();
        }

        internal static void CheckBoostersIntersections()
        {
            if (Game.SpawnedBoosters.Count == 0) return;
            var spawnedBoosters = new List<Booster>(Game.SpawnedBoosters);

            CheckPlayerBoosterIntersections(spawnedBoosters);

            if (Game.SpawnedEnemies.Count == 0) return;
            var spawnedEnemies = new List<Enemy>(Game.SpawnedEnemies);
            CheckEnemyBoosterIntersections(spawnedEnemies, spawnedBoosters);
        }

        private static void CheckEnemyBoosterIntersections(List<Enemy> spawnedEnemies, List<Booster> spawnedBoosters)
        {
            foreach (var booster in spawnedEnemies.SelectMany(enemy => spawnedBoosters
                         .Where(booster => enemy.Hitbox.IntersectsWith(booster.Hitbox))
                         .Where(enemy.GetBoost)))
            {
                Game.SpawnedBoosters.Remove(booster);
            }
        }

        private static void CheckPlayerBoosterIntersections(List<Booster> spawnedBoosters)
        {
            foreach (var booster in spawnedBoosters
                         .Where(booster => Game.Player.Hitbox.IntersectsWith(booster.Hitbox))
                         .Where(booster => Game.Player.GetBoost(booster)))
            {
                Game.SpawnedBoosters.Remove(booster);
            }
        }
    }
}