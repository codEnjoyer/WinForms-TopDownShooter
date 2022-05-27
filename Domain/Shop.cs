using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameProject.Domain.Weapons;

namespace GameProject.Domain
{
    internal class Shop : Control
    {
        private Form Form;
        private List<Control> controls;
        private Button handgunButton { get; set; }
        private Button rifleButton { get; set; }
        private Button shotgunButton { get; set; }

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








            handgunButton = new Button
            {
                Location = new Point(Location.X + 70, Location.Y + 20),
                Size = new Size(100, 70),
                Font = new Font(FontFamily.GenericSansSerif, 14),
            };

            if (!Game.AvailableWeapons.Contains(WeaponTypes.Handgun))
            {
                handgunButton.Text = "Купить пистолет";
                handgunButton.Click += (s, a) =>
                {
                    BuyWeapon(WeaponTypes.Handgun);
                    MakeButtonBought(handgunButton);
                };
            }
            else
            {
                handgunButton.Text = "Экипировать";
            }
            handgunButton.Click += (s, a) => UpdateButtons();
            Form.Controls.Add(handgunButton);
            controls.Add(handgunButton);



            rifleButton = new Button
            {
                Location = new Point(handgunButton.Right + 70, handgunButton.Top),
                Size = handgunButton.Size,
                Font = new Font(FontFamily.GenericSansSerif, 14),

            };

            if (!Game.AvailableWeapons.Contains(WeaponTypes.Rifle))
            {
                rifleButton.Text = "Купить автомат";
                rifleButton.Click += (s, a) =>
                {
                    BuyWeapon(WeaponTypes.Rifle);
                    MakeButtonBought(rifleButton);
                };
            }
            else
            {
                rifleButton.Text = "Экипировать";
            }
            rifleButton.Click += (s, a) => UpdateButtons();
            Form.Controls.Add(rifleButton);
            controls.Add(rifleButton);



            shotgunButton = new Button
            {
                Location = new Point(rifleButton.Right + 70, rifleButton.Top),
                Size = rifleButton.Size,
                Font = new Font(FontFamily.GenericSansSerif, 14),
            };

            if (!Game.AvailableWeapons.Contains(WeaponTypes.Shotgun))
            {
                shotgunButton.Text = "Купить дробовик";
                shotgunButton.Click += (s, a) =>
                {
                    BuyWeapon(WeaponTypes.Shotgun);
                    MakeButtonBought(shotgunButton);
                };
            }
            else
            {
                shotgunButton.Text = "Экипировать";
            }
            shotgunButton.Click += (s, a) => UpdateButtons();
            Form.Controls.Add(shotgunButton);
            controls.Add(shotgunButton);

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            switch (Game.Player.Weapon.Type)
            {
                case WeaponTypes.Handgun:
                    MakeButtonEquipped(handgunButton);
                    break;
                case WeaponTypes.Rifle:
                    MakeButtonEquipped(rifleButton);
                    break;
                case WeaponTypes.Shotgun:
                    MakeButtonEquipped(shotgunButton);
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

        
        private void MakeButtonEquipped(Button button)
        {
            button.Text = "Экипировано";
            button.Enabled = false;
        }
        private void MakeButtonBought(Button button)
        {
            button.Text = "Куплено";
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
