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
        private Size sizeOfPlayer;
        private Image imageOfPlayer = Image.FromFile(@"C:\Учёба\Прога\GameProject\Sprites\survivor-idle_knife_0.png");
        public MainForm()
        {
            InitializeComponent();

            var x = Screen.PrimaryScreen.WorkingArea.Width / 2;
            var y = Screen.PrimaryScreen.WorkingArea.Height / 2;
            sizeOfPlayer = new Size(100, 50);
            game = new Game(new Player(new Point(x - sizeOfPlayer.Width / 2, y - sizeOfPlayer.Height / 2)));
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

        internal void OnMouseMove(object sender, MouseEventArgs e)
        {

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
                //case Keys.R:
                //    Invalidate();
                //    break;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var player = new Rectangle(game.Player.Position, sizeOfPlayer);

            graphics.FillEllipse(Brushes.ForestGreen, player);
        }
    }
}
