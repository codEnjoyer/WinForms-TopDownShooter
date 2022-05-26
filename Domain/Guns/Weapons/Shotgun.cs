using GameProject.Properties;

namespace GameProject.Domain.Weapons
{
    internal class Shotgun : Weapon
    {
        internal Shotgun()
        {
            Ammo = int.Parse(Resources.ShotgunAmmo);
            Damage = 30;
        }
    }
}
