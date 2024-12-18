using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Warhammer
{
    public partial class Form2 : Form
    {
        // Enum for different states of the game
        private enum GameState
        {
            Setup,
            PlayerMove,
            PlayerShoot,
            EnemyMove,
            EnemyShoot
        }

        // player pieces on the board
        private List<PictureBox> players = new List<PictureBox>();
        private GameState currentState = GameState.Setup;
        private bool isPlayerSelected = false;
        private PictureBox selectedPlayer = null;

        private Button SetupendBtn;
        private Button TurnendBtn;
        private int piecesToPlace = 4;

        // Dictionary to track the movement of player pieces
        private Dictionary<PictureBox, int> playerMoves = new Dictionary<PictureBox, int>();

        // Set of players that have moved during the current turn
        private HashSet<PictureBox> playersMovedThisTurn = new HashSet<PictureBox>();

        // number showing the maximum movements
        private const int MaxMovementPlayer = 5;
        private const int MaxMovementNecrons = 6;

        // Random number for generating positions for enemies start
        private Random random = new Random();

        // Collections for mountains and necrons
        private List<PictureBox> mountains = new List<PictureBox>();
        private List<PictureBox> necrons = new List<PictureBox>();

        // Set of Necrons that have moved during the current turn
        private HashSet<PictureBox> necronsMovedThisTurn = new HashSet<PictureBox>();

        // Dictionary to track Necron movements
        private Dictionary<PictureBox, int> necronMoves = new Dictionary<PictureBox, int>();

        public Form2(Form1 parent)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Click += Form_Click;

            // Initialize setup and turn buttons
            SetupEndButton();
            EndTurnButton();

            // Place initial mountains and Necrons on the board
            PlaceMountains(3);
            PlaceNecrons(5); // maximum 15 or game crashes
        }

        // Variables for grid and UI layout
        int MX = 10;
        int MY = 10;
        int squaresize = 50;
        int highwidth = 10;
        Rectangle[] boxes;

        // Creates and configures the "End Setup" button
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

        // Creates and configures the "End Turn" button
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

        //  "End Setup" button click
        private void SetupEndBtn_Click(object sender, EventArgs e)
        {
            currentState = GameState.PlayerMove; // Transition to PlayerMove state
            SetupendBtn.Text = "Game Active";
            SetupendBtn.Enabled = false;
        }

        // "End Turn" button click
        private void EndTurnBtn_Click(object sender, EventArgs e)
        {
            // Reset player moves for the next turn
            foreach (var player in players)
            {
                playerMoves[player] = 0;
            }
            playersMovedThisTurn.Clear();

            // Switch to enemy move phase
            currentState = GameState.EnemyMove;
            MoveNecrons();

            // goes back to PlayerMove phase
            TurnendBtn.Enabled = true;
            currentState = GameState.PlayerMove;
        }

        // Adds a new player piece to the board during setup
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

                // Add the player piece to the list and dictionary
                players.Add(player);
                playerMoves[player] = 0;

                this.Controls.Add(player);
                piecesToPlace--;

                // Enable the "End Setup" button once all pieces are placed
                if (piecesToPlace == 0)
                {
                    SetupendBtn.Enabled = true;
                }
            }
        }

        // player piece selection during PlayerMove phase
        private void PlayerImage_Click(object sender, EventArgs e)
        {
            if (currentState != GameState.PlayerMove) return; // Only respond during PlayerMove phase

            PictureBox clickedPlayer = (PictureBox)sender;

            if (selectedPlayer == clickedPlayer)
            {
                // Deselect the player if already selected
                isPlayerSelected = false;
                selectedPlayer = null;
            }
            else
            {
                // Select a new player piece
                isPlayerSelected = true;
                selectedPlayer = clickedPlayer;
            }

            this.Invalidate(); // Refresh the form to show selection
        }

        // click events on the form during setup or player movement
        private void Form_Click(object sender, EventArgs e)
        {
            if (currentState == GameState.Setup)
            {
                // Allow placing players only in the last two rows
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
                // Calculate the movement distance and check restrictions
                int dx = Math.Abs(selectedPlayer.Location.X - MX) / squaresize;
                int dy = Math.Abs(selectedPlayer.Location.Y - MY) / squaresize;
                int distance = dx + dy;

                if (playersMovedThisTurn.Contains(selectedPlayer))
                {
                    MessageBox.Show("This unit has already moved this turn", "Move Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (playerMoves[selectedPlayer] + distance > MaxMovementPlayer)
                {
                    MessageBox.Show($"This unit cannot move more than {MaxMovementPlayer} squares!", "Move Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update the player's position and track movement
                selectedPlayer.Location = new Point(MX, MY);
                playerMoves[selectedPlayer] += distance;
                playersMovedThisTurn.Add(selectedPlayer);

                // Deselect the player piece after moving
                isPlayerSelected = false;
                selectedPlayer = null;

                this.Invalidate(); // Refresh the form
            }
        }

        // Draws the grid and highlights things od relevance
        private void GridLines(object sender, PaintEventArgs e)
        {
            int totalBoxes = highwidth * highwidth;
            boxes = new Rectangle[totalBoxes];
            int index = 0;

            // Draw grid lines
            for (int i = 0; i < highwidth + 1; i++)
            {
                e.Graphics.DrawLine(Pens.Red, i * squaresize, 0, i * squaresize, squaresize * highwidth);
                e.Graphics.DrawLine(Pens.Red, 0, i * squaresize, squaresize * highwidth, i * squaresize);
            }

            // Highlight selected player and potential move position
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

            // Highlight placement area during setup
            if (currentState == GameState.Setup)
            {
                if (MX < 500)
                {
                    e.Graphics.DrawRectangle(Pens.Green, MX, MY, squaresize, squaresize);
                }
            }

            // Stores the grid
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

        // Updates mouse position on the grid
        private void MouseMoved(object sender, MouseEventArgs e)
        {
            MX = 1 + Convert.ToInt32(e.X / squaresize) * squaresize;
            MY = 1 + Convert.ToInt32(e.Y / squaresize) * squaresize;
            this.Invalidate(); // Refresh the form to update UI
        }

        // Places a specified number of mountains on the board
        private void PlaceMountains(int count)
        {
            HashSet<Point> usedPositions = new HashSet<Point>();

            for (int i = 0; i < count; i++)
            {
                Point mountainLocation;

                do
                {
                    // Generate random position for the mountain
                    int randomX = random.Next(0, highwidth) * squaresize + 1;
                    int randomY = random.Next(0, highwidth) * squaresize + 1;
                    mountainLocation = new Point(randomX, randomY);
                } while (usedPositions.Contains(mountainLocation)); // Makes sure there are no duplicates'

                usedPositions.Add(mountainLocation);

                PictureBox mountain = new PictureBox
                {
                    Size = new Size(squaresize - 2, squaresize - 2),
                    Location = mountainLocation,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Properties.Resources.mountain_pixel
                };

                mountains.Add(mountain);
                this.Controls.Add(mountain);
            }
        }

        // Places a specified number of Necrons on the board
        private void PlaceNecrons(int count)
        {
            HashSet<Point> usedPositions = new HashSet<Point>();

            // Makes sure necrons dont spawn in occupied spots
            foreach (var mountain in mountains)
                usedPositions.Add(mountain.Location);
            foreach (var player in players)
                usedPositions.Add(player.Location);

            for (int i = 0; i < count; i++)
            {
                Point necronLocation;

                do
                {
                    // Generate random position for the Necrons to move to
                    int randomX = random.Next(0, highwidth) * squaresize + 1;
                    int randomY = random.Next(0, 2) * squaresize + 1;
                    necronLocation = new Point(randomX, randomY);
                } while (usedPositions.Contains(necronLocation)); // Makes sure there are no duplicates'

                usedPositions.Add(necronLocation);

                PictureBox necron = new PictureBox
                {
                    Size = new Size(squaresize - 2, squaresize - 2),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = necronLocation,
                    Image = Properties.Resources.necron
                };

                // Add click event for Necron
                necron.Click += NecronImage_Click;

                necrons.Add(necron);
                this.Controls.Add(necron);

                necronMoves[necron] = MaxMovementNecrons;
            }
        }

        // Does Necron selection during EnemyMove phase
        private void NecronImage_Click(object sender, EventArgs e)
        {
            if (currentState == GameState.EnemyMove)
            {
                PictureBox clickedNecron = (PictureBox)sender;

                if (selectedPlayer == clickedNecron)
                {
                    isPlayerSelected = false;
                    selectedPlayer = null;
                }
                else
                {
                    isPlayerSelected = true;
                    selectedPlayer = clickedNecron;
                }

                this.Invalidate(); // Refresh the form
            }
        }

        // Moves Necrons randomly during the EnemyMove phase
        private void MoveNecrons()
        {
            foreach (var necron in necrons)
            {
                if (necronsMovedThisTurn.Contains(necron)) continue;

                Point newLocation = new Point(0, 0);
                bool posOK = false;

                // Find a empty position for the Necron to move
                while (!posOK)
                {
                    int moveX = random.Next(-1, 2) * squaresize;
                    int moveY = random.Next(-1, 2) * squaresize;

                    newLocation = new Point(
                        Math.Max(1, Math.Min(necron.Location.X + moveX, (highwidth - 1) * squaresize + 1)),
                        Math.Max(1, Math.Min(necron.Location.Y + moveY, (highwidth - 1) * squaresize + 1))
                    );

                    posOK = true;

                    // Ensure the new position is not occupied
                    foreach (PictureBox m in mountains)
                        if (m.Location == newLocation)
                            posOK = false;
                    foreach (PictureBox n in necrons)
                        if (n.Location == newLocation)
                            posOK = false;
                    foreach (PictureBox p in players)
                        if (p.Location == newLocation)
                            posOK = false;
                }

                // Move the Necron and track its movement
                necron.Location = newLocation;
                necronsMovedThisTurn.Add(necron);
            }

            necronsMovedThisTurn.Clear(); // Reset after all Necrons move
        }
    }
}
