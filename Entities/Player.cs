using System.Collections.Generic;
using System.Drawing;
using GameProject.Domain;
using GameProject.Interfaces;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    
    internal class Player : Entity, IMovable, IFightable
    {
        public int Speed{ get; set; }
        public float RotationAngle { get; set; } //in radians
        public bool IsMovingUp { get; set; }
        public bool IsMovingLeft { get; set; }
        public bool IsMovingDown { get; set; }
        public bool IsMovingRight { get; set; }
        public int Damage { get; set; }
        public Dictionary<BoosterTypes, int> ActiveBoosters { get; set; }

        internal Player(Vector location) : base(location, Resources.HeroNormal)
        {
            Hitbox = new Rectangle(new Vector(location.X - Hitbox.Size.Width / 2, location.Y - Hitbox.Size.Height / 2).ToPoint(), Hitbox.Size);
            
            Speed = 5;

            Damage = 10; //TODO: Make weapons

            MaxHealth = 100 * 1000;
            Health = MaxHealth;

            HealthBar = new Rectangle(Hitbox.Location.X + (int) (0.25 * Hitbox.Width), Hitbox.Location.Y - 10,
                (int) (0.5 * Hitbox.Width), 10);

            ActiveBoosters = new Dictionary<BoosterTypes, int>
            {
                [BoosterTypes.HealthBoost] = 0,
                [BoosterTypes.DamageBoost] = 0,
                [BoosterTypes.SpeedBoost] = 0
            };

        }

        public void Move()
        {
            if (IsMovingLeft)
            {
                var delta = new Vector(-1, 0) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsX(Hitbox))
                    View.Offset += delta;
            }

            if (IsMovingRight)
            {
                var delta = new Vector(1, 0) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsX(Hitbox))
                    View.Offset += delta;
            }

            if (IsMovingUp)
            {
                var delta = new Vector(0, -1) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsY(Hitbox))
                    View.Offset += delta;
            }

            if (IsMovingDown)
            {
                var delta = new Vector(0, 1) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsY(Hitbox))
                    View.Offset += delta;
            }
        }

        internal float AngleToTarget(Vector targetLocation)
        {
            var x = targetLocation.X - (Hitbox.Location.X + Hitbox.Size.Width / 2f);
            var y = targetLocation.Y - (Hitbox.Location.Y + Hitbox.Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }

        internal float AngleToTarget(Rectangle hitbox)
        {
            var x = (hitbox.Location.X + hitbox.Size.Width / 2f) - (Hitbox.Location.X + Hitbox.Size.Width / 2f);
            var y = (hitbox.Location.Y + hitbox.Size.Height / 2f) - (Hitbox.Location.Y + Hitbox.Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }

        public void GetBoost(Booster booster)
        {
            ActiveBoosters[booster.Type] = booster.Time;
        }
        public void GetHealthBoost(double impact)
        {
            if (Health == MaxHealth)
            {
                ActiveBoosters[BoosterTypes.HealthBoost] = 0;
                return;
            }
            
            if (Health + impact > MaxHealth)
            {
                Health = MaxHealth;
                return;
            }

            Health += impact;
        }

        public void GetDamageBoost(int impact)
        {
            Damage += impact;
        }
        public void GetSpeedBoost(int impact)
        {
            Speed += impact;
        }

        public void DealDamage(Entity entity)
        {
            ((Enemy)entity).TakeDamage(Damage * 1000);
        }

        public void TakeDamage(int damage)
        {
            if (Health - damage < MinHealth)
            {
                Health = MinHealth;
                return;
            }
            Health -= damage;
        }
    }
}
