using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
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

        internal Player(Vector location) : base(location, Resources.HeroNormal)
        {
            Hitbox = new Rectangle(new Vector(location.X - Hitbox.Size.Width / 2, location.Y - Hitbox.Size.Height / 2).ToPoint(), Hitbox.Size);
            
            Speed = 5;
            MaxSpeed = 2 * Speed;

            Damage = 10;
            MaxDamage = 2 * Damage;//TODO: Make weapons

            MaxHealth = 100;
            Health = 75;

            HealthBar = new Rectangle(Hitbox.Location.X + (int) (0.25 * Hitbox.Width), Hitbox.Location.Y - 10,
                (int) (0.5 * Hitbox.Width), 10);
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

            

            //Movement relative to cursor:
            //if (IsMovingUp)
            //    Location += Speed * new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle));
            //if (IsMovingLeft)
            //    Location += Speed * new Vector(Math.Cos(RotationAngle - Math.PI / 2), Math.Sin(RotationAngle - Math.PI / 2));
            //if (IsMovingDown)
            //    Location -= Speed * new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle));
            //if (IsMovingRight)
            //    Location -= Speed * new Vector(Math.Cos(RotationAngle - Math.PI / 2), Math.Sin(RotationAngle - Math.PI / 2));
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

        public bool GetBoost(Booster booster)
        {
            switch (booster.Type)
            {
                case BoosterTypes.HealthBoost:
                    return GetHealth(booster.Impact);

                case BoosterTypes.DamageBoost:
                    return GetDamage(booster.Impact);

                case BoosterTypes.SpeedBoost:
                    return GetSpeed(booster.Impact);

                default: return false;
            }
        }
        public bool GetHealth(int impact)
        {
            if (Health == MaxHealth) return false;

            if (Health + impact > MaxHealth)
            {
                Health = MaxHealth;
                return true;
            }
            Health += impact;
            return true;
        }

        public bool GetDamage(int impact)
        {
            if (Damage == MaxDamage) return false;

            if (Damage + impact > MaxDamage)
            {
                Damage = MaxDamage;
                return true;
            }
            Damage += impact;
            return true;
        }
        public bool GetSpeed(int impact)
        {
            if (Speed == MaxSpeed) return false;

            if (Speed + impact > MaxSpeed)
            {
                Speed = MaxSpeed;
                return true;
            }
            Speed += impact;
            return true;
        }

        public bool GetSlowdown(int impact)
        {
            if (Speed == MinSpeed) return false;

            if (Speed - impact < MinSpeed)
            {
                Speed = MinSpeed;
                return true;
            }
            Speed -= impact;
            return true;
        }

        public void DealDamage(Entity entity)
        {
            ((Enemy)entity).TakeDamage(Damage);
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
