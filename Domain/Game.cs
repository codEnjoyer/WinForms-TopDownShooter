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
        internal static Rectangle GameZone { get; private set; }
        internal static Rectangle CameraZone { get; private set; }
        internal GameStage Stage { get; private set; } = GameStage.NotStarted;
        internal event Action<GameStage> StageChanged;
        internal static bool KeyPressed = false;

        internal Game()
        {
            
        }
        internal Game(Player player) : this()
        {
            Player = player;
            GameZone = new Rectangle(new Point(0, 0),
                new Size(3860 - (int)(1.3 * Player.Size.Width),2140 - (int)(0.7 * Player.Size.Height)));

            CameraZone = new Rectangle(new Point(Player.Size.Width, Player.Size.Height),
                new Size(GameZone.Width - 2 * Player.Size.Width, GameZone.Height - 2 * Player.Size.Height));
        }

        private void ChangeStage(GameStage stage)
        {
            Stage = stage;
            StageChanged?.Invoke(stage);
        }
        
        internal static bool InBounds(Rectangle hitbox)
        {
            return GameZone.Contains(hitbox);
        }

        internal static bool InCameraBounds(Vector location)
        {
            return location.X > CameraZone.Left && location.X < CameraZone.Right &&
                   location.Y > CameraZone.Top && location.Y < CameraZone.Bottom;
        }
    }
}
