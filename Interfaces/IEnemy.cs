using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Entities;

namespace GameProject.Interfaces
{
    internal interface IEnemy
    {
        void DealDamage(Entity entity);
        void TakeDamage();

    }
}
