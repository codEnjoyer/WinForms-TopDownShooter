using GameProject.Entities;

namespace GameProject.Interfaces
{
    internal interface IFightable
    {
        int Damage { get; set; }
        void DealDamage(Entity entity);
        void TakeDamage(int damage);
        void GetBoost(Booster booster);
        void GetSpeedBoost(int impact);
        void GetDamageBoost(int impact);
        void GetHealthBoost(double impact);
    }
}
