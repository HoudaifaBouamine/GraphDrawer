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
           
            Console.ReadLine();
        }

        class clsFunction
        {
            private List<stOpNode> expression;
            public clsFunction(string expression) 
            {
                

                
            }

           
            public float calc(float input)
            {



                return input;
            }

            struct stOpNode
            {
                private enum enType
                {
                    eOp,
                    eNumber,
                    eOpen,
                    eClose,
                    eFunction
                }
            }


        }
    }
}
