﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ucFunctionControl_Project
{
    public partial class ucFunctionTextBox : UserControl
    {
        public ucFunctionTextBox(ucFunctionControl_Project.ucFunctionControler owner)
        {
            this.owner = owner;
            InitializeComponent();
        }

     
        private void ucFunctionTextBox_Load(object sender, EventArgs e)
        {
            this.expression = textBox1.Text;
            this.color = panel1.BackColor;
            this.textBox1.Focus();
        }

        
        public string expression;
        public Color color;
        public int id;
        public ucFunctionControl_Project.ucFunctionControler owner;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.expression = textBox1.Text;
        }

        private void panel1_BackColorChanged(object sender, EventArgs e)
        {
            this.color = panel1.BackColor;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            
            colorDialog1.ShowDialog();
            
            panel1.BackColor = colorDialog1.Color;
            owner.btn_draw.PerformClick();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
