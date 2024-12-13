using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype_1._1
{
    public partial class Form2 : Form
    {
        private enum GameState
        {
            Setup,
            PlayerMove,
            PlayerShoot,
            EnemyMove,
            EnemyShoot

        }

        private List<PictureBox> players = new List<PictureBox>();
        private GameState currentState = GameState.Setup;
        private bool isPlayerSelected = false;
        private PictureBox selectedPlayer = null;
        private Button SetupendBtn;
        private Button TurnendBtn;
        private int piecesToPlace = 4;

        public Form2(Form1 parent)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Click += Form_Click;
            SetupEndButton();
            EndTurnButton();
        }

        int MX = 10;
        int MY = 10;
        int squaresize = 50;
        int highwidth = 10;
        Rectangle[] boxes;

        private void SetupEndButton()
        {
            SetupendBtn = new Button();
            SetupendBtn.Text = "End Setup";
            SetupendBtn.Size = new Size(100, 30);
            SetupendBtn.Location = new Point(highwidth * squaresize + 20, 50);
            SetupendBtn.Click += SetupEndBtn_Click;
            SetupendBtn.Enabled = false;
            this.Controls.Add(SetupendBtn);
        }

        private void EndTurnButton()
        {
            TurnendBtn = new Button();
            TurnendBtn.Text = "End Turn";
            TurnendBtn.Size = new Size(100, 30);
            TurnendBtn.Location = new Point(highwidth * squaresize + 20, 90);
            TurnendBtn.Click += EndTurnBtn_Click;
            TurnendBtn.Enabled = true;
            this.Controls.Add(TurnendBtn);
        }

        private void SetupEndBtn_Click(object sender, EventArgs e)
        {
            currentState = GameState.PlayerMove;
            SetupendBtn.Text = "Game Active";
            SetupendBtn.Enabled = false;
        }

        private void EndTurnBtn_Click(object sender, EventArgs e)
        {
            currentState = GameState.EnemyMove;
            TurnendBtn.Enabled = false;
        }

        private void Player(int x, int y)
        {
            if (piecesToPlace > 0)
            {
                PictureBox player = new PictureBox();
                player.Size = new System.Drawing.Size(squaresize - 2, squaresize - 2);
                player.SizeMode = PictureBoxSizeMode.StretchImage;
                player.Location = new System.Drawing.Point(x, y);
                player.Image = Properties.Resources.Space_marine_2;
                player.Click += PlayerImage_Click;
                players.Add(player);
                this.Controls.Add(player);
                piecesToPlace--;

                if (piecesToPlace == 0)
                {
                    SetupendBtn.Enabled = true;
                }
            }
        }

        private void PlayerImage_Click(object sender, EventArgs e)
        {
            if (currentState != GameState.PlayerMove) return;

            PictureBox clickedPlayer = (PictureBox)sender;

            if (selectedPlayer == clickedPlayer)
            {
                isPlayerSelected = false;
                selectedPlayer = null;
            }
            else
            {
                isPlayerSelected = true;
                selectedPlayer = clickedPlayer;
            }

            this.Invalidate();
        }

        private void Form_Click(object sender, EventArgs e)
        {
            if (currentState == GameState.Setup)
            {
                if ((MY >= (highwidth - 2) * squaresize) && MX < 500)
                {
                    Player(MX, MY);
                }
                else
                {
                    MessageBox.Show("You can only place players in the last two rows.", "Placement Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (currentState == GameState.PlayerMove && isPlayerSelected && selectedPlayer != null && MX < 500)
            {
                selectedPlayer.Location = new Point(MX, MY);
                isPlayerSelected = false;
                selectedPlayer = null;
                this.Invalidate();
            }

        }

        private void GridLines(object sender, PaintEventArgs e)
        {
            int totalBoxes = highwidth * highwidth;
            boxes = new Rectangle[totalBoxes];
            int index = 0;

            for (int i = 0; i < highwidth + 1; i++)
            {
                e.Graphics.DrawLine(Pens.Red, i * squaresize, 0, i * squaresize, squaresize * highwidth);
                e.Graphics.DrawLine(Pens.Red, 0, i * squaresize, squaresize * highwidth, i * squaresize);
            }

            foreach (var player in players)
            {
                Rectangle playerRect = new Rectangle(
                    (player.Location.X - 1) / squaresize * squaresize,
                    (player.Location.Y - 1) / squaresize * squaresize,
                    squaresize,
                    squaresize
                );

                if (currentState == GameState.PlayerMove && player == selectedPlayer)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), playerRect);
                    if (MX < 500)
                    {
                        e.Graphics.DrawRectangle(Pens.Green, MX, MY, squaresize, squaresize);
                    }
                }
            }

            if (currentState == GameState.Setup)
            {
                if (MX < 500)
                {

                    e.Graphics.DrawRectangle(Pens.Green, MX, MY, squaresize, squaresize);
                }
            }

            for (int i = 0; i < highwidth; i++)
            {
                for (int j = 0; j < highwidth; j++)
                {
                    if (index < totalBoxes)
                    {
                        Rectangle rect = new Rectangle(j * squaresize, i * squaresize, squaresize, squaresize);
                        boxes[index++] = rect;
                    }
                }
            }
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            MX = 1 + Convert.ToInt32(e.X / squaresize) * squaresize;
            MY = 1 + Convert.ToInt32(e.Y / squaresize) * squaresize;
            this.Invalidate();
        }
    }
}