using System;
using System.Windows.Forms;
using GameProject.Domain.Guns.Bullets;
using GameProject.Physics;

namespace GameProject.Domain.Weapons
{
    internal class Handgun : Weapon
    {
        internal Handgun()
        {
            Ammo = 7;
            Damage = 20;
        }

        internal override void Shoot(float angle)
        {
            var playerCenter = Game.Player.GetHitboxCenter();
            var bulletLocation = new Vector(
                playerCenter.X + Game.Player.Hitbox.Width / 2 * Math.Cos(angle),
                playerCenter.Y + Game.Player.Hitbox.Height / 2 * Math.Sin(angle));
            var bullet = new HandgunBullet(bulletLocation, angle);
            Game.SpawnedBullets.Add(bullet);
            Recoil = MainForm.MainTimer.Interval * 20;
        }
    }
}
