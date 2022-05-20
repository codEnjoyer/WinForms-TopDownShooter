using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GameProject.Physics;

namespace GameProject.Interfaces
{
    interface IMovable
    {
        bool IsMovingUp { get; set; }
        bool IsMovingLeft { get; set; }
        bool IsMovingDown { get; set; }
        bool IsMovingRight { get; set; }
        float RotationAngle { get; set; }
        int Speed { get; set; }
        void Move();
    }
}
