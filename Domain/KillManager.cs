using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GameProject.Entities;

namespace GameProject.Domain
{
    internal class KillManager
    {
        private static Timer killTimer;

        internal KillManager()
        {
            killTimer = new Timer();
            killTimer.Interval = 150;
            killTimer.Tick += (sender, args) => CheckConfrontations();
            killTimer.Start();
        }

        private static void CheckConfrontations()
        {
            var spawnedEnemies = new List<Enemy>(Game.SpawnedEnemies);

            foreach (var enemy in spawnedEnemies.Where(enemy => Game.Player.Hitbox.IntersectsWith(enemy.Hitbox)))
            {
                Game.Player.DealDamage(enemy);

                if (enemy.Health == enemy.MinHealth)
                    Game.SpawnedEnemies.Remove(enemy);

                enemy.DealDamage(Game.Player);

                if(Game.Player.Health == Game.Player.MinHealth)
                    Game.ChangeStage(GameStage.Finished);
            }
        }
    }
}
