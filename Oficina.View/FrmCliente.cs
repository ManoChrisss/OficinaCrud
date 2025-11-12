using System;
using System.Drawing;
using System.Windows.Forms;
using Oficina.View.Controllers;
using oficina.Models;

namespace Oficina.View
{
    public class FrmCliente : Form
    {
        private readonly CCliente _Control;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblTelefone;
        private TextBox txtTelefone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnIncluir;
        private Button btnAlterar;
        private Button btnExcluir;
        private Button btnConsultar;
        private DataGridView dgvClientes;

        private int clienteSelecionadoId = -1;

        public FrmCliente()
        {
            _Control = new CCliente();
            InitializeCustomComponent();
            this.Load += FrmCliente_Load;
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            try
            {
                dgvClientes.AutoGenerateColumns = true;
                dgvClientes.DataSource = _Control.SelecionarTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeCustomComponent()
        {
            lblNome = new Label();
            txtNome = new TextBox();
            lblTelefone = new Label();
            txtTelefone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            btnIncluir = new Button();
            btnAlterar = new Button();
            btnExcluir = new Button();
            btnConsultar = new Button();
            dgvClientes = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();

            // lblNome
            lblNome.Location = new Point(12, 12);
            lblNome.Size = new Size(80, 23);
            lblNome.Text = "Nome:";

            // txtNome
            txtNome.Location = new Point(100, 12);
            txtNome.Size = new Size(320, 27);

            // lblTelefone
            lblTelefone.Location = new Point(12, 44);
            lblTelefone.Size = new Size(80, 23);
            lblTelefone.Text = "Telefone:";

            // txtTelefone
            txtTelefone.Location = new Point(100, 44);
            txtTelefone.Size = new Size(200, 27);

            // lblEmail
            lblEmail.Location = new Point(12, 76);
            lblEmail.Size = new Size(80, 23);
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.Location = new Point(100, 76);
            txtEmail.Size = new Size(320, 27);

            // btnIncluir
            btnIncluir.Location = new Point(440, 12);
            btnIncluir.Size = new Size(90, 30);
            btnIncluir.Text = "Incluir";
            btnIncluir.Click += btnIncluir_Click;

            // btnAlterar
            btnAlterar.Location = new Point(540, 12);
            btnAlterar.Size = new Size(90, 30);
            btnAlterar.Text = "Alterar";
            btnAlterar.Click += btnAlterar_Click;

            // btnExcluir
            btnExcluir.Location = new Point(640, 12);
            btnExcluir.Size = new Size(90, 30);
            btnExcluir.Text = "Excluir";
            btnExcluir.Click += btnExcluir_Click;

            // btnConsultar
            btnConsultar.Location = new Point(740, 12);
            btnConsultar.Size = new Size(90, 30);
            btnConsultar.Text = "Consultar";
            btnConsultar.Click += btnConsultar_Click;

            // dgvClientes
            dgvClientes.ColumnHeadersHeight = 29;
            dgvClientes.Location = new Point(12, 120);
            dgvClientes.Size = new Size(906, 432);
            dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvClientes.CellClick += dgvClientes_CellClick;

            // Adiciona controles
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblTelefone);
            Controls.Add(txtTelefone);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(btnIncluir);
            Controls.Add(btnAlterar);
            Controls.Add(btnExcluir);
            Controls.Add(btnConsultar);
            Controls.Add(dgvClientes);

            ClientSize = new Size(930, 564);
            Font = new Font("Segoe UI", 9F);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Clientes";

            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome do cliente não pode ser vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente oCliente = new Cliente
            {
                Nome = txtNome.Text,
                Telefone = txtTelefone.Text,
                Email = txtEmail.Text
            };

            try
            {
                _Control.Incluir(oCliente);
                CarregaGrid();
                LimparCampos();
                MessageBox.Show("Cliente incluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao incluir cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (clienteSelecionadoId == -1)
            {
                MessageBox.Show("Selecione um cliente para alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente oCliente = _Control.Selecionar(clienteSelecionadoId);
            if (oCliente != null)
            {
                oCliente.Nome = txtNome.Text;
                oCliente.Telefone = txtTelefone.Text;
                oCliente.Email = txtEmail.Text;

                _Control.Alterar(oCliente);
                CarregaGrid();
                LimparCampos();
                MessageBox.Show("Cliente alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (clienteSelecionadoId == -1)
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Deseja realmente excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                bool sucesso = _Control.Excluir(clienteSelecionadoId);

                if (sucesso)
                {
                    CarregaGrid();
                    LimparCampos();
                    MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não é possível excluir este cliente, pois ele possui veículos cadastrados.",
                                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvClientes.Rows[e.RowIndex];

            txtNome.Text = row.Cells["Nome"].Value?.ToString();
            txtTelefone.Text = row.Cells["Telefone"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();

            if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int id))
            {
                clienteSelecionadoId = id;
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            clienteSelecionadoId = -1;
            txtNome.Focus();
        }
    }
}
