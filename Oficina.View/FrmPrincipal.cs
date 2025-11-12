using System;
using System.Drawing;
using System.Windows.Forms;
using Oficina.View.Controllers;
using oficina.Models;

namespace Oficina.View
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCliente frm = new FrmCliente();
            frm.FormClosed += (s, args) => this.Show(); // abre o principal dnv pq n eh mdi
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void btnVeiculo_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmVeiculo frm = new FrmVeiculo();
            frm.FormClosed += (s, args) => this.Show();
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void btnServico_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmServico frm = new FrmServico();
            frm.FormClosed += (s, args) => this.Show();
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmRelatorios frm = new FrmRelatorios();
            frm.FormClosed += (s, args) => this.Show();
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
    }
}
