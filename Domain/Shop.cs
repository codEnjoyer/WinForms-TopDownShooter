using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameProject.Domain.Weapons;
using GameProject.Properties;

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

        internal void InitButtons()
        {
            var exitButton = new Button
            {
                Location = new Point(Location.X + Size.Width - 70, Location.Y + 20),
                Size = new Size(50, 50),
                Text = Resources.Exit,
                Font = new Font(FontFamily.GenericSansSerif,  14),
                
            };
            exitButton.Click += (s,a) => Game.ChangeStage(GameStage.Battle);
            Form.Controls.Add(exitButton);
            controls.Add(exitButton);








            handgunButton = new Button
            {
                Location = new Point(Location.X + 70, Location.Y + 20),
                Size = new Size(150, 200),
                Font = new Font(FontFamily.GenericSansSerif, 14),
                TabStop = false
            };

            if (!Game.AvailableWeapons.Contains(WeaponTypes.Handgun))
            {
                handgunButton.Text = Resources.BuyHandgunFor + Resources.HandgunCost + Resources._Coins;
            }
            else
            {
                if (Game.Player.Weapon.Type != WeaponTypes.Handgun)
                {
                    handgunButton.Text = Resources.Equip;
                    handgunButton.Click += (s,a) => ChangeWeapon(handgunButton);
                }
                else MakeButtonEquipped(handgunButton);

            }

            DefineButtonClickEvent(handgunButton, int.Parse(Resources.HandgunCost), WeaponTypes.Handgun);
            Form.Controls.Add(handgunButton);
            controls.Add(handgunButton);



            rifleButton = new Button
            {
                Location = new Point(handgunButton.Right + 70, handgunButton.Top),
                Size = handgunButton.Size,
                Font = new Font(FontFamily.GenericSansSerif, 14),
                TabStop = false
            };

            if (!Game.AvailableWeapons.Contains(WeaponTypes.Rifle))
            {
                rifleButton.Text = Resources.BuyRifleFor + Resources.RifleCost + Resources._Coins;
            }
            else
            {
                if (Game.Player.Weapon.Type != WeaponTypes.Rifle)
                {
                    rifleButton.Text = Resources.Equip;
                    rifleButton.Click += (s, a) => ChangeWeapon(rifleButton);
                }
                else MakeButtonEquipped(rifleButton);

            }
            DefineButtonClickEvent(rifleButton, int.Parse(Resources.RifleCost), WeaponTypes.Rifle);
            Form.Controls.Add(rifleButton);
            controls.Add(rifleButton);



            shotgunButton = new Button
            {
                Location = new Point(rifleButton.Right + 70, rifleButton.Top),
                Size = rifleButton.Size,
                Font = new Font(FontFamily.GenericSansSerif, 14),
                TabStop = false
            };

            if (!Game.AvailableWeapons.Contains(WeaponTypes.Shotgun))
            {
                shotgunButton.Text = Resources.BuyShotgunFor + Resources.ShotgunCost + Resources._Coins;
            }
            else
            {
                if (Game.Player.Weapon.Type != WeaponTypes.Shotgun)
                {
                    shotgunButton.Text = Resources.Equip;
                    shotgunButton.Click += (s, a) => ChangeWeapon(shotgunButton);
                }
                else MakeButtonEquipped(shotgunButton);
            }

            DefineButtonClickEvent(shotgunButton, int.Parse(Resources.ShotgunCost), WeaponTypes.Shotgun);
            Form.Controls.Add(shotgunButton);
            controls.Add(shotgunButton);
        }

        private void BuyWeapon(Button button, WeaponTypes weaponType)
        {
            View.CoinsLabel.Text = Game.Coins.ToString();
            ChangeWeapon(button);
            EquipWeapon(weaponType);
        }
        private void ChangeWeapon(Button button)
        {
            foreach (var control in controls.Where(control => control.Text == Resources.Equipped))
            {
                control.Text = Resources.Equip;
                control.Enabled = true;
            }

            MakeButtonEquipped(button);
        }

        private void DefineButtonClickEvent(Button button, int cost, WeaponTypes weaponType)
        {
            button.Click += (s, a) =>
            {
                if (Game.Coins > cost && !Game.AvailableWeapons.Contains(weaponType))
                {
                    Game.Coins -= cost;
                    BuyWeapon(button, weaponType);
                }
                else if (Game.AvailableWeapons.Contains(weaponType))
                {
                    EquipWeapon(weaponType);
                    ChangeWeapon(button);
                }
            };
        }
        private void EquipWeapon(WeaponTypes weaponType)
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

        internal void Open()
        {
            InitButtons();
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
