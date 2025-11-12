using System;
using System.Drawing;
using System.Windows.Forms;

namespace Oficina.View
{
    public partial class FrmVeiculo : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmVeiculo
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(914, 600);
            this.Margin = new Padding(3, 4, 3, 4);
            this.Name = "FrmVeiculo";
            this.Text = "FrmVeiculo";
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);
        }
    }
}
