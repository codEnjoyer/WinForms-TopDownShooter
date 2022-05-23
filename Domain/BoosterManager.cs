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
            boosterTimer.Tick += (sender, args) => CheckBoostersRemainingTime();
            boosterTimer.Tick += (sender, args) => CheckBoostersIntersections();
            boosterTimer.Start();
        }

        private static void CheckBoostersRemainingTime()
        {
            CheckPlayerBoostersRemainingTime();

            if (Game.SpawnedEnemies.Count == 0) return;
            var spawnedEnemies = new List<Enemy>(Game.SpawnedEnemies);
            CheckEnemiesBoostersRemainingTime(spawnedEnemies);
        }

        private static void CheckPlayerBoostersRemainingTime()
        {
            foreach (var boosterTypeRemainingTime in Game.Player.ActiveBoosters)
            {
                if (boosterTypeRemainingTime.Value <= 0) continue;

                switch (boosterTypeRemainingTime.Key)
                {
                    case BoosterTypes.HealthBoost:
                        Game.Player.GetHealth(10 * 1000 / boosterTimer.Interval);
                        break;

                    case BoosterTypes.DamageBoost:
                        if (boosterTypeRemainingTime.Value == 10 * 1000 && Game.Player.Damage == 10) //Link to existing boosters impact?
                            Game.Player.GetDamage(Game.Player.Damage);
                        break;

                    case BoosterTypes.SpeedBoost:
                        if (boosterTypeRemainingTime.Value == 10 * 1000 && Game.Player.Speed == 5)
                            Game.Player.GetSpeed(Game.Player.Speed);
                        break;
                }

                Game.Player.ActiveBoosters[boosterTypeRemainingTime.Key] = boosterTypeRemainingTime.Value - boosterTimer.Interval;

                if (Game.Player.ActiveBoosters[boosterTypeRemainingTime.Key] == 0)
                {
                    switch (boosterTypeRemainingTime.Key)
                    {
                        case BoosterTypes.DamageBoost:
                            Game.Player.Damage = 10;
                            break;
                        case BoosterTypes.SpeedBoost:
                            Game.Player.Speed = 5;
                            break;
                    }
                }
            }
        }

        private static void CheckEnemiesBoostersRemainingTime(List<Enemy> spawnedEnemies)
        {
            foreach (var enemy in spawnedEnemies)
            {
                foreach (var boosterTypeRemainingTime in enemy.ActiveBoosters)
                {
                    if (boosterTypeRemainingTime.Value <= 0) continue;

                    switch (boosterTypeRemainingTime.Key)
                    {
                        case BoosterTypes.HealthBoost:
                            enemy.GetHealth(10 * 1000 / boosterTimer.Interval);
                            break;

                        case BoosterTypes.DamageBoost:
                            if (boosterTypeRemainingTime.Value == 10 * 1000) //Link to existing boosters impact? +bonus damage field
                                enemy.GetDamage(10);
                            break;

                        case BoosterTypes.SpeedBoost:
                            if (boosterTypeRemainingTime.Value == 10 * 1000)
                                enemy.GetSpeed(5);
                            break;
                    }

                    enemy.ActiveBoosters[boosterTypeRemainingTime.Key] = boosterTypeRemainingTime.Value - boosterTimer.Interval;

                    if (enemy.ActiveBoosters[boosterTypeRemainingTime.Key] == 0)
                    {
                        switch (boosterTypeRemainingTime.Key)
                        {
                            case BoosterTypes.DamageBoost:
                                enemy.Damage = 10;
                                break;
                            case BoosterTypes.SpeedBoost:
                                enemy.Speed = 5;
                                break;
                        }
                    }
                }
            }
            
        }
        private static void CheckBoostersIntersections()
        {
            if (Game.SpawnedBoosters.Count == 0) return;
            var spawnedBoosters = new List<Booster>(Game.SpawnedBoosters);

            CheckPlayerBoosterIntersections(spawnedBoosters);

            if (Game.SpawnedEnemies.Count == 0) return;
            var spawnedEnemies = new List<Enemy>(Game.SpawnedEnemies);
            CheckEnemyBoosterIntersections(spawnedEnemies, spawnedBoosters);
        }

        private static void CheckPlayerBoosterIntersections(List<Booster> spawnedBoosters)
        {
            foreach (var booster in spawnedBoosters)
            {
                if (Game.Player.Hitbox.IntersectsWith(booster.Hitbox))
                {
                    Game.Player.GetBoost(booster);
                    Game.SpawnedBoosters.Remove(booster);
                }
            }
        }

        private static void CheckEnemyBoosterIntersections(List<Enemy> spawnedEnemies, List<Booster> spawnedBoosters)
        {
            foreach (var booster in spawnedBoosters)
            {
                foreach (var enemy in spawnedEnemies)
                {
                    if (enemy.Hitbox.IntersectsWith(booster.Hitbox))
                    {
                        enemy.GetBoost(booster);
                        Game.SpawnedBoosters.Remove(booster);
                    }
                }
            }
        }
    }
}