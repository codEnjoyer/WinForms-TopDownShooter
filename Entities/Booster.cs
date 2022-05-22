using GameProject.Physics;
using System.Drawing;

namespace GameProject.Entities
{
    abstract class Booster : Entity
    {
        internal BoosterTypes Type { get; set; }
        internal int Impact { get; set; }

        protected Booster(Vector location, Image image) : base(location, image)
        {
            PictureBox.Image = Image;
        }
    }
}
