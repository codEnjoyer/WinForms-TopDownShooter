using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject.Entities
{
    internal class Player
    {
        internal Vector Position { get; private set; }
        //internal float Speed{ get; private set; }

        internal Player(Vector position)//float speed)
        {
            this.Position = position;
            //this.Speed = speed;
        }

        internal Player()//float speed)
        {
            this.Position = new Vector(100, 100);
            //this.Speed = speed;
        }

        internal void Move(float dx, float dy)
        {
            Position = new Vector(Position.X + dx, Position.Y + dy);
        }


    }
}
