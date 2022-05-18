using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Interfaces;
using GameProject.Physics;

namespace GameProject.Entities
{
    abstract class Enemy : Entity, IFightable
    {
        public float RotationAngle { get; set; }
        public int Speed { get; set; }

        protected Enemy(Vector location, Image image) : base(location, image)
        {
            RotationAngle = Game.Player.AngleToTarget(Hitbox);
        }

        public void Move()
        {
            RotationAngle = Game.Player.AngleToTarget(Hitbox);

            var delta = new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle)) * Speed;
            var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

            Hitbox = new Rectangle(nextLocation, Hitbox.Size);
        }

        public void Accelerate(int speed)
        {
            Speed += speed;
        }
    }
}
