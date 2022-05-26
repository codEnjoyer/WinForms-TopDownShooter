using System;
using GameProject.Domain.Guns.Bullets;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Domain.Weapons
{
    internal class Shotgun : Weapon
    {
        internal Shotgun()
        {
            Type = WeaponTypes.Shotgun;
            MaxAmmo = int.Parse(Resources.ShotgunAmmo);
            Ammo = MaxAmmo;
            Damage = 30;
        }
        internal override void Shoot(float angle)
        {
            var r = new Random();
            var playerCenter = Game.Player.GetHitboxCenter();

            for (var i = 0; i < 5; i++)
            {
                var spreadCoefficient = r.NextDouble() * (Math.PI / 2) - Math.PI / 4;

                var bulletLocation = new Vector(
                    playerCenter.X + Game.Player.Hitbox.Width / 2 * Math.Cos(angle + spreadCoefficient),
                    playerCenter.Y + Game.Player.Hitbox.Height / 2 * Math.Sin(angle + spreadCoefficient));

                var bullet = new ShotgunBullet(bulletLocation, angle);
                Game.SpawnedBullets.Add(bullet);
            }

            Recoil = MainForm.MainTimer.Interval * 50;
            Ammo--;
        }

        internal override void Reload()
        {
            Recoil = MainForm.MainTimer.Interval * 150;
            IsReloading = true;
        }
    }
}
