using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Physics;

namespace GameProject.Views
{
    internal class Camera
    {
        internal Vector Offset { get; set; }
        internal Vector Location { get; set; }

        internal Camera()
        {
            Offset = Vector.Zero;
            
        }
        internal Camera(Vector location)
        {
            Location = location;
        }
    }
}
