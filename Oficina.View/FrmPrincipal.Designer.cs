using System;
using System.Drawing;
using System.Windows.Forms;

namespace Oficina.View
{
    partial class FrmPrincipal : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            btnRelatorio = new Button();
            btnServico = new Button();
            btnVeiculo = new Button();
            btnCliente = new Button();
            pnlOficina = new Panel();
            pictureBox1 = new PictureBox();
            pnlOficina.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnRelatorio
            // 
            btnRelatorio.Image = Properties.Resources.Captura_de_tela_2025_11_06_181943;
            btnRelatorio.Location = new Point(441, 301);
            btnRelatorio.Margin = new Padding(3, 4, 3, 4);
            btnRelatorio.Name = "btnRelatorio";
            btnRelatorio.Size = new Size(227, 53);
            btnRelatorio.TabIndex = 4;
            btnRelatorio.UseVisualStyleBackColor = true;
            btnRelatorio.Click += btnRelatorio_Click;
            // 
            // btnServico
            // 
            btnServico.Image = Properties.Resources.Captura_de_tela_2025_11_06_181533;
            btnServico.Location = new Point(198, 301);
            btnServico.Margin = new Padding(3, 4, 3, 4);
            btnServico.Name = "btnServico";
            btnServico.Size = new Size(237, 53);
            btnServico.TabIndex = 3;
            btnServico.UseVisualStyleBackColor = true;
            btnServico.Click += btnServico_Click;
            // 
            // btnVeiculo
            // 
            btnVeiculo.Image = Properties.Resources.Captura_de_tela_2025_11_06_181523;
            btnVeiculo.Location = new Point(441, 256);
            btnVeiculo.Margin = new Padding(3, 4, 3, 4);
            btnVeiculo.Name = "btnVeiculo";
            btnVeiculo.Size = new Size(227, 47);
            btnVeiculo.TabIndex = 2;
            btnVeiculo.UseVisualStyleBackColor = true;
            btnVeiculo.Click += btnVeiculo_Click;
            // 
            // btnCliente
            // 
            btnCliente.BackColor = Color.Transparent;
            btnCliente.Image = (Image)resources.GetObject("btnCliente.Image");
            btnCliente.Location = new Point(198, 256);
            btnCliente.Margin = new Padding(3, 4, 3, 4);
            btnCliente.Name = "btnCliente";
            btnCliente.Size = new Size(237, 47);
            btnCliente.TabIndex = 1;
            btnCliente.UseVisualStyleBackColor = false;
            btnCliente.Click += btnCliente_Click;
            // 
            // pnlOficina
            // 
            pnlOficina.Anchor = AnchorStyles.Top;
            pnlOficina.BackColor = SystemColors.ControlLight;
            pnlOficina.BorderStyle = BorderStyle.FixedSingle;
            pnlOficina.Controls.Add(btnCliente);
            pnlOficina.Controls.Add(btnVeiculo);
            pnlOficina.Controls.Add(btnServico);
            pnlOficina.Controls.Add(btnRelatorio);
            pnlOficina.Controls.Add(pictureBox1);
            pnlOficina.Location = new Point(0, -8);
            pnlOficina.Margin = new Padding(3, 4, 3, 4);
            pnlOficina.Name = "pnlOficina";
            pnlOficina.Size = new Size(827, 539);
            pnlOficina.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-12, -62);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(892, 652);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 533);
            Controls.Add(pnlOficina);
            IsMdiContainer = true;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "FrmPrincipal";
            Text = "Oficina - Principal";
            pnlOficina.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnRelatorio;
        private Button btnServico;
        private Button btnVeiculo;
        private Button btnCliente;
        private Panel pnlOficina;
        private PictureBox pictureBox1;
    }
}
