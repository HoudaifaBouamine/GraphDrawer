using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ucFunctionControl_Project.Properties;
using System.Diagnostics;

namespace ucFunctionControl_Project
{
    public partial class ucFunctionControler: UserControl
    {
        public ucFunctionControler(int w,int h)
        {
            InitializeComponent();
            _bisMaximize = true;

            def_size = new Size(w, h);
        }

        public ucFunctionControler()
        {
            InitializeComponent();
            _bisMaximize = true;

            def_size = new Size(59, 100);
            this.BackgroundImage = null;
            
        }

        public ucGraph.ucGraph graph;
        private void ucFunctionControler_Load(object sender, EventArgs e)
        {
            functions = new List<ucFunctionTextBox>();
            //minmize();
        }

        public Size def_size;

        private bool _bisMaximize;
        public bool isMaximize()
        {
            return _bisMaximize;
        }

        public bool isMinimize()
        {
            return !_bisMaximize;
        }
        public void minmize()
        {
            if (_bisMaximize)
            {
                this.Dock = DockStyle.None;
                this.Location = new Point(0, this.Size.Height/2 - 40);
                _bisMaximize = false;
                def_size = this.Size;
                this.Size = new Size(22, 80);

                foreach (Control c in this.Controls)
                {
                    c.Visible = false;
                }

                this.BackgroundImage = Resources.img_close_window_Open;
            }
        }

        public void maxmize()
        {
            if (!_bisMaximize)
            {
                this.Dock = DockStyle.Left;
                this.BackgroundImage = null;


                _bisMaximize = true;
                this.Size = this.def_size;
                foreach (Control c in this.Controls)
                {
                    c.Visible = true;
                }

                // this.BackColor = Color.FromArgb(39, 39, 39);
                this.BackgroundImage = null;

            }
        }

        private void ucFunctionControler_Click(object sender, EventArgs e)
        {
            maxmize();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            minmize();

        }

        public List<ucFunctionTextBox> functions;
        public int counter = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            ucFunctionTextBox uc_fun = new ucFunctionTextBox();

            uc_fun.Location = new Point(30,functions.Count * (uc_fun.Height + 8) + 80);
            uc_fun.panel1.BackColor = Color.Red;
            uc_fun.id = counter++;
            functions.Add(uc_fun);
            this.Controls.Add(uc_fun);
            uc_fun.Focus();
        }

        public bool b_draw = false;
        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < functions.Count; i++)
            {
                if (!graph.update_func(functions[i].id, functions[i].expression, new Pen(functions[i].color, 2)))
                {
                    graph.add_new_func(functions[i].id, functions[i].expression, new Pen(functions[i].color, 2));
                }
            }

            owner.Refresh();
            b_draw = true;
        }

        Form owner;
        public void set_form(Form main_form)
        {
            this.owner = main_form;
        }
    }
}
