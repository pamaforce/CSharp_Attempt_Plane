using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plane
{
    public partial class MainForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                //WS_EX_COMPOSITED
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private Game game;
        public MainForm()
        {
            InitializeComponent();
        }

        private void gameArea_Load(object sender, EventArgs e)
        {
            game = new Game(this);
            gameArea.parent = this;
        }

        private void gameArea_Paint(object sender, PaintEventArgs e)
        {
            if (game != null)
            {
                game.Draw(e.Graphics);
            }
        }

        private string comboKeys = string.Empty;

        private void gameTick_Tick(object sender, EventArgs e)
        {
            if (comboKeys != string.Empty)
            {
                game.KeyDown(comboKeys);
            }
            game.DetectCollision();
            this.gameArea.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (comboKeys.Contains(e.KeyCode.ToString()) == false)
            {
                comboKeys += e.KeyCode.ToString();
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            comboKeys = comboKeys.Replace(e.KeyCode.ToString(), string.Empty);
        }
        
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            comboKeys = string.Empty;
        }

        public void StartGame()
        {
            game.StartGame();
        }
        public void HideMenu()
        {
            gameArea.Controls["titleLabel"].Visible = false;
            gameArea.Controls["startBtn"].Visible = false;
            gameArea.Controls["aboutBtn"].Visible = false;
            gameArea.Controls["scoreLabel"].Visible = true;
        }
        public void ShowMenu()
        {
            gameArea.Controls["titleLabel"].Visible = true;
            gameArea.Controls["startBtn"].Visible = true;
            gameArea.Controls["aboutBtn"].Visible = true;
            gameArea.Controls["scoreLabel"].Visible = false;
        }
        public void updateScore(int score)
        {
            gameArea.Controls["scoreLabel"].Text= score.ToString();
        }
    }
}
