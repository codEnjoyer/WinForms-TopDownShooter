using GameProject.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Interfaces;

namespace GameProject.Entities
{
    abstract class Booster : Entity, ITakeable
    {
        protected Booster(Vector location, Image image) : base(location, image)
        {
            PictureBox.Image = Image;
        }
    }
}
