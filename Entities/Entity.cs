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
        public Vector Location { get; set; }
        
        public Size Size { get; set; }

        protected Entity(Vector location, Image image)
        {
            Image = image;
            Location = location;
            Size = new Size(Math.Max(Image.Width, Image.Height), Math.Max(Image.Width, Image.Height));
            
        }
    }
}
