using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    
    internal class Player
    {
        //internal bool InBounds;
        internal Vector Location { get; private set; }
        internal int Speed{ get; private set; }
        internal float RotationAngle { get; set; } //in radians
        internal bool Forward, Left, Back, Right; //TODO: replace with Enums?
        internal Size Size { get; }
        internal Image Image { get; set; }
        internal PictureBox PictureBox { get; set; }

        internal Player()
        {
            Location = new Vector(100, 100);
            Speed = 7;
            Size = new Size(289, 289);
            RotationAngle = 0;
            Image = Resources.Hero;
            PictureBox = new PictureBox
            {
                Location = Location.ToPoint(),
                Size = Size,
            };

        }
        internal Player(Vector location) : this()
        {
            Location = new Vector(location.X - PictureBox.Size.Width / 2, location.Y - PictureBox.Size.Height / 2);
        }

        internal void Move()
        {

            if (Forward)
            {
                var delta = new Vector(0, -1) * Speed;
                var nextLocation = new Point((int)(Location.X + delta.X), (int)(Location.Y + delta.Y));
                if (Game.InBounds(new Rectangle(nextLocation, Size)))
                    Location += delta;
                //if (Location.Y >= 100)
                //    View.Offset += delta;
            }

            if (Left)
            {
                var delta = new Vector(-1, 0) * Speed;
                var nextLocation = new Point((int)(Location.X + delta.X), (int)(Location.Y + delta.Y));
                if (Game.InBounds(new Rectangle(nextLocation, Size)))
                    Location += delta;
                //if (Location.X <= 1000)
                //    View.Offset += delta;
            }

            if (Back)
            {
                var delta = new Vector(0, 1) * Speed;
                var nextLocation = new Point((int)(Location.X + delta.X), (int)(Location.Y + delta.Y));
                if (Game.InBounds(new Rectangle(nextLocation, Size)))
                    Location += delta;
                //if (Location.Y <= 1000)
                //    View.Offset += delta;
            }

            if (Right)
            {
                var delta = new Vector(1, 0) * Speed;
                var nextLocation = new Point((int) (Location.X + delta.X), (int) (Location.Y + delta.Y));
                if (Game.InBounds(new Rectangle(nextLocation, Size)))
                    Location += delta;
                //if(Location.X >= 100)
                //    View.Offset += delta;
            }



            //Movement relative to cursor:
            //if (Forward)
            //    Location += Speed * new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle));
            //if (Left)
            //    Location += Speed * new Vector(Math.Cos(RotationAngle - Math.PI / 2), Math.Sin(RotationAngle - Math.PI / 2));
            //if (Back)
            //    Location -= Speed * new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle));
            //if (Right)
            //    Location -= Speed * new Vector(Math.Cos(RotationAngle - Math.PI / 2), Math.Sin(RotationAngle - Math.PI / 2));
        }

        internal void GetMouseRotation(float angle)
        {
            RotationAngle = angle;
        }

        internal void Accelerate(int speed)
        {
            Speed += speed;
        }

        internal float AngleToTarget(Vector targetLocation)
        {
            //TODO: consider the size of the target

            var x = targetLocation.X - (Location.X + Size.Width / 2f);
            var y = targetLocation.Y - (Location.Y + Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }
    }
}
