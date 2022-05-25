namespace GameProject.Domain.Weapons
{
    internal abstract class Weapon
    {
        internal bool IsAttacking { get; set; }
        internal bool IsReloading { get; set; }
        internal int Recoil { get; set; }
        internal int Ammo { get; set; }
        internal int Damage { get; set; }

        internal Weapon()
        {

        }

        internal void Shoot()
        {
            Ammo--;

        }
    }
}
