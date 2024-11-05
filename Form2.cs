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
        public Form2(Form1 parent)
        {
            InitializeComponent();
            
        }

        int MX = 10;
        int MY = 10;
        int squaresize = 50;
        int highwidth = 10;
        Rectangle[] boxes;

        private void Player(int x, int y)
        {
            PictureBox image;
            image = new PictureBox();
            image.Size = new System.Drawing.Size(50, 50);
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            image.Location = new System.Drawing.Point(x, y);
            image.Image = Properties.Resources.Space_marine;
            this.Controls.Add(image);
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

                
                for (int j = 0; j < highwidth; j++)
                {
                    if (index < totalBoxes)
                    {
                        Rectangle rect = new Rectangle(j * squaresize, i * squaresize, squaresize, squaresize);
                        boxes[index++] = rect;

                        if (rect.Contains(MousePosition))
                        {
                            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), rect);
                        }
                    }
                }
            }
        }

        private void MouseClicked(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Contains(e.Location))
                {
                    Player(boxes[i].X, boxes[i].Y);
                    break;
                }
            }
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            MX = Convert.ToInt32(e.X / squaresize) * squaresize;
            MY = Convert.ToInt32(e.Y / squaresize) * squaresize;
        }

        
    }
}
