using System;
using System.Drawing;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Entities;
using GameProject.Physics;
using Timer = System.Windows.Forms.Timer;

namespace GameProject
{
    public partial class MainForm : Form
    {
        internal static Timer timer;
        private Label testLabel;
        internal static Label Scores { get; set; }

        public MainForm()
        {
            InitializeComponent();

            InitGame();

            timer = new Timer
            {
                Interval = 15
            };
            timer.Tick += (sender, args) => Invalidate();
            timer.Tick += (sender, args) => CheckIntersections();
            //timer.Tick += (sender, args) => CheckGame();
            timer.Start();

            Cursor = Cursors.Cross;
            
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            MouseMove += MainForm_MouseMove;
            MouseClick += MainForm_MouseClick;

            //testLabel = new Label
            //{
            //    Location = new Point(50, 50),
            //    Size = new Size(90, 70),
            //};
            //Controls.Add(testLabel);

            Scores = new Label()
            {
                Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - 200, 50),
                Size = new Size(200, 20),
                Font = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold)
            };
            Controls.Add(Scores);

            MouseMove += (s, e) =>
            {
                //testLabel.Text = "Camera offset: " + View.Offset + "\nPlayer location: " + Game.Player.Hitbox.Location;
                Scores.Text = "Score:" + Game.Scores;
            };
            KeyDown += (s, e) =>
            {
                //testLabel.Text = "Camera offset: " + View.Offset + "\nPlayer location: " + Game.Player.Hitbox.Location;
            };
        }

        private static void InitGame()
        {
            var center = new Vector(Screen.PrimaryScreen.WorkingArea.Width / 2,
                Screen.PrimaryScreen.WorkingArea.Height / 2);

            var gameSize = new Size(3000, 3000);

            var gameZone = new Rectangle(Point.Empty, gameSize);

            var playerSpawnPoint = new Vector(center.X, center.Y);

            new Game(new Player(playerSpawnPoint), gameZone);
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
            var eGraphics = e.Graphics;
            View.UpdateTextures(eGraphics);
        }

        private static void CheckIntersections()
        {
            Game.CheckBoostersIntersections();
        }

        private static void CheckGameFinished()
        {

        }
        #region Fullscreen, FormLoad
        private void MainForm_Load(object sender, EventArgs e)
        {
            GoFullscreen(true);
            //GameZone = Screen.PrimaryScreen.WorkingArea.GameZone;
            DoubleBuffered = true;
        }


        //TODO : Move to View.cs
        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                //TopMost = true;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                Bounds = Screen.PrimaryScreen.Bounds;
            }
        }
        #endregion
    }
}
