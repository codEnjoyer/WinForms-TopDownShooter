using System;
using System.Collections.Generic;
using System.Drawing;
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
            game = new Game(new Player(new Point(x - 50, y - 25)));
            timer = new Timer();

            timer.Interval = 15;
            timer.Tick += (sender, args) => Invalidate();
            timer.Start();

            KeyDown += OnControlKeyPressed;
            KeyDown += OnSystemKeyPressed;
        }

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

        internal void OnControlKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                    game.Player.Move(0, -game.Player.Speed);
                    break;
                case Keys.A:
                    game.Player.Move(-game.Player.Speed, 0);
                    break;
                case Keys.S:
                    game.Player.Move(0, game.Player.Speed);
                    break;
                case Keys.D:
                    game.Player.Move(game.Player.Speed, 0);
                    break;
            }
        }

        internal void OnSystemKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var rect = new Rectangle(game.Player.Position, new Size(100, 50));
            
            graphics.FillRectangle(Brushes.Blue, rect);

            RotateGraphics(graphics, 45);

            graphics.FillRectangle(Brushes.ForestGreen, rect);
        }

        private void RotateGraphics(Graphics graphics,  float angle)
        {
            graphics.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-ClientSize.Width / 2, -ClientSize.Height / 2);
        }
    }
}
