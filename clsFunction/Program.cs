using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            clsFunction fun = new clsFunction("2 x");
            Console.WriteLine(fun.calc(2));

            Console.ReadLine();
        }

        class clsFunction
        {
            private string expression;
            public clsFunction(string expression) 
            {
                this.expression = "";
                for(int i = 0; i < expression.Length;i++)
                {
                    if (expression[i] != ' ')
                    {
                        this.expression += expression[i];
                    }
                }
            }

            public float calc(float input)
            {


                return 2 * input;
            }
        }
    }
}
