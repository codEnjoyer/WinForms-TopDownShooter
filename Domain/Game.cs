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
        internal static Size GameSize { get; private set; }
        internal GameStage Stage { get; private set; } = GameStage.NotStarted;
        internal event Action<GameStage> StageChanged;
        internal static bool KeyPressed = false;

        internal Game()
        {
            
        }
        internal Game(Player player) : this()
        {
            Player = player;
            GameSize = new Size(1920 - (int)(1.3 * Player.Size.Width), 1080 - (int)(0.7 * Player.Size.Height));
        }

        private void ChangeStage(GameStage stage)
        {
            Stage = stage;
            StageChanged?.Invoke(stage);
        }
        
        internal static bool InBounds(Rectangle hitbox)
        {
            return new Rectangle(new Point(0, 0), GameSize).Contains(hitbox);
        }
    }
}
