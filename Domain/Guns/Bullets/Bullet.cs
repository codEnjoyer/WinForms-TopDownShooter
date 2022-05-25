using System.Drawing;
using System.Windows.Forms;
using GameProject.Entities;
using GameProject.Physics;

namespace GameProject.Domain.Weapons
{
    internal abstract class Bullet : Entity
    {
        internal int Speed { get; set; }
        internal float Angle { get; set; }

        internal Bullet(Vector location, Image image, float angle) : base(location, image)
        {
            Angle = angle;
        }
    }
}
