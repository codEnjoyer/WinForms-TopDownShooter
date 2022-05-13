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
        internal static Player Player { get; set; }
        internal Size GameSize { get; }
        internal GameStage Stage { get; private set; } = GameStage.NotStarted;
        internal event Action<GameStage> StageChanged;
        internal static bool KeyPressed = false;

        internal Game()
        {
            GameSize = new Size(10000, 10000);
        }
        internal Game(Player player) : this()
        {
            Player = player;
        }

        private void ChangeStage(GameStage stage)
        {
            Stage = stage;
            StageChanged?.Invoke(stage);
        }

        //private bool InBounds(Vector location)
        //{
        //    return location.X >= 0 && location.X <= GameSize.Width &&
        //           location.Y >= 0 && location.Y <= GameSize.Height;
        //}
    }
}
