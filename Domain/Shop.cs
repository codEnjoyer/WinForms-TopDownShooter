using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Properties;

namespace GameProject.Domain
{
    internal class Shop : Control
    {
        internal Shop(Form form)
        {
            Location = new Point(form.Left + form.Width / 5, form.Top + 30);
            Size = new Size(form.Width * 3 / 5, form.Height - 2 * 30);
            BackColor = Color.Wheat;

        }
    }
}
