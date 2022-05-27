using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject.Extensions
{
    internal class WeaponButton : Button
    {
        internal WeaponTypes Type { get; set; }

        internal WeaponButton(WeaponTypes type)
        {
            Type = type;
        }
    }
}
