using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ucGraph
{
    public partial class ucGraph: UserControl
    {
        public ucGraph()
        {
            InitializeComponent();
            padding_x = 125;
            padding_y = 200;
            pen_per = new Pen(Color.FromArgb(206, 206, 206), 2);


        }

        Graphics gfx;
        int padding_x;
        int padding_y;
        Pen pen_per;
       
        int step_size_x;
        int step_size_y;
        int step_value_x;
        int step_value_y;

        private void UserControl1_Load(object sender, EventArgs e)
        {
            set_steps_size(30, 30, 1, 5);
            draw_Origin();


            //draw_value(100, 200, 150, 400, new Pen(Color.Yellow, 3));
        }


        private void draw_Origin()
        {
            draw_per();
            draw_steps_num();
            draw_steps();
            // 

            void draw_steps()
            {
                // x steps

                int size = 3;

                for (int i = padding_x; i < Width; i += step_size_x)
                {
                    gfx.DrawLine(pen_per, i, Height - padding_y - size, i, Height - padding_y + size);
                }

                for (int i = padding_x; i > -Width; i -= step_size_x)
                {
                    gfx.DrawLine(pen_per, i, Height - padding_y - size, i, Height - padding_y + size);
                }

                // y steps

                for (int i = Height - padding_y; i >= 0; i -= step_size_y)
                {
                    gfx.DrawLine(pen_per, padding_x - size, i, padding_x + size, i);
                }

                for (int i = Height - padding_y; i < Height; i += step_size_y)
                {
                    gfx.DrawLine(pen_per, padding_x - size, i, padding_x + size, i);
                }
            }

            void draw_steps_num()
            {

                for (int i = 0; i < Width / step_size_x; i++)
                {
                    if(i != 0)
                        gfx.DrawString((i * step_value_x).ToString(), Font, pen_per.Brush, new PointF(padding_x - 7 - ((i * step_value_x).ToString().Length-1) * ((float) Font.Size / 2.2f) + i * step_size_x, Height - padding_y + 3));
                }

                for (int i = 0; i > - Width / step_size_x; i--)
                {
                    if(i !=0)
                        gfx.DrawString((i * step_value_x).ToString(), Font, pen_per.Brush, new PointF(padding_x - 7 - ((i * step_value_x).ToString().Length - 1) * ((float)Font.Size / 2.2f) + i * step_size_x, Height - padding_y + 3));
                }

                for (int i = 0; i < Height / step_size_y; i++)
                {
                    if(i != 0)
                        gfx.DrawString((i * step_value_y).ToString(), Font, pen_per.Brush, new PointF(padding_x - 15 - (i * step_value_y).ToString().Length * 5, Height - padding_y - 10 - i * step_size_y));
                }


                for (int i = 0; i >- Height / step_size_y; i--)
                {
                    if (i != 0)
                        gfx.DrawString((i * step_value_y).ToString(), Font, pen_per.Brush, new PointF(padding_x - 15 - (i * step_value_y).ToString().Length * 5, Height - padding_y - 10 - i * step_size_y));
                }

            }

            void draw_per(){

                
                pictureBox1.Image = new Bitmap(Width, Height);
                gfx = Graphics.FromImage(pictureBox1.Image);
                gfx.DrawLine(pen_per, 0, Height - padding_y, Width, Height - padding_y);
                gfx.DrawLine(pen_per, padding_x, 0, padding_x, Height);

            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void set_steps_size(int size_x,int size_y,int value_x,int value_y)
        {
            step_size_x = size_x;
            step_size_y = size_y;
            step_value_x = value_x;
            step_value_y = value_y;
        }

        public void draw_pixel(int x,int y,Pen pen)
        {
            y = Height - padding_x - y;
            x = x + padding_x;

            gfx.FillEllipse(pen.Brush, x - pen.Width/ 2, y - pen.Width / 2, pen.Width, pen.Width);
        }

        public void draw_pixel(int x1, int y1,int x2,int y2, Pen pen)
        {
            y1 = Height - padding_x -  y1;
            y2 = Height - padding_x -  y2;
            x1 = x1 + padding_x;
            x2 = x2 + padding_x;
            gfx.DrawLine(pen,x1 - pen.Width / 2, y1 - pen.Width / 2, x2 - pen.Width / 2, y2 - pen.Width / 2);

        }

        public void draw_value(int x, int y, Pen pen)
        {
            draw_pixel(x * step_size_x / step_value_x,y * step_size_y/step_value_y,pen);    
        }

        class clsGraph
        {
            public List<Point> points;
            public Color color;

        }
    }
}
