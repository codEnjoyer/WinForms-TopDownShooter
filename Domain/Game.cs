using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameProject.Entities;
using GameProject.Physics;
using GameProject.Views;

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
        internal Game(Player player, Rectangle gameZone) : this()
        {
            Player = player;
            GameZone = gameZone;

            CameraZone = new Rectangle(GameZone.Location.X + Screen.PrimaryScreen.WorkingArea.Width / 2 - Player.Size.Width / 2,
                GameZone.Location.Y + Screen.PrimaryScreen.WorkingArea.Height / 2 - Player.Size.Height / 2,
                GameZone.Width - Screen.PrimaryScreen.WorkingArea.Width,
                GameZone.Height - Screen.PrimaryScreen.WorkingArea.Height);

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

        internal static Vector SnapToCameraZone(Vector location)
        {
            var result = Vector.Zero;

            if (location.X < CameraZone.Left)
            {
                result.X = CameraZone.Left;
            }
            if (location.Y < CameraZone.Top)
            {
                result.Y = CameraZone.Top;
            }
            if (location.X > CameraZone.Right)
            {
                result.X = CameraZone.Right;
            }
            if (location.Y > CameraZone.Bottom)
            {
                result.Y = CameraZone.Bottom;
            }

            return result;
        }
    }
}
