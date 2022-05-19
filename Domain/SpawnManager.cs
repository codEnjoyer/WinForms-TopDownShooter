using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Physics;
using Microsoft.Win32;

namespace GameProject.Domain
{
    internal class SpawnManager
    {
        private Random random;
        private Timer enemySpawner;
        private Timer itemSpawner;

        //internal SpawnManager()
        //{
        //    random = new Random();


        //    enemySpawner = new Timer
        //    {
        //        Interval = 5 * 1000,
        //        Tick += SpawnEnemy(Game.Enemies[0], GetValidSpawnLocation()) //Enemy ENUMs
        //    }
        //}

        //private void SpawnEnemy(IFightable enemy, Vector location)
        //{

        //}

        private Vector GetValidSpawnLocation()
        {
            var result = new Vector(Game.GameZone.Location.X - 100, Game.GameZone.Y - 100); //-100 in order not to be in the GameZone

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
        private bool InSpawnZone(Vector location)
        {
            return Game.InBounds(location) && !View.ViewedZone.Contains(location.ToPoint());
        }
    }
}
