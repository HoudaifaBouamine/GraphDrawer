namespace Function_Graph_Drawer
{
    partial class frm_Graph_Drawer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ucGraph1 = new ucGraph.ucGraph();
            this.ucFunctionControler1 = new ucFunctionControl_Project.ucFunctionControler();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 32;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucGraph1
            // 
            this.ucGraph1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ucGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGraph1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F);
            this.ucGraph1.Location = new System.Drawing.Point(0, 0);
            this.ucGraph1.Name = "ucGraph1";
            this.ucGraph1.Size = new System.Drawing.Size(1189, 710);
            this.ucGraph1.TabIndex = 2;
            // 
            // ucFunctionControler1
            // 
            this.ucFunctionControler1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.ucFunctionControler1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.ucFunctionControler1.Location = new System.Drawing.Point(0, 0);
            this.ucFunctionControler1.Name = "ucFunctionControler1";
            this.ucFunctionControler1.Size = new System.Drawing.Size(398, 772);
            this.ucFunctionControler1.TabIndex = 3;
            this.ucFunctionControler1.Load += new System.EventHandler(this.ucFunctionControler1_Load_1);
            // 
            // frm_Graph_Drawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 710);
            this.Controls.Add(this.ucFunctionControler1);
            this.Controls.Add(this.ucGraph1);
            this.Name = "frm_Graph_Drawer";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_Graph_Drawer_MouseDown);
            this.Resize += new System.EventHandler(this.frm_Graph_Drawer_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ucGraph.ucGraph ucGraph1;
        private ucFunctionControl_Project.ucFunctionControler ucFunctionControler1;
    }
}

