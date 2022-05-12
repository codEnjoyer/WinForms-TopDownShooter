using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using GameProject.Physics;

namespace GameProject.Entities
{
    internal class Player
    {
        internal Vector Location { get; private set; }
        internal int Speed{ get; private set; }
        internal float RotationAngle { get; set; }
        internal bool Up, Left, Down, Right;
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
            if (Up)
                Location = new Vector(Location.X, Location.Y - Speed);
            else if (Left)
                Location = new Vector(Location.X - Speed, Location.Y);
            else if (Down)
                Location = new Vector(Location.X, Location.Y + Speed);
            else if (Right)
                Location = new Vector(Location.X + Speed, Location.Y);
        }

        internal void GetMouseRotation()
        {
            PictureBox.Image?.Dispose();
            var bitmap = new Bitmap(Image, PictureBox.Size);

            PictureBox.Image = View.RotateImageMatrix(bitmap, RotationAngle);
        }

        internal void Accelerate(int speed)
        {
            Speed += speed;
        }

        internal float AngleToTarget(Vector targetLocation)
        {
            //TO DO: consider the size of the target
            var x = targetLocation.X - (Location.X + Size.Width) / 2f;
            var y = targetLocation.Y - (Location.Y + Size.Height) / 2f;
            return new Vector(x, y).AngleInDegrees;
            //return new Vector(x, y).AngleInRadians;
        }
    }
}
