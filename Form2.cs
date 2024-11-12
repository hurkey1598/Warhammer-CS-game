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
            this.DoubleBuffered = true;
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
            image.Size = new System.Drawing.Size(squaresize-2, squaresize-2);
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
                e.Graphics.DrawRectangle(Pens.Green, MX, MY, squaresize, squaresize);
                
                for (int j = 0; j < highwidth; j++)
                {
                    if (index < totalBoxes)
                    {
                        Rectangle rect = new Rectangle(j * squaresize, i * squaresize, squaresize, squaresize);
                        boxes[index++] = rect;

                        //if (rect.Contains(MousePosition))
                        //{
                        //    e.Graphics.DrawRectangle(new Pen(Color.Green, 2), rect);
                        //}
                    }
                }
            }
        }

        private void MouseClicked(object sender, MouseEventArgs e)
        {
            Player(MX,MY);
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            MX = 1+Convert.ToInt32(e.X / squaresize) * squaresize;
            MY = 1+Convert.ToInt32(e.Y / squaresize) * squaresize;
            this.Invalidate();
        }

        
    }
}
