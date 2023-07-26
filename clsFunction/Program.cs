using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace nsFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            clsFunction fun = new clsFunction("2 x");
            clsFunction.stSubString sub = new clsFunction.stSubString();
            sub.open = 3;
            sub.close = 5;
            string test = sub.replace("houdaifa", "+-+");
            Console.WriteLine(test);
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

            private float calc(string expression,float input)
            {



                return input;
            }
            public float calc(float input)
            {



                return input;
            }

            
            public struct stSubString
            {
                public int start;
                public int end;

                public int open;
                public int close;

              
                public string replace(string source,string new_substring)
                {
                    source = source.Remove(open, close - open + 1);
                    source = source.Insert(open,new_substring);

                    return source;
                }
            }
        }
    }
}
