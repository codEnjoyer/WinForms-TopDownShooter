using GameProject.Entities;

namespace GameProject.Interfaces
{
    internal interface IFightable
    {
        int Damage { get; set; }
        void DealDamage(Entity entity);
        void TakeDamage(int damage);
        bool GetBoost(Booster booster);
        bool GetSpeed(int impact);
        bool GetDamage(int impact);
        bool GetHealth(int impact);
        bool GetSlowdown(int impact);
    }
}
