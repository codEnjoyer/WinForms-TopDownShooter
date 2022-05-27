using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Properties;

namespace GameProject.Domain
{
    internal class MainMenu : Control
    {
        private Form Form;
        private bool Initialized;
        private List<Control> controls;
        private Font buttonFont;
        private PictureBox background;
        private Button startGame;
        private Button tutorial;
        private Button tutorialExitButton;
        private Button exitGame;
        private Control tutorialWindow;
        internal MainMenu(Form form)
        {
            Form = form;
            controls = new List<Control>();
            buttonFont = new Font(FontFamily.GenericSansSerif, 16);
            Location = Point.Empty;
            Size = Form.ClientSize;
        }

        private void UpdateButtons()
        {
            var centerX = Form.ClientSize.Width / 2;
            var centerY = Form.ClientSize.Height / 2;

            tutorial = new Button
            {
                Size = new Size(300, 70),
                Location = new Point(centerX - 300 / 2, centerY - 70 / 2),
                Text = Resources.Tutorial,
                Font = buttonFont,
                TabStop = false
            };
            tutorial.Click += (s,a) =>
            {
                tutorial.Enabled = false;
                ShowTutorialWindow();
            };
            Form.Controls.Add(tutorial);
            controls.Add(tutorial);

            startGame = new Button
            {
                Size = tutorial.Size,
                Location = new Point(tutorial.Left, tutorial.Top - tutorial.Size.Height - 50),
                Text = Resources.StartGame,
                Font = buttonFont,
                TabStop = false
            };

            startGame.Click += (s, a) =>
            {
                Game.ChangeStage(GameStage.Battle);
            };
            Form.Controls.Add(startGame);
            controls.Add(startGame);

            exitGame = new Button
            {
                Size = tutorial.Size,
                Location = new Point(tutorial.Left, tutorial.Bottom + 50),
                Text = Resources.ExitGame,
                Font = buttonFont,
                TabStop = false
            };
            exitGame.Click += (s, a) => Application.Exit();
            Form.Controls.Add(exitGame);
            controls.Add(exitGame);
        }

        private void ShowTutorialWindow()
        {
            tutorialWindow = new Control
            {
                Location = new Point(100, 100),
                Size = new Size(200, 200),
            };
            Form.Controls.Add(tutorialWindow);
            controls.Add(tutorialWindow);

            tutorialExitButton = new Button
            {
                Location = new Point(tutorialWindow.Location.X + tutorialWindow.Size.Width - 150 - 20, tutorialWindow.Location.Y + 20),
                Size = new Size(150, 50),
                Text = Resources.Exit,
                TabStop = false,
                Font = buttonFont
            };
            tutorialExitButton.Click += (s, a) => CloseTutorialWindow();
            Form.Controls.Add(tutorialExitButton);
            controls.Add(tutorialExitButton);
        }

        private void CloseTutorialWindow()
        {
            tutorial.Enabled = false;
            Form.Controls.Remove(tutorialWindow);
            controls.Remove(tutorialWindow);
            Form.Controls.Remove(tutorialExitButton);
            controls.Remove(tutorialExitButton);
        }
        internal void Open()
        {
            UpdateButtons();

            if (Initialized) return;
            //background
            Initialized = true;
        }

        internal void Close()
        {
            foreach (var control in controls)
            {
                Form.Controls.Remove(control);
            }
        }
    }
}
