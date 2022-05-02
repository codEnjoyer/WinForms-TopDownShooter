using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameProject.Entities
{
    internal class Player
    {
        internal Point Position { get; private set; }
        internal int Speed{ get; private set; }

        internal Player(Point position)//float speed)
        {
            this.Position = position;
            //this.Speed = speed;
        }

        internal Player()//float speed)
        {
            this.Position = new Point(100, 100);
            this.Speed = 10;
        }

        internal void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }


    }
}
