using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

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

        private void UserControl1_Load(object sender, EventArgs e)
        {
            set_steps_size(30, 30, 1f, 1f);
            draw_Origin();
            save_mouse();
            timer_MouseChangeTracer.Start();
            functions = new List<clsFunction>();
            functions.Add(new clsFunction("sin(x)",0,0  ,new Pen(Color.Red,2)));

            draw_functions();
            
        }

        private void save_mouse()
        {
            mouse.X = Cursor.Position.X;
            mouse.Y = Cursor.Position.Y;
        }
        private void draw_Origin()
        {
            draw_per();
            draw_steps_num();
            draw_steps();

            void draw_steps()
            {

                int size = 3;

                draw_x_steps();
                draw_y_steps();


                void draw_x_steps() 
                {
                    for (int i = padding_x; i < Width; i += step_size_x)
                    {
                        gfx.DrawLine(pen_per, i, Height - padding_y - size, i, Height - padding_y + size);
                    }

                    for (int i = padding_x; i > -Width; i -= step_size_x)
                    {
                        gfx.DrawLine(pen_per, i, Height - padding_y - size, i, Height - padding_y + size);
                    }
                }
                void draw_y_steps()
                {
                    for (int i = Height - padding_y; i >= 0; i -= step_size_y)
                    {
                        gfx.DrawLine(pen_per, padding_x - size, i, padding_x + size, i);
                    }

                    for (int i = Height - padding_y; i < Height; i += step_size_y)
                    {
                        gfx.DrawLine(pen_per, padding_x - size, i, padding_x + size, i);
                    }
                }
            }

            void draw_steps_num()
            {
                draw_x_num();
                draw_y_num();


                void draw_x_num()
                {
                    int counter = 0;

                    for (int i = padding_x; i < Width; i += step_size_x)
                    {

                        gfx.DrawString((counter * step_value_x).ToString(), Font, pen_per.Brush, new PointF(i - 12,Height -(padding_y) + 5));;
                        counter++;
                    }
                    counter = 0;
                    for (int i = padding_x; i > -Width; i -= step_size_x)
                    {
                        gfx.DrawString((counter * step_value_x).ToString(), Font, pen_per.Brush, new PointF(i - 12, Height - (padding_y) + 5));
                        counter--;
                    }

                 
                }


                void draw_y_num()
                {
                    int counter = -1;

                    for (int i = Height - padding_y; i < Height; i += step_size_y)
                    {

                            gfx.DrawString((counter * step_value_y).ToString(), Font, pen_per.Brush, new PointF(padding_x - 27 - (((counter * step_value_y).ToString().Length - 2) * ((float)Font.Size)/2.2f) , Height - padding_y - counter * step_size_y));

                        counter--;
                    }

                    counter = 1;
                    for (int i = Height - padding_y; i >= 0; i -= step_size_y)
                    {
                            gfx.DrawString((counter * step_value_y).ToString(), Font, pen_per.Brush, new PointF(padding_x - 27 - (((counter * step_value_y).ToString().Length - 2) * ((float)Font.Size)/2.2f), Height - padding_y - counter * step_size_y));

                        counter++;
                    }

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

        private void set_steps_size(int size_x,int size_y,float value_x,float value_y)
        {
            step_size_x = size_x;
            step_size_y = size_y;
            step_value_x = value_x;
            step_value_y = value_y;
        }

        public void draw_pixel(int x,int y,Pen pen)
        {
            y = Height - padding_y - y;
            x = x + padding_x;

            gfx.FillEllipse(pen.Brush, x - pen.Width/ 2, y - pen.Width / 2, pen.Width, pen.Width);
        }

        public void draw_pixel(int x1, int y1,int x2,int y2, Pen pen)
        {
            y1 = Height - padding_y -  y1;
            y2 = Height - padding_y -  y2;
            x1 = x1 + padding_x;
            x2 = x2 + padding_x;
            gfx.DrawLine(pen,x1 - pen.Width / 2, y1 - pen.Width / 2, x2 - pen.Width / 2, y2 - pen.Width / 2);

        }

        public void draw_value(int x, int y, Pen pen)
        {
            draw_pixel(  (int)(x * step_size_x / step_value_x),(int)(y * step_size_y/step_value_y),pen);    
        }

        private void ucGraph_MouseMove(object sender, MouseEventArgs e)
        {

            //if (Control.MouseButtons == MouseButtons.Left)
            //{
            //    padding_x = Cursor.Position.X;
            //    padding_y = Height - Cursor.Position.Y;
            //    draw_Origin();
            //}
        }

        private void timer_MouseChangeTracer_Tick(object sender, EventArgs e)
        {
            

            

            if (Control.MouseButtons == MouseButtons.Left)
            {

                padding_x +=    Cursor.Position.X - mouse.X;
                padding_y +=  - Cursor.Position.Y + mouse.Y;
                draw_Origin();
                save_mouse();
                draw_functions();
            }
        }

        private void ucGraph_MouseDown(object sender, MouseEventArgs e)
        {
            save_mouse();     
        }

        private void draw_function(clsFunction fun)
        {

            int x = (int) (-padding_x * step_value_x);
            int y = (int)(step_size_y * fun.calc((float)x / step_size_x));

            for (int i = (int)(-padding_x * step_value_x); i < Width; i++)
            {
                int tmp_y = (int)(step_size_y * fun.calc((float)i / step_size_x)) ;
                draw_pixel( (int)((float)i/  step_value_x),(int) ((float)tmp_y / step_value_y), (int)((float)x / step_value_x), (int)((float)y / step_value_y), fun.pen) ;
                x = i;
                y = tmp_y;
            }
        }

        private void draw_functions()
        {
            foreach(clsFunction fun in functions)
            {
                draw_function(fun);
            }
        }

        public Graphics gfx;
        public int padding_x;
        public int padding_y;
        public Pen pen_per;
        public int step_size_x;
        public int step_size_y;
        public float step_value_x;
        public float step_value_y;
        public Point mouse;
        public List<clsFunction> functions;

        public class clsFunction
        {

            public string function;
          
            

            public float  start;
            public float  end;
            public Pen pen = new Pen(Color.AliceBlue,2);


            public float calc(float input)
            {
                
                return (float) Math.Sin(input);
            }

            public clsFunction(string function, float start, float end, Pen pen)
            {
                this.function = function;
                this.start = start;
                this.end = end;
                this.pen = pen;
            }
        }
    }
}
