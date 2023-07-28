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
            this.ucGraph1 = new ucGraph.ucGraph();
            this.SuspendLayout();
            // 
            // ucGraph1
            // 
            this.ucGraph1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ucGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGraph1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F);
            this.ucGraph1.Location = new System.Drawing.Point(0, 0);
            this.ucGraph1.Name = "ucGraph1";
            this.ucGraph1.Size = new System.Drawing.Size(1189, 710);
            this.ucGraph1.TabIndex = 0;
            // 
            // frm_Graph_Drawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 710);
            this.Controls.Add(this.ucGraph1);
            this.Name = "frm_Graph_Drawer";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucGraph.ucGraph ucGraph1;
    }
}

