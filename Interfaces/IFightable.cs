using GameProject.Entities;

namespace GameProject.Interfaces
{
    internal interface IFightable
    {
        int Damage { get; set; }
        void DealDamage(Entity entity);
        void TakeDamage(int damage);
        void GetBoost(Booster booster);
        void GetSpeed(int impact);
        void GetDamage(int impact);
        void GetHealth(int impact);
        void GetSlowdown(int impact);
    }
}
