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
        //internal static bool KeyPressed = false;

        internal Game()
        {
            
        }
        internal Game(Player player, Rectangle gameZone) : this()
        {
            Player = player;
            GameZone = gameZone;

            var cameraZoneLocation = new Point(
                GameZone.Location.X + (Screen.PrimaryScreen.WorkingArea.Width - Player.Size.Width) / 2,
                GameZone.Location.Y + (Screen.PrimaryScreen.WorkingArea.Height - Player.Size.Height) / 2);

            var cameraZoneSize = new Size(
                GameZone.Width - Screen.PrimaryScreen.WorkingArea.Width,
                GameZone.Height - Screen.PrimaryScreen.WorkingArea.Height);

            CameraZone = new Rectangle(cameraZoneLocation, cameraZoneSize);

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

        internal static bool InCameraBoundsX(Vector location)
        {
            return location.X > CameraZone.Left && location.X < CameraZone.Right;

        }

        internal static bool InCameraBoundsY(Vector location)
        {
            return location.Y > CameraZone.Top && location.Y < CameraZone.Bottom;
        }
    }
}
