using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GameProject.Domain.Weapons;
using GameProject.Entities;

namespace GameProject.Domain
{
    internal class KillManager
    {
        private static Timer killTimer;

        internal KillManager()
        {
            killTimer = new Timer();
            killTimer.Interval = MainForm.MainTimer.Interval * 5;
            killTimer.Tick += (sender, args) => CheckConfrontations();
            killTimer.Start();
        }

        private static void CheckConfrontations()
        {
            if(Game.SpawnedEnemies.Count == 0) return;
            var spawnedEnemies = new List<Enemy>(Game.SpawnedEnemies);
            CheckEnemiesHits(spawnedEnemies);

            if(Game.SpawnedBullets.Count == 0) return;
            var spawnedBullets = new List<Bullet>(Game.SpawnedBullets);
            CheckBulletsHits(spawnedBullets, spawnedEnemies);
        }

        private static void CheckEnemiesHits(List<Enemy> spawnedEnemies)
        {
            foreach (var enemy in spawnedEnemies.Where(enemy => enemy.Hitbox.IntersectsWith(Game.Player.Hitbox)))
            {
                enemy.DealDamage(Game.Player);

                if (Math.Abs(Game.Player.Health - Game.Player.MinHealth) < 10)
                    Game.ChangeStage(GameStage.Finished);
            }
            
        }
        private static void CheckBulletsHits(List<Bullet> spawnedBullets, List<Enemy> spawnedEnemies)
        {
            foreach (var enemy in spawnedEnemies)
            {
                foreach (var bullet in spawnedBullets.Where(bullet => bullet.Hitbox.IntersectsWith(enemy.Hitbox)))
                {
                    enemy.TakeDamage(Game.Player.Weapon.Damage * 1000);
                    Game.SpawnedBullets.Remove(bullet);

                    if (Math.Abs(enemy.Health - enemy.MinHealth) < 10)
                    {
                        Game.Coins += enemy.Score;
                        View.CoinsLabel.Text = Game.Coins.ToString();
                        Game.SpawnedEnemies.Remove(enemy);
                    }

                    if (bullet.Hitbox.IntersectsWith(View.ViewedZone)) continue;
                    Game.SpawnedBullets.Remove(bullet);
                }
            }
        }
        internal void StopTimer() => killTimer.Stop();
        internal void StartTimer() => killTimer.Start();
    }
}
