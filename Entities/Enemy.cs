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
        public int Damage { get; set; }

        protected Enemy(Vector location, Image image) : base(location, image)
        {
        }

        internal void Move()
        {
            var delta = new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle)) * Speed;
            var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

            Hitbox = new Rectangle(nextLocation, Hitbox.Size);
        }

        private void Accelerate(int speed)
        {
            Speed += speed;
        }

        internal float AngleToPlayer()
        {
            var x = (Game.Player.Hitbox.Location.X + Game.Player.Hitbox.Size.Width / 2f) - (Hitbox.Location.X + Hitbox.Size.Width / 2f);
            var y = (Game.Player.Hitbox.Location.Y + Game.Player.Hitbox.Size.Height / 2f) - (Hitbox.Location.Y + Hitbox.Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }

        public void GetBoost(Booster booster)
        {
            switch (booster.Type)
            {
                case BoosterTypes.HealthBoost:
                    Health += booster.Impact;
                    break;

                case BoosterTypes.DamageBoost:
                    Damage += booster.Impact;
                    break;

                case BoosterTypes.SpeedBoost:
                    Speed += booster.Impact;
                    break;
            }
        }

        public void DealDamage(Entity entity)
        {
            entity.Health -= Damage;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}
