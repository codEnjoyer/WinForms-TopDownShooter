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
        private Form Form;
        private List<Control> controls;
        internal Shop(Form form)
        {
            Form = form;
            controls = new List<Control>();
            Location = new Point(Form.Left + Form.Width / 5, Form.Top + 30);
            Size = new Size(Form.Width * 3 / 5, Form.Height - 2 * 30);
            BackColor = Color.Wheat;
        }

        private void ShowButtons()
        {
            var exitButton = new Button
            {
                Location = new Point(Location.X + Size.Width - 70, Location.Y + 20),
                Size = new Size(50, 50),
                Text = "Exit",
            };
            exitButton.Click += (s,a) => Game.ChangeStage(GameStage.Battle);
            Form.Controls.Add(exitButton);
            controls.Add(exitButton);
        }

        internal void Open()
        {
            ShowButtons();
        }

        internal void Close()
        {
            foreach (var control in controls)
            {
                Form.Controls.Remove(control);
            }
        }
    }
}
