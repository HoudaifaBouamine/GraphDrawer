﻿using System;
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

        private Point getmouse()
        {
            return new Point( Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Tag = '1';
            ucGraph1.set_owner(this);
           
            
            ucFunctionControler1.minmize();
            ucFunctionControler1.graph = ucGraph1;
            ucFunctionControler1.set_form(this);
            this.AcceptButton = ucFunctionControler1.btn_draw;
            timer1.Start();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (getmouse().X <= ucFunctionControler1.Location.X + ucFunctionControler1.Width&& getmouse().X  >= ucFunctionControler1.Location.X && getmouse().Y <= ucFunctionControler1.Location.Y + ucFunctionControler1.Height && getmouse().Y >= ucFunctionControler1.Location.Y)
            {
                Tag = '0';
            }
            else
            {
                Tag = '1';
            }


        }

        private void frm_Graph_Drawer_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void ucFunctionControler1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void ucFunctionControler1_Click_1(object sender, EventArgs e)
        {
            Tag = '0';

        }

        private void ucFunctionControler1_Enter(object sender, EventArgs e)
        {
            Tag = '0';

        }

     
        private void frm_Graph_Drawer_Resize(object sender, EventArgs e)
        {
            ucGraph1.Refresh();
            ucGraph1.update();
        }

        private void ucFunctionControler1_Load(object sender, EventArgs e)
        {

        }

        private void ucFunctionControler1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
