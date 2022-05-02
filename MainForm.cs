using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Entities;
using System.Windows.Forms.VisualStyles;

namespace GameProject
{
    public partial class MainForm : Form
    {
        private Game game;
        private Timer timer;

        public MainForm()
        {
            InitializeComponent();

            game = new Game(new Player());
            timer = new Timer();

            timer.Interval = 15;
            timer.Tick += (sender, args) => Invalidate();
            timer.Start();

            KeyDown += OnKeyPressed;
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
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        internal void OnKeyPressed(object sender, KeyEventArgs e)
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

                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.FillEllipse(Brushes.ForestGreen, new Rectangle(game.Player.Position, new Size(50, 50)));
        }

    }
}
