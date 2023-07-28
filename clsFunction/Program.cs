using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace nsFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            clsFunction fun = new clsFunction("1 + sin(3.2 - 4 * 3) + 5 - 9 * 0.1 * (1-cos(43 - exp(3 + sin(3.2)))) - (5-4 * (3-1)) + exp(1-2 * sqrt(20.32)+3*3+4-3)");

            Console.WriteLine(fun.calc(3.14f));

            int x = 0;
            Console.ReadLine();
        }


        class clsFunction
        {
            public List<stOpNode> op_nodes;
            public clsFunction(string expression) 
            {
                op_nodes = new List<stOpNode>();

                for(int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == ' ')
                    {
                        expression = expression.Remove(i, 1);
                        i--;
                    }
                }

                string current_number = "";
                enCommonFun fun_type;
                // "1+(4-5)+7*(5-8)"
                
                for(int i = 0; i < expression.Length; i++)
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
                    else if ("+-*/".Contains(expression[i]))
                    {
                        if (current_number != "")
                            op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                        op_nodes.Add(new stOpNode(stOpNode.enType.eOp, expression[i]));
                        current_number = "";
                    }
                    else if (char.IsDigit(expression[i]) || expression[i] == '.')
                    {
                        current_number += expression[i];
                    }
                    else if (isVariable(expression,i))
                    {
                        if (current_number != "")
                            op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                        op_nodes.Add(new stOpNode(stOpNode.enType.eVariable,'x'));
                        current_number = "";
                    }
                    else if ((fun_type = DetermineCommonFunciton(expression,i)) != enCommonFun.none)
                    {
                        if (current_number != "")
                            op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

                        op_nodes.Add(new stOpNode(stOpNode.enType.eFunction, fun_type));
                        current_number = "";

                        i = i + funcs_names[((int)fun_type)].Length -1;// skip the rest of the function
                    }
                }

                if (current_number != "")
                    op_nodes.Add(new stOpNode(stOpNode.enType.eNumber, Convert.ToSingle(current_number)));

            }



            float calc_between_Brackets(List<stOpNode> opNodes)
            {
                while(opNodes.Count != 1) 
                {
                    calc_mul_div();
                    calc_add_sub();
                }

                return opNodes[0].number;


                void calc_mul_div()
                {
                    for(int i = 0; i < opNodes.Count; i++)
                    {
                        if (opNodes[i].isOperation() && (opNodes[i].op == '*' || opNodes[i].op == '/'))
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
                        if (opNodes[i].isOperation() && (opNodes[i].op == '+' || opNodes[i].op == '-'))
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
                    int index_open_bracket = -1,index_close_bracket = 0;

                    for(int i = 0; i < list_op_nodes.Count(); i++)
                    {

                        if (list_op_nodes[i].isOpenBraket() && ((i == 0) || (!list_op_nodes[i-1].isFunction())))
                        {
                            index_open_bracket = i;
                        }
                        else if (list_op_nodes[i].isCloseBraket() && index_open_bracket != -1)
                        {
                            index_close_bracket = i;

                            float result = calc_between_Brackets(subOperation(index_open_bracket + 1, index_close_bracket - 1));

                            list_op_nodes.RemoveRange(index_open_bracket,index_close_bracket - index_open_bracket + 1);
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

                    for (int i = 0; i < list_op_nodes.Count; i++)
                    {
                        
                        if (list_op_nodes[i].isFunction())
                        {
                            index_open_bracket = i + 1;
                        }
                        else if (list_op_nodes[i].isCloseBraket() && index_open_bracket != -1)
                        {
                            index_close_bracket = i;

                            float func_input_result = calc_between_Brackets(subOperation(index_open_bracket + 1, index_close_bracket - 1));
                            float result = apply_function(list_op_nodes[index_open_bracket-1].fun, func_input_result);
                            list_op_nodes.RemoveRange(index_open_bracket - 1, index_close_bracket - (index_open_bracket - 1) + 1);
                            list_op_nodes.Insert(index_open_bracket-1, new stOpNode(stOpNode.enType.eNumber, result));
                            i = 0;
                            index_open_bracket = -1;
                        }



                    }
                    // Comming soon
                }

                float apply_function(enCommonFun fun_type,float fun_input)
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
                            return 404;//(float)Math.Pow(fun_input,); // To Handle this case we must handle multivariable function and this for later time

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

                List<stOpNode> subOperation(int begin,int end)
                {

                    List<stOpNode> subOperation_nodes = new List<stOpNode>();
                    for(int i =begin; i<=end; i++)
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
                    eVariable
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

                public void set(enType type,float number)
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
                    this.fun =fun ;
                }

                
                public stOpNode(enType type)
                {
                    this.type = type;
                    this.number = 0;
                    this.op = ' ';
                    fun = enCommonFun.none;
                }
                public stOpNode(enType type,char op)
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
                while (start_index >= 0 && char.IsLetter(expression[start_index]) )
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
               
                



                enCommonFun _DetermineCommonFunciton(string fun){


                    
                    for(int i = 0; i < funcs_names.Length; i++)
                    {
                        if(fun == funcs_names[i])
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
