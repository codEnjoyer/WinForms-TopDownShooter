using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Interfaces;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    
    internal class Player : Entity, IMovable
    {
        public int Speed{ get; set; }
        public float RotationAngle { get; set; } //in radians
        public bool IsMovingUp { get; set; }
        public bool IsMovingLeft { get; set; }
        public bool IsMovingDown { get; set; }
        public bool IsMovingRight { get; set; }
        internal PictureBox PictureBox { get; set; }

        internal Player(Vector location) : base(location, Resources.Hero)
        {
            Speed = 7;
            RotationAngle = 0;
            PictureBox = new PictureBox
            {
                Location = Location.ToPoint(),
                Size = Size,
            };
            Location = new Vector(location.X - PictureBox.Size.Width / 2, location.Y - PictureBox.Size.Height / 2);
        }

        public void Move()
        {
            if (IsMovingUp)
            {
                var delta = new Vector(0, -1) * Speed;
                var nextLocation = new Point((int)(Location.X + delta.X), (int)(Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Size))) return;
                Location += delta;

                if (Game.InCameraBoundsY(Location))
                    View.Offset += delta;
            }

            if (IsMovingLeft)
            {
                var delta = new Vector(-1, 0) * Speed;
                var nextLocation = new Point((int)(Location.X + delta.X), (int)(Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Size))) return;
                Location += delta;

                if (Game.InCameraBoundsX(Location))
                    View.Offset += delta;
            }

            if (IsMovingDown)
            {
                var delta = new Vector(0, 1) * Speed;
                var nextLocation = new Point((int)(Location.X + delta.X), (int)(Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Size))) return;
                Location += delta;

                if (Game.InCameraBoundsY(Location))
                    View.Offset += delta;
            }

            if (IsMovingRight)
            {
                var delta = new Vector(1, 0) * Speed;
                var nextLocation = new Point((int) (Location.X + delta.X), (int) (Location.Y + delta.Y));

                if (!Game.InBounds(new Rectangle(nextLocation, Size))) return;
                Location += delta;

                if (Game.InCameraBoundsX(Location))
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

        internal void GetMouseRotation(float angle)
        {
            RotationAngle = angle;
        }

        public void Accelerate(int speed)
        {
            Speed += speed;
        }

        internal float AngleToTarget(Vector targetLocation)
        {
            var x = targetLocation.X - (Location.X + Size.Width / 2f);
            var y = targetLocation.Y - (Location.Y + Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }

        internal float AngleToTarget(Vector targetLocation, Size targetSize)
        {
            var x = (targetLocation.X + targetSize.Width / 2f) - (Location.X + Size.Width / 2f);
            var y = (targetLocation.Y + targetSize.Height / 2f)- (Location.Y + Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }
    }
}
