using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warhammer
{
    internal class Player
    {
    }
}

/* 

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

                this.Controls.Add(player);
                piecesToPlace--;

                // Enable the "End Setup" button once all pieces are placed
                if (spaceMarineCount >= 0)
                {
                    SetupendBtn.Enabled = true;
                }

                // Add the player piece to the list and dictionary and adds them to a count
                players.Add(player);
                playerMoves[player] = 0;
                spaceMarineCount++; // Increment the count for Space Marines
            }

        }



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
*/
