using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly Random r;
        private static Timer enemySpawner;
        private static Timer itemSpawner;
        private static int spawnedItems;
        private static int enemiesLimit;
        private static int itemsLimit;

        internal SpawnManager()
        {
            enemiesLimit = 1;
            itemsLimit = 10;

            r = new Random();
            enemySpawner = new Timer
            {
                Interval = 1000
                
            };

            enemySpawner.Tick += (s,a) =>
                SpawnEnemy((EnemyTypes)r.Next(1), GetValidSpawnLocation());
            enemySpawner.Start();
            
        }

        private void SpawnEnemy(EnemyTypes enemyType, Vector location)
        {
            if (!CanSpawnEnemy()) return;
            switch (enemyType)
            {
                case EnemyTypes.SmallEnemy:
                    var smallEnemy = new SmallEnemy(location);

                    smallEnemy.Hitbox = new Rectangle(location.ToPoint(), smallEnemy.Hitbox.Size);
                    smallEnemy.PictureBox.BringToFront();
                    Form.ActiveForm.Controls.Add(smallEnemy.PictureBox);
                    Game.SpawnedEnemies.Add(smallEnemy);
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
    }
}
