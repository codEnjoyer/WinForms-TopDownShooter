using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Entities;
using GameProject.Interfaces;
using GameProject.Physics;
using Microsoft.Win32;

namespace GameProject.Domain
{
    internal class SpawnManager
    {
        private readonly Random random;
        private Timer enemySpawner;
        private Timer itemSpawner;
        private int spawnedItems;
        private int enemiesLimit;
        private int itemsLimit;

        internal SpawnManager()
        {
            enemiesLimit = 10;
            itemsLimit = 10;

            random = new Random();

            enemySpawner = new Timer
            {
                Interval = 5 * 1000
                
            };
            enemySpawner.Tick += (s,a) =>
                SpawnEnemy(Game.EnemiesSpecies[(int)Enemies.SmallEnemy], GetValidSpawnLocation());
            enemySpawner.Start();
        }

        private void SpawnEnemy(Enemy enemy, Vector location)
        {
            if (Game.SpawnedEnemies.Count >= enemiesLimit) return;

            enemy.Hitbox = new Rectangle(location.ToPoint(), enemy.Hitbox.Size);

            Form.ActiveForm.Controls.Add(enemy.PictureBox);
            enemy.PictureBox.BringToFront();
            Game.SpawnedEnemies.Add(enemy);
        }

        private Vector GetValidSpawnLocation()
        {
            var result = new Vector(Game.Player.Hitbox.Location);

            while (!InSpawnZone(result))
            {
                var randomLocationX = random.Next(Game.GameZone.X + Game.Player.Hitbox.Size.Width,
                    Game.GameZone.Right - Game.Player.Hitbox.Size.Width);

                var randomLocationY = random.Next(Game.GameZone.Y + Game.Player.Hitbox.Size.Height,
                    Game.GameZone.Bottom - Game.Player.Hitbox.Size.Height);

                result = new Vector(randomLocationX, randomLocationY);
            }

            return result;
        }
        private static bool InSpawnZone(Vector location)
        {
            return Game.InBounds(location) && !View.ViewedZone.Contains(location.ToPoint());
        }
    }
}
