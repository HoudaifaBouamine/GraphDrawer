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

        Form owner;
        public void set_owner(Form frm)
        {
            this.owner = frm;
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            set_steps_size(30, 30, 1f, 1f);
            draw_Origin();
            save_mouse();
            timer_MouseChangeTracer.Start();
            functions = new List<clsGraph>();

          
        }

        public void add_new_func(int id,string expression,Pen pen)
        {
            functions.Add(new clsGraph(id,expression, pen));
            draw_functions();
        }

        public bool update_func(int id,string expression,Pen pen)
        {
            for (int i = 0; i < functions.Count; i++)
            {
                if (functions[i].id == id)
                {
                    functions[i] = new clsGraph(id, expression, pen);
                    draw_functions();

                    return true;
                }
            }

            return false;
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


            float pt1_x = x1 - pen.Width / 2;
            float pt1_y = y1 -pen.Width / 2;

            float pt2_x = x2 - pen.Width / 2;
            float pt2_y = y2 - pen.Width / 2;

            if(in_x_range(pt1_x) && in_x_range(pt2_x) && in_y_range(pt1_y) && in_y_range(pt2_y))
                gfx.DrawLine(pen,pt1_x,pt1_y, pt2_x, pt2_y);


            bool in_x_range(float num)
            {
                return num >= 0 && num < this.Width;
            }

            bool in_y_range(float num)
            {
                return num >= 0 && num < this.Height;
            }
        }

        public void draw_value(int x, int y, Pen pen)
        {
            draw_pixel(  (int)(x * step_size_x / step_value_x),(int)(y * step_size_y/step_value_y),pen);    
        }

        private void ucGraph_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void timer_MouseChangeTracer_Tick(object sender, EventArgs e)
        {

            if (Control.MouseButtons == MouseButtons.Left && mouse_in_range())
            {

                padding_x +=    Cursor.Position.X - mouse.X;
                padding_y +=  - Cursor.Position.Y + mouse.Y;
                draw_Origin();
                save_mouse();
                draw_functions();
            }

            bool mouse_in_range()
            {
                if(owner != null)
                    return Cursor.Position.X >= this.owner.Location.X  && Cursor.Position.X < Width + this.owner.Location.X && Cursor.Position.Y >= this.owner.Location.Y +(this.owner.Height-this.Height - 5) && Cursor.Position.Y < Height + this.owner.Location.Y && owner.Tag.ToString() == "1";
                else 
                    return true;
            }
        }

        private void ucGraph_MouseDown(object sender, MouseEventArgs e)
        {
            save_mouse();     
        }

        private void draw_function(clsGraph fun)
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
            foreach(clsGraph fun in functions)
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
        public List<clsGraph> functions;

        public class clsGraph
        {

            private clsFunction fun_expresstion;
            public int id;

            public clsGraph(int id,string expression,Pen pen)
            {
                this.id = id;
                this.fun_expresstion = new clsFunction(expression);
                this.pen = pen;
            }

            public float calc(float input)
            {
                return fun_expresstion.calc(input);
            }



            public float  start;
            public float  end;
            public Pen pen = new Pen(Color.AliceBlue,2);
            public char variable_char = 'x';
            private float debug_input;

            public class clsFunction
            {
                public List<stOpNode> op_nodes;
                public clsFunction(string expression)
                {
                    op_nodes = new List<stOpNode>();

                    for (int i = 0; i < expression.Length; i++)
                    {
                        if (expression[i] == ' ')
                        {
                            expression = expression.Remove(i, 1);
                            i--;
                        }
                    }

                    string current_number = "";
                    enCommonFun fun_type;

                    for (int i = 0; i < expression.Length; i++)
                    {

                        if (expression[i] == '(')
                        {
                            if (current_number != "")
                                op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                            op_nodes.Add(new stOpNode(stOpNode.enType.eOpen, '('));

                            current_number = "";
                        }
                        else if (expression[i] == ')')
                        {
                            if (current_number != "")
                                op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                            op_nodes.Add(new stOpNode(stOpNode.enType.eClose, ')'));
                            current_number = "";
                        }
                        else if ("+-*/".Contains(expression[i])&&i!=0&&(expression[i-1] != '('))
                        {
                            if (current_number != "")
                                op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                            op_nodes.Add(new stOpNode(stOpNode.enType.eOp, expression[i]));
                            current_number = "";
                        }
                        else if (expression[i] == ',')
                        {
                            if (current_number != "")
                                op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                            op_nodes.Add(new stOpNode(stOpNode.enType.eSeperator, expression[i]));
                            current_number = "";
                        }
                        else if (char.IsDigit(expression[i]) || expression[i] == '.' || ("+-".Contains(expression[i])))
                        {
                            current_number += expression[i];
                        }
                        else if (isVariable(expression, i))
                        {
                            if (current_number != "")
                                op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                            op_nodes.Add(new stOpNode(stOpNode.enType.eVariable, 'x'));
                            current_number = "";
                        }
                        else if ((fun_type = DetermineCommonFunciton(expression, i)) != enCommonFun.none)
                        {
                            if (current_number != "")
                                op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                            op_nodes.Add(new stOpNode(stOpNode.enType.eFunction, fun_type));
                            current_number = "";

                            i = i + funcs_names[((int)fun_type)].Length - 1;// skip the rest of the function
                        }

                    }

                    if (current_number != "")
                        op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                }



                float calc_between_Brackets(List<stOpNode> opNodes)
                {
                    while (opNodes.Count != 1)
                    {
                        calc_mul_div();
                        calc_add_sub();
                    }

                    return opNodes[0].number;


                    void calc_mul_div()
                    {
                        for (int i = 0; i < opNodes.Count; i++)
                        {
                            if (i != 0 && opNodes[i].isOperation() && (opNodes[i].op == '*' || opNodes[i].op == '/'))
                            {
                                float result;
                                if (opNodes[i].op == '*')
                                {
                                    result = opNodes[i - 1].number * opNodes[i + 1].number;
                                }
                                else
                                {
                                    result = opNodes[i - 1].number / opNodes[i + 1].number;
                                }

                                opNodes.RemoveRange(i - 1, 3);

                                opNodes.Insert(i - 1, new stOpNode(stOpNode.enType.eNumber, result));
                                i--;
                            }
                        }
                    }

                    void calc_add_sub()
                    {
                        for (int i = 0; i < opNodes.Count; i++)
                        {
                            if (i != 0 && opNodes[i].isOperation() && (opNodes[i].op == '+' || opNodes[i].op == '-'))
                            {
                                float result;
                                if (opNodes[i].op == '+')
                                {
                                    result = opNodes[i - 1].number + opNodes[i + 1].number;
                                }
                                else
                                {
                                    result = opNodes[i - 1].number - opNodes[i + 1].number;
                                }

                                opNodes.RemoveRange(i - 1, 3);

                                opNodes.Insert(i - 1, new stOpNode(stOpNode.enType.eNumber, result));
                                i--;
                            }
                        }
                    }
                }

                public float calc(float input)
                {

                    List<stOpNode> list_op_nodes; copy_list();
                    replace_var_with_input();
                    find_free_brackets_and_calc();
                    find_common_function_and_calc();//
                    find_free_brackets_and_calc();

                    float final_result = calc_between_Brackets(list_op_nodes);
                    return final_result;


                    void copy_list()
                    {
                        list_op_nodes = new List<stOpNode>();

                        for (int i = 0; i < this.op_nodes.Count; i++)
                        {
                            list_op_nodes.Add(new stOpNode(this.op_nodes[i]));
                        }
                    }

                    void replace_var_with_input()
                    {
                        for (int i = 0; i < list_op_nodes.Count; i++)
                        {
                            if (list_op_nodes[i].isVariable())
                            {
                                list_op_nodes[i].set(stOpNode.enType.eNumber, input);
                            }
                        }
                    }

                    void find_free_brackets_and_calc()
                    {
                        int index_open_bracket = -1, index_close_bracket = 0;

                        for (int i = 0; i < list_op_nodes.Count(); i++)
                        {

                            if (list_op_nodes[i].isOpenBraket() && ((i == 0) || (!list_op_nodes[i - 1].isFunction())))
                            {
                                index_open_bracket = i;
                            }
                            else if (list_op_nodes[i].isCloseBraket() && index_open_bracket != -1)
                            {
                                index_close_bracket = i;

                                float result = calc_between_Brackets(subOperation(index_open_bracket + 1, index_close_bracket - 1));

                                list_op_nodes.RemoveRange(index_open_bracket, index_close_bracket - index_open_bracket + 1);
                                list_op_nodes.Insert(index_open_bracket, new stOpNode(stOpNode.enType.eNumber, result));

                                i = -1;
                                index_open_bracket = -1;
                            }
                            else if (list_op_nodes[i].isFunction())
                            {
                                index_open_bracket = -1;
                            }
                        }

                        return;
                    }

                    void find_common_function_and_calc()
                    {

                        // this handle only non-composite function , composite function later

                        int index_open_bracket = -1;
                        int index_close_bracket = -1;
                        int index_seperator = -1;
                        bool mul_variable = false;

                        for (int i = 0; i < list_op_nodes.Count; i++)
                        {

                            if (list_op_nodes[i].isFunction())
                            {
                                index_open_bracket = i + 1;
                            }
                            else if (list_op_nodes[i].isCloseBraket() && index_open_bracket != -1)
                            {
                                index_close_bracket = i;

                                if (mul_variable)
                                {
                                    float func_input_result_1 = calc_between_Brackets(subOperation(index_open_bracket + 1, index_seperator - 1));
                                    float func_input_result_2 = calc_between_Brackets(subOperation(index_seperator + 1, index_close_bracket - 1));
                                    float result = apply_function(list_op_nodes[index_open_bracket - 1].fun, func_input_result_1, func_input_result_2);
                                    list_op_nodes.RemoveRange(index_open_bracket - 1, index_close_bracket - (index_open_bracket - 1) + 1);
                                    list_op_nodes.Insert(index_open_bracket - 1, new stOpNode(stOpNode.enType.eNumber, result));
                                    i = -1;
                                    index_open_bracket = -1;
                                    mul_variable = false;
                                }
                                else
                                {

                                    float func_input_result = calc_between_Brackets(subOperation(index_open_bracket + 1, index_close_bracket - 1));
                                    float result = apply_function(list_op_nodes[index_open_bracket - 1].fun, func_input_result);
                                    list_op_nodes.RemoveRange(index_open_bracket - 1, index_close_bracket - (index_open_bracket - 1) + 1);
                                    list_op_nodes.Insert(index_open_bracket - 1, new stOpNode(stOpNode.enType.eNumber, result));
                                    i = -1;
                                    index_open_bracket = -1;

                                }
                            }
                            else if (list_op_nodes[i].isSeperator() && index_open_bracket != -1)
                            {
                                mul_variable = true;
                                index_seperator = i;

                            }



                        }
                        // Comming soon
                    }

                    float apply_function(enCommonFun fun_type, float fun_input, float fun_input_2 = 0)
                    {




                        switch (fun_type)
                        {
                            case enCommonFun.exp:
                                return (float)Math.Exp(fun_input);

                            case enCommonFun.ln:
                                return (float)Math.Log(fun_input);

                            case enCommonFun.log:
                                return (float)Math.Log10(fun_input);

                            case enCommonFun.pow:
                                return (float)Math.Pow(fun_input, fun_input_2);

                            case enCommonFun.sqrt:
                                return (float)Math.Sqrt(fun_input);

                            case enCommonFun.cos:
                                return (float)Math.Cos(fun_input);

                            case enCommonFun.sin:
                                return (float)Math.Sin(fun_input);

                            case enCommonFun.tan:
                                return (float)Math.Tan(fun_input);

                            case enCommonFun.acos:
                                return (float)Math.Acos(fun_input);

                            case enCommonFun.asin:
                                return (float)Math.Asin(fun_input);

                            case enCommonFun.atan:
                                return (float)Math.Atan(fun_input);

                            case enCommonFun.none:
                                return -505;

                            default:
                                return -111;
                        }




                    }

                    List<stOpNode> subOperation(int begin, int end)
                    {

                        List<stOpNode> subOperation_nodes = new List<stOpNode>();
                        for (int i = begin; i <= end; i++)
                        {
                            subOperation_nodes.Add(list_op_nodes[i]);
                        }

                        return subOperation_nodes;
                    }
                }





                public class stOpNode
                {
                    public enum enType
                    {
                        eOp,
                        eNumber,
                        eOpen,
                        eClose,
                        eFunction,
                        eVariable,
                        eSeperator
                    }

                    public stOpNode(stOpNode opNode)
                    {
                        this.type = opNode.type;
                        this.op = opNode.op;
                        this.number = opNode.number;
                        this.fun = opNode.fun;
                    }

                    public enType type;
                    public float number;
                    public char op;
                    public enCommonFun fun;

                    public bool isVariable()
                    {
                        return type == enType.eVariable;
                    }

                    public bool isFunction()
                    {
                        return type == enType.eFunction;
                    }

                    public bool isNumber()
                    {
                        return type == enType.eNumber;
                    }

                    public bool isOperation()
                    {
                        return type == enType.eOp;
                    }

                    public bool isOpenBraket()
                    {
                        return type == enType.eOpen;
                    }

                    public bool isCloseBraket()
                    {
                        return type == enType.eClose;
                    }

                    public bool isSeperator()
                    {
                        return type == enType.eSeperator;
                    }
                    public void set(enType type, float number)
                    {
                        this.type = type;
                        this.number = number;
                        this.op = ' ';
                        this.fun = enCommonFun.none;
                    }

                    public void set(enType type, char op)
                    {
                        this.type = type;
                        this.number = 0;
                        this.op = op;
                        this.fun = enCommonFun.none;
                    }

                    public void set(enType type, enCommonFun fun)
                    {
                        this.type = type;
                        this.number = 0;
                        this.op = ' ';
                        this.fun = fun;
                    }


                    public stOpNode(enType type)
                    {
                        this.type = type;
                        this.number = 0;
                        this.op = ' ';
                        fun = enCommonFun.none;
                    }
                    public stOpNode(enType type, char op)
                    {
                        this.type = type;
                        this.number = 0;
                        this.op = op;
                        fun = enCommonFun.none;
                    }

                    public stOpNode(enType type, float number)
                    {
                        this.type = type;
                        this.number = number;
                        this.op = ' ';
                        fun = enCommonFun.none;
                    }

                    public stOpNode(enType type, enCommonFun fun)
                    {
                        this.type = type;
                        this.number = 0;
                        this.op = ' ';
                        this.fun = fun;
                    }


                }

                string[] funcs_names = { "exp", "ln", "log", "pow", "sqrt", "cos", "sin", "tan", "acos", "asin", "atan" };

                public enum enCommonFun
                {
                    exp = 0,
                    ln,
                    log,
                    pow,
                    sqrt,
                    cos,
                    sin,
                    tan,
                    acos,
                    asin,
                    atan,

                    none
                };
                private enCommonFun DetermineCommonFunciton(string expression, int index)
                {

                    int start_index = index;
                    while (start_index >= 0 && char.IsLetter(expression[start_index]))
                    {
                        start_index--;

                    }
                    start_index++;

                    int function_length = 0;

                    while (start_index + function_length < expression.Length && char.IsLetter(expression[start_index + function_length]))
                    {
                        function_length++;
                    }

                    return _DetermineCommonFunciton(expression.Substring(start_index, function_length));





                    enCommonFun _DetermineCommonFunciton(string fun)
                    {



                        for (int i = 0; i < funcs_names.Length; i++)
                        {
                            if (fun == funcs_names[i])
                            {
                                return (enCommonFun)i;
                            }
                        }

                        return enCommonFun.none;
                    }
                }

                private bool isVariable(string expression, int index)
                {
                    if (expression[index] == 'x' && index == 0 && index + 1 < expression.Length && !char.IsLetter(expression[index + 1]))
                        return true;

                    if (expression[index] == 'x' && index > 0 && !char.IsLetter(expression[index - 1]) && index + 1 < expression.Length && !char.IsLetter(expression[index + 1]))
                        return true;

                    if (expression[index] == 'x' && index > 0 && !char.IsLetter(expression[index - 1]) && index + 1 >= expression.Length)
                        return true;

                    if (expression[index] == 'x' && expression.Length == 1)
                        return true;

                    return false;
                }
            }



        }
    }
}
