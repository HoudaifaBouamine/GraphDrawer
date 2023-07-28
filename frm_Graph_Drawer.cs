using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Function_Graph_Drawer
{
    public partial class frm_Graph_Drawer : Form
    {
        public frm_Graph_Drawer()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            Tag = '1';
            ucGraph1.set_owner(this);
            ucGraph1.add_new_func("pow(x,2)", new Pen(Color.Red, 2));
            ucGraph1.add_new_func("pow(x,3)", new Pen(Color.Yellow, 2));

        }
    }
}
