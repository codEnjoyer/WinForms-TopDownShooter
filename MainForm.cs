using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Entities;
using GameProject.Physics;
using Timer = System.Windows.Forms.Timer;

namespace GameProject
{
    public partial class MainForm : Form
    {
        internal static Timer MainTimer;

        public MainForm()
        {
            InitializeComponent();

            InitGame();

            MainTimer = new Timer();
            MainTimer.Interval = 15;
            MainTimer.Tick += (sender, args) => Invalidate();
            MainTimer.Start();

            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            MouseMove += MainForm_MouseMove;
            MouseClick += MainForm_MouseClick;
        }

        private void InitGame()
        {

            var center = new Vector(Screen.PrimaryScreen.WorkingArea.Width / 2,
                Screen.PrimaryScreen.WorkingArea.Height / 2);

            var gameSize = new Size(3000, 3000);

            var gameZone = new Rectangle(Point.Empty, gameSize);

            var playerSpawnPoint = new Vector(center.X, center.Y);

            new Game(new Player(playerSpawnPoint), gameZone, this);
        }

        private static void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
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

        private static void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Controller.ControlMouse(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            View.UpdateTextures(e.Graphics, this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }
    }
}
