using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Interfaces;
using GameProject.Physics;

namespace GameProject.Entities
{
    abstract class Entity
    {
        public Image Image { get; set; }
        public Rectangle Hitbox { get; set; }
        public PictureBox PictureBox { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public Rectangle HealthBar { get; set; }

        protected Entity(Vector location, Image image)
        {
            Image = image;
            var size = new Size(Math.Max(Image.Width, Image.Height), Math.Max(Image.Width, Image.Height));
            Hitbox = new Rectangle(location.ToPoint(), size);
            PictureBox = new PictureBox
            {
                Location = Hitbox.Location,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = Hitbox.Size,
            };
        }

        internal float GetHPPercent()
        {
            return (float)(Health / MaxHealth);
        }
    }
}
