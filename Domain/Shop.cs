using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GameProject.Domain.Weapons;

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
                Font = new Font(FontFamily.GenericSansSerif,  14),
                
            };
            exitButton.Click += (s,a) => Game.ChangeStage(GameStage.Battle);
            Form.Controls.Add(exitButton);
            controls.Add(exitButton);

            var handgunButton = new Button
            {
                Location = new Point(Location.X + 70, Location.Y + 20),
                Size = new Size(100, 70),
                Text = "Купить пистолет",
                Font = new Font(FontFamily.GenericSansSerif, 14),

            };

            if (Game.Player.Weapon.Type == WeaponTypes.Handgun)
            {
                handgunButton.Text = "Экипировано";
                handgunButton.Enabled = false;
            }
            handgunButton.Click += (s, a) =>
            {
                BuyWeapon(WeaponTypes.Handgun);
                handgunButton.Text = "Куплено";
                handgunButton.Enabled = false;
            };
            Form.Controls.Add(handgunButton);
            controls.Add(handgunButton);

            var rifleButton = new Button
            {
                Location = new Point(handgunButton.Right + 70, handgunButton.Top),
                Size = handgunButton.Size,
                Text = "Купить автомат",
                Font = new Font(FontFamily.GenericSansSerif, 14),

            };

            rifleButton.Click += (s, a) =>
            {
                BuyWeapon(WeaponTypes.Rifle);
                rifleButton.Text = "Куплено";
                rifleButton.Enabled = false;
            };
            Form.Controls.Add(rifleButton);
            controls.Add(rifleButton);

            var shotgunButton = new Button
            {
                Location = new Point(rifleButton.Right + 70, rifleButton.Top),
                Size = rifleButton.Size,
                Text = "Купить дробовик",
                Font = new Font(FontFamily.GenericSansSerif, 14),

            };

            shotgunButton.Click += (s, a) =>
            {
                BuyWeapon(WeaponTypes.Shotgun);
                shotgunButton.Text = "Куплено";
                shotgunButton.Enabled = false;
            };
            Form.Controls.Add(shotgunButton);
            controls.Add(shotgunButton);

            switch (Game.Player.Weapon.Type)
            {
                case WeaponTypes.Handgun:
                    Equip(handgunButton);
                    break;
                case WeaponTypes.Rifle:
                    Equip(rifleButton);
                    break;
                case WeaponTypes.Shotgun:
                    Equip(shotgunButton);
                    break;
            }
        }

        private void BuyWeapon(WeaponTypes weaponType)
        {
            switch (weaponType)
            {
                case WeaponTypes.Handgun:
                    Game.Player.Weapon = new Handgun();
                    break;
                case WeaponTypes.Rifle:
                    Game.Player.Weapon = new Rifle();
                    break;
                case WeaponTypes.Shotgun:
                    Game.Player.Weapon = new Shotgun();
                    break;
            }
            Game.UpdateAvailableWeapons();
        }

        private void Equip(Button button)
        {
            button.Text = "Экипировано";
            button.Enabled = false;
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
