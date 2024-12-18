using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warhammer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form2 Game;


        private void GameBtn_Click(object sender, EventArgs e)
        {
            Game = new Form2(this);
            Game.FormClosed += Game_FormClosed;
            Game.Show();
            this.Hide();
        }

        private void SettingBtn_Click(object sender, EventArgs e)
        {

        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        { 
            this.Close();
        }
    }
}
