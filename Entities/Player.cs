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
    
    internal class Player : Entity, IMovable/*, IFightable*/
    {
        public int Speed{ get; set; }
        public float RotationAngle { get; set; } //in radians
        public bool IsMovingUp { get; set; }
        public bool IsMovingLeft { get; set; }
        public bool IsMovingDown { get; set; }
        public bool IsMovingRight { get; set; }
        internal Rectangle ViewedZone { get; set; }

        internal Player(Vector location) : base(location, Resources.Hero)
        {
            Speed = 7;
            RotationAngle = 0;
            ViewedZone = new Rectangle(
                new Point(
                    Screen.PrimaryScreen.WorkingArea.Location.X - Hitbox.Size.Width * 2,
                    Screen.PrimaryScreen.WorkingArea.Location.Y - Hitbox.Size.Height * 2),
                new Size(
                    Screen.PrimaryScreen.WorkingArea.Width + 4 * Hitbox.Size.Width,
                    Screen.PrimaryScreen.WorkingArea.Height + 4 * Hitbox.Size.Height)); //Move to View.cs
        }

        public void Move()
        {
            if (IsMovingUp)
            {
                var delta = new Vector(0, -1) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsY(Hitbox))
                    View.Offset += delta;

                ViewedZone = new Rectangle(new Point(ViewedZone.X + nextLocation.X, ViewedZone.Y - nextLocation.Y), ViewedZone.Size);
            }

            if (IsMovingLeft)
            {
                var delta = new Vector(-1, 0) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsY(Hitbox))
                    View.Offset += delta;

                ViewedZone = new Rectangle(new Point(ViewedZone.X + nextLocation.X, ViewedZone.Y - nextLocation.Y), ViewedZone.Size);
            }

            if (IsMovingDown)
            {
                var delta = new Vector(0, 1) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsY(Hitbox))
                    View.Offset += delta;

                ViewedZone = new Rectangle(new Point(ViewedZone.X + nextLocation.X, ViewedZone.Y - nextLocation.Y), ViewedZone.Size);
            }

            if (IsMovingRight)
            {
                var delta = new Vector(1, 0) * Speed;
                var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Hitbox.Size))) return;

                Hitbox = new Rectangle(nextLocation, Hitbox.Size);

                if (Game.InCameraBoundsY(Hitbox))
                    View.Offset += delta;

                ViewedZone = new Rectangle(new Point(ViewedZone.X + nextLocation.X, ViewedZone.Y - nextLocation.Y), ViewedZone.Size);
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

        public void Accelerate(int speed)
        {
            Speed += speed;
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
            var y = (hitbox.Location.Y + hitbox.Size.Height / 2f)- (Hitbox.Location.Y + Hitbox.Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }
    }
}
