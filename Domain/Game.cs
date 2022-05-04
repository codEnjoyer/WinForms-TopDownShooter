using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameProject.Entities;
using GameProject.Physics;

namespace GameProject.Domain
{
    internal class Game
    {
        internal Player Player{ get; }
        internal GameStage Stage { get; private set; } = GameStage.NotStarted;
        internal event Action<GameStage> StageChanged;
        internal Game(Player player)
        {
            Player = player;
        }

        //private void Game_MouseMove(object sender, MouseEventArgs e)
        //{
        //    var RotationAngle = 0;
            
        //    var PositionOfMouse = Cursor.Location;
        //    var direction = new Vector(e.X - pictureBox.Location.X, e.Y - pictureBox.Location.Y);
        //}

        private void ChangeStage(GameStage stage)
        {
            Stage = stage;
            StageChanged?.Invoke(stage);
        }
    }
}
