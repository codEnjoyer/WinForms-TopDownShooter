using GameProject.Properties;

namespace GameProject.Domain.Weapons
{
    internal class RifleGun : Weapon
    {
        internal RifleGun()
        {
            Ammo = int.Parse(Resources.RiflegunAmmo);
            Damage = 15;
        }
    }

}
