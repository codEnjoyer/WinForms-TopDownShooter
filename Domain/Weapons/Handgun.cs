using System.Data.Common;

namespace GameProject.Domain.Weapons
{
    internal class Handgun : Weapon
    {
        internal Handgun()
        {
            Ammo = 7;
            Damage = 20;
        }
    }
}
