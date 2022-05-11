using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Entities;
using GameProject.Physics;
using Timer = System.Windows.Forms.Timer;

namespace GameProject
{
    public partial class MainForm : Form
    {
        private Game game;
        private Timer timer;
        public MainForm()
        {
            InitializeComponent();

            var x = Screen.PrimaryScreen.WorkingArea.Width / 2;
            var y = Screen.PrimaryScreen.WorkingArea.Height / 2;

            game = new Game(new Player(new Vector(x, y)));
            timer = new Timer();

            timer.Interval = 15;
            timer.Tick += (sender, args) => Invalidate();
            timer.Start();

            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            MouseMove += MainForm_MouseMove;
        }


        private static void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Game.KeyPressed = true;
            Controller.ControlKeys(e.KeyCode, true);
        }

        private static void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Controller.ControlKeys(e.KeyCode, false);
        }
        private static void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Controller.ControlMouse(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            
            graphics.DrawImage(Game.Player.Image, Game.Player.Location.ToPoint());
            View.UpdateTextures(graphics);
        }

        //private void Rotate(Graphics graphics, float angle)
        //{
        //    var rotated = new Bitmap(playerSize.Width, playerSize.Height);
        //    using (Graphics fromImage = Graphics.FromImage(rotated))
        //    {
        //        fromImage.TranslateTransform(playerSize.Width / 2f, playerSize.Height / 2f);
        //        fromImage.RotateTransform(angle);
        //        fromImage.TranslateTransform(-(playerSize.Width / 2f), -(playerSize.Height / 2f));
        //        fromImage.DrawImage(playerImage, 0, 0, playerSize.Width, playerSize.Height);
        //    }
        //    graphics.DrawImage(rotated,
        //        Game.Player.Location.ToPoint().X, Game.Player.Location.ToPoint().Y,
        //        playerSize.Width, playerSize.Height);
        //}

        internal float GetAngleFromTargetToPlayer(Vector playerLocation, Vector targetLocation)
        {
            return targetLocation.AngleToPlayer(Game.Player);
        }

        internal void MouseControl(object sender, MouseEventArgs e)
        {

        }
        #region Fullscreen, FormLoad
        private void MainForm_Load(object sender, EventArgs e)
        {
            GoFullscreen(true);
            DoubleBuffered = true;
        }

        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.None;
                Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                FormBorderStyle = FormBorderStyle.Sizable;
            }
        }
        #endregion
    }
}
