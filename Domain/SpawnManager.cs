using System;
using System.Windows.Forms;
using GameProject.Entities;
using GameProject.Entities.Enemies;
using GameProject.Physics;

namespace GameProject.Domain
{
    internal class SpawnManager
    {
        private readonly Random r;

        private static Timer enemySpawner;
        private static Timer boosterSpawner;

        private static int enemiesLimit;
        private static int boostersLimit;

        internal SpawnManager()
        {
            enemiesLimit = 3;
            boostersLimit = 7;

            r = new Random();

            enemySpawner = new Timer();
            enemySpawner.Interval = 1000;
            enemySpawner.Tick += (s,a) =>
                SpawnEnemy((EnemyTypes)r.Next(3), GetValidSpawnLocation());
            enemySpawner.Start();

            boosterSpawner = new Timer();
            boosterSpawner.Interval =  1000;
            boosterSpawner.Tick += (s, a) =>
                SpawnBooster((BoosterTypes)r.Next(3), GetValidSpawnLocation());
            boosterSpawner.Start();
        }

        internal void StopTimers()
        {
            enemySpawner.Stop();
            boosterSpawner.Stop();
        }
        internal void StartTimers()
        {
            enemySpawner.Start();
            boosterSpawner.Start();
        }
        private static void SpawnEnemy(EnemyTypes enemyType, Vector location)
        {
            if (!CanSpawnEnemy()) return;
            switch (enemyType)
            {
                case EnemyTypes.SmallEnemy:
                    var smallZombie = new SmallZombie(location);
                    Game.SpawnedEnemies.Add(smallZombie);
                    break;

                case EnemyTypes.MediumZombie:
                    var mediumZombie = new MediumZombie(location);
                    Game.SpawnedEnemies.Add(mediumZombie);
                    break;

                case EnemyTypes.HeavyZombie:
                    var heavyZombie = new HeavyZombie(location);
                    Game.SpawnedEnemies.Add(heavyZombie);
                    break;
            }
        }
        private static void SpawnBooster(BoosterTypes booster, Vector location)
        {
            if (!CanSpawnBooster()) return;
            switch (booster)
            {
                case BoosterTypes.HealthBoost:
                    var healthBoost = new HealthBooster(location);
                    Game.SpawnedBoosters.Add(healthBoost);
                    break;

                case BoosterTypes.DamageBoost:
                    var damageBoost = new DamageBooster(location);
                    Game.SpawnedBoosters.Add(damageBoost);
                    break;

                case BoosterTypes.SpeedBoost:
                    var speedBoost = new SpeedBooster(location);
                    Game.SpawnedBoosters.Add(speedBoost);
                    break;
            }
        }

        private Vector GetValidSpawnLocation()
        {
            var result = new Vector(Game.Player.Hitbox.Location);

            while (!InSpawnZone(result))
            {
                var randomLocationX = r.Next(Game.GameZone.X + Game.Player.Hitbox.Size.Width,
                    Game.GameZone.Right - Game.Player.Hitbox.Size.Width);

                var randomLocationY = r.Next(Game.GameZone.Y + Game.Player.Hitbox.Size.Height,
                    Game.GameZone.Bottom - Game.Player.Hitbox.Size.Height);

                result = new Vector(randomLocationX, randomLocationY);
            }

            return result;
        }
        private static bool InSpawnZone(Vector location)
        {
            return Game.InBounds(location) && !View.ViewedZone.Contains(location.ToPoint());
        }

        private static bool CanSpawnEnemy()
        {
            return Game.SpawnedEnemies.Count < enemiesLimit;
        }
        private static bool CanSpawnBooster()
        {
            return Game.SpawnedBoosters.Count < boostersLimit;
        }
    }
}
