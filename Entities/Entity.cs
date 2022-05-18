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
        protected Entity(Vector location, Image image)
        {
            Image = image;
            var size = new Size(Math.Max(Image.Width, Image.Height), Math.Max(Image.Width, Image.Height));
            Hitbox = new Rectangle(location.ToPoint(), size);

            PictureBox = new PictureBox
            {
                Location = Hitbox.Location,
                Size = Hitbox.Size,
            };

            Hitbox = new Rectangle(new Vector(location.X - Hitbox.Size.Width / 2, location.Y - Hitbox.Size.Height / 2).ToPoint(), size);
        }
    }
}
