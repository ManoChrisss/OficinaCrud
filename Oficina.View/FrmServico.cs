using System;
using System.Drawing;
using System.Windows.Forms;
using Oficina.View.Controllers;
using oficina.Models;

namespace Oficina.View
{
    public class FrmServico : Form
    {
        private readonly CServico _Control;
        private Label lblDescricao;
        private TextBox txtDescricao;
        private Label lblPreco;
        private TextBox txtPreco;
        private Button btnIncluir;
        private Button btnAlterar;
        private Button btnExcluir;
        private Button btnConsultar;
        private DataGridView dgvServicos;

        private int servicoSelecionadoId = -1;

        public FrmServico()
        {
            _Control = new CServico();
            InitializeCustomComponent();
            this.Load += FrmServicos_Load;
        }

        private void FrmServicos_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            try
            {
                dgvServicos.AutoGenerateColumns = true;
                dgvServicos.DataSource = _Control.SelecionarTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeCustomComponent()
        {
            lblDescricao = new Label();
            txtDescricao = new TextBox();
            lblPreco = new Label();
            txtPreco = new TextBox();
            btnIncluir = new Button();
            btnAlterar = new Button();
            btnExcluir = new Button();
            btnConsultar = new Button();
            dgvServicos = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvServicos).BeginInit();
            SuspendLayout();

            // lblDescricao
            lblDescricao.Location = new Point(12, 12);
            lblDescricao.Size = new Size(80, 23);
            lblDescricao.Text = "Descrição:";

            // txtDescricao
            txtDescricao.Location = new Point(100, 12);
            txtDescricao.Size = new Size(320, 27);

            // lblPreco
            lblPreco.Location = new Point(12, 44);
            lblPreco.Size = new Size(80, 23);
            lblPreco.Text = "Preço:";

            // txtPreco
            txtPreco.Location = new Point(100, 44);
            txtPreco.Size = new Size(120, 27);

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

            // dgvServicos
            dgvServicos.ColumnHeadersHeight = 29;
            dgvServicos.Location = new Point(12, 90);
            dgvServicos.Size = new Size(906, 462);
            dgvServicos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvServicos.CellClick += dgvServicos_CellClick;

            // Adiciona controles
            Controls.Add(lblDescricao);
            Controls.Add(txtDescricao);
            Controls.Add(lblPreco);
            Controls.Add(txtPreco);
            Controls.Add(btnIncluir);
            Controls.Add(btnAlterar);
            Controls.Add(btnExcluir);
            Controls.Add(btnConsultar);
            Controls.Add(dgvServicos);

            ClientSize = new Size(930, 564);
            Font = new Font("Segoe UI", 9F);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Serviços";

            ((System.ComponentModel.ISupportInitialize)dgvServicos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("A descrição do serviço não pode ser vazia.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco))
            {
                MessageBox.Show("Preço inválido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Servico oServico = new Servico
            {
                Descricao = txtDescricao.Text,
                Preco = preco
            };

            try
            {
                _Control.Incluir(oServico);
                CarregaGrid();
                LimparCampos();
                MessageBox.Show("Serviço incluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao incluir serviço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (servicoSelecionadoId == -1)
            {
                MessageBox.Show("Selecione um serviço para alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Servico oServico = _Control.Selecionar(servicoSelecionadoId);
            if (oServico != null)
            {
                oServico.Descricao = txtDescricao.Text;
                oServico.Preco = decimal.TryParse(txtPreco.Text, out decimal preco) ? preco : 0;

                _Control.Alterar(oServico);
                CarregaGrid();
                LimparCampos();
                MessageBox.Show("Serviço alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (servicoSelecionadoId == -1)
            {
                MessageBox.Show("Selecione um serviço para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Deseja realmente excluir este serviço?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                bool sucesso = _Control.Excluir(servicoSelecionadoId);

                if (sucesso)
                {
                    CarregaGrid();
                    LimparCampos();
                    MessageBox.Show("Serviço excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não é possível excluir este serviço, pois ele possui ordens de serviço associadas.",
                                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void dgvServicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvServicos.Rows[e.RowIndex];

            txtDescricao.Text = row.Cells["Descricao"].Value?.ToString();
            txtPreco.Text = row.Cells["Preco"].Value?.ToString();

            if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int id))
            {
                servicoSelecionadoId = id;
            }
        }

        private void LimparCampos()
        {
            txtDescricao.Clear();
            txtPreco.Clear();
            servicoSelecionadoId = -1;
            txtDescricao.Focus();
        }
    }
}
