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
            functions[0].set_expression("x*x + 2 * x - 1"); 
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

            private string fun_expresstion;
          
            

            public float  start;
            public float  end;
            public Pen pen = new Pen(Color.AliceBlue,2);
            public char variable_char = 'x';
            private float debug_input;

            public void set_expression(string expression)
            {
                fun_expresstion = "";
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] != ' ')
                    {
                        fun_expresstion += expression[i];
                    }
                }
               
            }
            
            public float calc(float input)
            {
                debug_input = input;
                string fun = fun_expresstion;

                int i_start = 0,i_end;

                for(int i = fun.Length - 1; i  >= 0; i--)
                {
                    if (fun[i] == variable_char)
                    {
                        fun = fun.Remove(i, 1);
                        fun = fun.Insert(i, input.ToString());
                        
                    }
                }


                while (!(result_is_calculated(fun)))
                {
                    bool no_brucets = true;

                      for (int i = 0; i < fun.Length; i++)
                      {

                          if (fun[i] == '(')
                          {
                              i_start = i;
                            no_brucets = false;
                          }
                          else if (fun[i] == ')')
                          {
                              i_end = i;
                            no_brucets = false;


                              string sub_result = (calc_op(fun.Substring(i_start + 1, i_end - i_start - 1)).ToString());
                              fun = fun.Remove(i_start, i_end - i_start + 1);
                              fun = fun.Insert(i_start, sub_result);
                          }
                      }

                    if (no_brucets)
                    {
                        return calc_op(fun);
                    }

                }                


                return Convert.ToSingle(fun);
            }

            private float calc_op(string expression)
            {

                if(debug_input + 0.00001 >= -0.966666639 && debug_input - 0.00001 <= -0.966666639)
                {
                    int x = 0;
                }

                for (int i = 0; i < expression.Length; i++)
                {
                    if (i + 1 < expression.Length && (expression[i] == '*' || expression[i] == '/')) 
                    {

                        if (expression[i + 1] == '+' || expression[i + 1] == '-')
                        {
                            char remover_char = expression[i + 1];
                            expression = expression.Remove(i + 1, 1);

                            for(int j = i - 1;j >= 0; j--)
                            {

                                if (remover_char == '-')
                                {
                                    if (expression[j] == '-')
                                    {

                                        expression = expression.Remove(j, 1);
                                        if(j != 0)
                                            expression = expression.Insert(j, "+");
                                        break;
                                    }
                                    else if (expression[j] == '+')
                                    {

                                        expression = expression.Remove(j, 1);
                                            expression = expression.Insert(j, "-");
                                        break;
                                    }
                                }
                                

                            }
                        }
                    
                    }
                    
                }


                for (int i = 0; i < expression.Length; i++)
                {
                    if (i + 1 < expression.Length && (expression[i] == '+' || expression[i] == '-'))
                    {
                        if ((expression[i+1] == '+' || expression[i+1] == '-'))
                        {

                            if(expression[i + 1] == expression[i])
                            {
                                expression = expression.Insert(i, "+");
                            }
                            else
                            {
                                expression = expression.Insert(i, "-");

                            }

                            expression = expression.Remove(i + 1, 2);
                        }
                    }

                }

                for (int i = 0; i < expression.Length; i++)
                {

                if (expression[i] == '*' || expression[i] == '/')
                {



                    // get left number
                    int k = 1;

                    k = 1;
                    while ((i - k) >= 0 && (char.IsDigit(expression[i - k]) || expression[i - k] == '.'))
                    {
                        k++;
                    }
                    k--;

                    float number1 = Convert.ToSingle(expression.Substring(i - k, k));

                    int i_start = i - k;


                    // get right number
                        k = 1;
                    while (i + k < expression.Length && (char.IsDigit(expression[i + k]) || expression[i + k] == '.'))
                    {
                        k++;
                    }
                    k--;

                    float number2 = Convert.ToSingle(expression.Substring(i+1,k));
                    int i_end = i + k;

                    float result;

                    if (expression[i] == '*') 
                    {
                        result = number1 * number2;
                    }
                    else
                    {
                        result = number1 / number2;
                    }

                    expression = expression.Remove(i_start,i_end - i_start + 1);
                    expression = expression.Insert(i_start,result.ToString());

                    i = 0;
                }
                   

            }

                for (int i = 0; i < expression.Length; i++)
                {

                    if ((expression[i] == '+' || expression[i] == '-' )&& i != 0)
                    {



                        // get left number
                        int k = 1;

                        k = 1;
                        char signe_num_1 = '+';
                        
                        while ((i-k) >= 0 && (char.IsDigit(expression[i - k]) || expression[i - k] == '.'))
                        {
                            k++;

                            
                        }
                        if(i-k >= 0)
                        {
                            signe_num_1 = expression[i - k];
                        }
                        k--;

                      
                        float number1 = Convert.ToSingle(expression.Substring(i - k, k));

                        int i_start = i - k;


                        // get right number
                        k = 1;
                        while (i + k < expression.Length && (char.IsDigit(expression[i + k]) || expression[i + k] == '.'))
                        {
                            k++;
                        }
                        k--;

                        float number2 = Convert.ToSingle(expression.Substring(i + 1, k));
                        int i_end = i + k;

                        float result;
                        int remove_singe = 0;

                        if (expression[i] == '+')
                        {
                            if (signe_num_1 == '-')
                            {
                                remove_singe = 1;
                                number1 *= -1;
                            }

                                result = number1 + number2;
                        }
                        else
                        {
                            if (signe_num_1 == '-')
                            {
                                number1 *= -1;
                                remove_singe = 1;
                            }

                            result = number1 - number2;
                        }

                        expression = expression.Remove(i_start - remove_singe, i_end - i_start + 1 + remove_singe);
                        expression = expression.Insert(i_start - remove_singe, result.ToString());

                        i = 0;
                    }


                }



                return Convert.ToSingle( expression);

            }

            private bool result_is_calculated(string result)
            {

                for(int i = 0; i < result.Length; i++)
                {
                    if(i == 0)
                    {
                        if (result[0] == '-')
                        {
                            continue;
                        }
                    }

                    if (!(char.IsDigit(result[i]) || result[i] == '.'))
                    {
                        return false;
                    }
                }

                return true;
            }
            public clsFunction(string function, float start, float end, Pen pen)
            {
                this.fun_expresstion = function;
                this.start = start;
                this.end = end;
                this.pen = pen;
            }
        }
    }
}
