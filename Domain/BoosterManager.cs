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

        internal void StopTimer()
        {
            boosterTimer.Stop();
        }
        internal void StartTimer()
        {
            boosterTimer.Start();
        }
        private static void CheckBoostersRemainingTime()
        {
            var activeBoosters = new Dictionary<BoosterTypes, int>(Game.Player.ActiveBoosters);
            CheckPlayerBoostersRemainingTime(activeBoosters);

            if (Game.SpawnedEnemies.Count == 0) return;
            var spawnedEnemies = new List<Enemy>(Game.SpawnedEnemies);
            CheckEnemiesBoostersRemainingTime(spawnedEnemies);
        }

        private static void CheckPlayerBoostersRemainingTime(Dictionary<BoosterTypes, int> activeBoosters)
        {
            foreach (var boosterTypeRemainingTime in activeBoosters
                         .Where(boosterTypeRemainingTime => boosterTypeRemainingTime.Value > 0))
            {
                switch (boosterTypeRemainingTime.Key)
                {
                    case BoosterTypes.HealthBoost:
                        Game.Player.GetHealthBoost((double)(10 * 1000 * 2)/ boosterTimer.Interval);
                        break;

                    case BoosterTypes.DamageBoost:
                        if (boosterTypeRemainingTime.Value == 10 * 1000 && Game.Player.BonusDamage == 0) //Link to existing boosters impact?
                            Game.Player.GetDamageBoost(15); //boosters.cs impacts
                        break;

                    case BoosterTypes.SpeedBoost:
                        if (boosterTypeRemainingTime.Value == 10 * 1000 && Game.Player.BonusSpeed == 0)
                            Game.Player.GetSpeedBoost(5);
                        break;
                }

                Game.Player.ActiveBoosters[boosterTypeRemainingTime.Key] = boosterTypeRemainingTime.Value - boosterTimer.Interval;

                if (Game.Player.ActiveBoosters[boosterTypeRemainingTime.Key] == 0)
                {
                    switch (boosterTypeRemainingTime.Key)
                    {
                        case BoosterTypes.DamageBoost:
                            Game.Player.Damage -= Game.Player.BonusDamage;
                            Game.Player.BonusDamage = 0;
                            break;

                        case BoosterTypes.SpeedBoost:
                            Game.Player.Speed -= Game.Player.BonusSpeed;
                            Game.Player.BonusSpeed = 0;
                            break;
                    }
                }
            }
        }

        private static void CheckEnemiesBoostersRemainingTime(List<Enemy> spawnedEnemies)
        {
            foreach (var enemy in spawnedEnemies)
            {
                var activeBoosters = new Dictionary<BoosterTypes, int>(enemy.ActiveBoosters);
                foreach (var boosterTypeRemainingTime in activeBoosters)
                {
                    if (boosterTypeRemainingTime.Value <= 0) continue;

                    switch (boosterTypeRemainingTime.Key)
                    {
                        case BoosterTypes.HealthBoost:
                            enemy.GetHealthBoost((double)(10 * 1000) / boosterTimer.Interval);
                            break;

                        case BoosterTypes.DamageBoost:
                            if (boosterTypeRemainingTime.Value == 10 * 1000 && enemy.BonusDamage == 0) //Link to existing boosters impact? +bonus damage field
                                enemy.GetDamageBoost(15); //boosters.cs impacts
                            break;

                        case BoosterTypes.SpeedBoost:
                            if (boosterTypeRemainingTime.Value == 10 * 1000 && enemy.BonusSpeed == 0)
                                enemy.GetSpeedBoost(5);
                            break;
                    }

                    enemy.ActiveBoosters[boosterTypeRemainingTime.Key] = boosterTypeRemainingTime.Value - boosterTimer.Interval;

                    if (enemy.ActiveBoosters[boosterTypeRemainingTime.Key] == 0)
                    {
                        switch (boosterTypeRemainingTime.Key)
                        {
                            case BoosterTypes.DamageBoost:
                                enemy.Damage -= enemy.BonusDamage;
                                enemy.BonusDamage = 0;
                                break;
                            case BoosterTypes.SpeedBoost:
                                enemy.Speed -= enemy.BonusSpeed;
                                enemy.BonusSpeed = 0;
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