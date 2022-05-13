using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GameProject.Physics;

namespace GameProject.Entities
{
    
    internal class Player
    {
        internal Vector Location { get; private set; }
        internal int Speed{ get; private set; }
        internal float RotationAngle { get; set; } //in radians
        internal bool Forward, Left, Back, Right;
        internal Size Size { get; }
        internal Image Image { get; set; }
        internal PictureBox PictureBox { get; set; }

        internal Player()
        {
            Location = new Vector(100, 100);
            Speed = 5;
            Size = new Size(289, 224);
            RotationAngle = 0;
            Image = Image.FromFile(@"C:\Учёба\Прога\GameProject\Sprites\survivor-idle_knife_0.png");
            PictureBox = new PictureBox
            {
                //Anchor = AnchorStyles.None,
                Location = Location.ToPoint(),
                Size = Size,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            
        }
        internal Player(Vector location) : this()
        {
            Location = new Vector(location.X - Image.Width / 2, location.Y - Image.Height / 2);
        }

        internal void Move()
        {
            if (Forward)
                Location += Speed * new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle));
            else if (Left)
                Location += Speed * new Vector(Math.Cos(RotationAngle - Math.PI / 2), Math.Sin(RotationAngle - Math.PI / 2));
            else if (Back)
                Location -= Speed * new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle));
            else if (Right)
                Location -= Speed * new Vector(Math.Cos(RotationAngle - Math.PI / 2), Math.Sin(RotationAngle - Math.PI / 2));
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
