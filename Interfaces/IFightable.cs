using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Entities;

namespace GameProject.Interfaces
{
    internal interface IFightable
    {
        //int Health { get; set; }
        //void DealDamage(Player entity);
        //void TakeDamage();
        void GetBoost(Booster booster);
    }
}
