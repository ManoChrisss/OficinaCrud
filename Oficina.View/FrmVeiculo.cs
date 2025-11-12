using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Oficina.View.Controllers;
using oficina.Models;

namespace Oficina.View
{
    public partial class FrmVeiculo : Form
    {
        // --- Campos de interface ---
        private Label lblCliente, lblMarca, lblModelo, lblAno, lblPlaca;
        private ComboBox cmbCliente;
        private TextBox txtMarca, txtModelo, txtAno, txtPlaca;
        private Button btnIncluir, btnAlterar, btnExcluir, btnConsultar;
        private DataGridView dgvVeiculos;
        private CVeiculo _Control = new CVeiculo();

        private int veiculoSelecionadoId = -1;

        public FrmVeiculo()
        {
            BuildUi();
            this.Load += FrmVeiculo_Load;
        }

        private void FrmVeiculo_Load(object sender, EventArgs e)
        {
            CarregarClientes();
            CarregarVeiculos();
        }

        private void BuildUi()
        {
            // --- Label e ComboBox de Cliente ---
            lblCliente = new Label { Text = "Cliente:", AutoSize = true, Location = new Point(20, 18), Font = new Font("Segoe UI", 10F, FontStyle.Regular) };
            cmbCliente = new ComboBox { Location = new Point(110, 14), Size = new Size(480, 26), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };

            // --- Campos do veículo ---
            lblMarca = new Label { Text = "Marca:", Location = new Point(20, 58), AutoSize = true };
            txtMarca = new TextBox { Location = new Point(110, 54), Size = new Size(300, 26) };

            lblModelo = new Label { Text = "Modelo:", Location = new Point(20, 96), AutoSize = true };
            txtModelo = new TextBox { Location = new Point(110, 92), Size = new Size(300, 26) };

            lblAno = new Label { Text = "Ano:", Location = new Point(20, 134), AutoSize = true };
            txtAno = new TextBox { Location = new Point(110, 130), Size = new Size(120, 26) };

            lblPlaca = new Label { Text = "Placa:", Location = new Point(250, 134), AutoSize = true };
            txtPlaca = new TextBox { Location = new Point(300, 130), Size = new Size(110, 26) };

            // --- Botões ---
            btnIncluir = new Button { Text = "Incluir", Location = new Point(110, 178), Size = new Size(100, 34) };
            btnAlterar = new Button { Text = "Alterar", Location = new Point(220, 178), Size = new Size(100, 34) };
            btnExcluir = new Button { Text = "Excluir", Location = new Point(330, 178), Size = new Size(100, 34) };
            btnConsultar = new Button { Text = "Consultar", Location = new Point(440, 178), Size = new Size(110, 34) };

            btnIncluir.Click += BtnIncluir_Click;
            btnAlterar.Click += BtnAlterar_Click;
            btnExcluir.Click += BtnExcluir_Click;
            btnConsultar.Click += BtnConsultar_Click;

            // --- DataGridView ---
            dgvVeiculos = new DataGridView
            {
                Location = new Point(20, 230),
                Size = new Size(900, 400),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvVeiculos.CellClick += DgvVeiculos_CellClick;

            // --- Adiciona controles ao formulário ---
            Controls.Add(lblCliente);
            Controls.Add(cmbCliente);
            Controls.Add(lblMarca);
            Controls.Add(txtMarca);
            Controls.Add(lblModelo);
            Controls.Add(txtModelo);
            Controls.Add(lblAno);
            Controls.Add(txtAno);
            Controls.Add(lblPlaca);
            Controls.Add(txtPlaca);
            Controls.Add(btnIncluir);
            Controls.Add(btnAlterar);
            Controls.Add(btnExcluir);
            Controls.Add(btnConsultar);
            Controls.Add(dgvVeiculos);

            ClientSize = new Size(960, 650);
            Font = new Font("Segoe UI", 9F);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadastro de Veículos";
        }

        // --- Eventos dos botões ---
        private void BtnIncluir_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            int? ano = int.TryParse(txtAno.Text, out int parsedAno) ? parsedAno : null;

            Veiculo v = new Veiculo
            {
                ClienteId = (int)cmbCliente.SelectedValue,
                Marca = txtMarca.Text,
                Modelo = txtModelo.Text,
                Ano = ano,
                Placa = txtPlaca.Text
            };

            try
            {
                _Control.Incluir(v);
                CarregarVeiculos();
                LimparCampos();
                MessageBox.Show("Veículo incluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao incluir veículo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            if (veiculoSelecionadoId == -1)
            {
                MessageBox.Show("Selecione um veículo para alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            int? ano = int.TryParse(txtAno.Text, out int parsedAno) ? parsedAno : null;

            Veiculo v = new Veiculo
            {
                VeiculoId = veiculoSelecionadoId,
                ClienteId = (int)cmbCliente.SelectedValue,
                Marca = txtMarca.Text,
                Modelo = txtModelo.Text,
                Ano = ano,
                Placa = txtPlaca.Text
            };

            try
            {
                _Control.Alterar(v);
                CarregarVeiculos();
                LimparCampos();
                MessageBox.Show("Veículo alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar veículo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (veiculoSelecionadoId == -1)
            {
                MessageBox.Show("Selecione um veículo para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Deseja realmente excluir este veículo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                bool sucesso = _Control.Excluir(veiculoSelecionadoId);

                if (sucesso)
                {
                    CarregarVeiculos();
                    LimparCampos();
                    MessageBox.Show("Veículo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não é possível excluir este veículo, pois ele possui ordens de serviço associadas.",
                                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            CarregarVeiculos();
        }

        // --- Carregar dados ---
        private void CarregarClientes()
        {
            try
            {
                using var controller = new CVeiculo();
                cmbCliente.DataSource = controller.SelecionarTodosClientes(); // <-- Alteração aqui
                cmbCliente.DisplayMember = "Nome";
                cmbCliente.ValueMember = "ClienteId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
            }
        }

        private void CarregarVeiculos()
        {
            try
            {
                var lista = _Control.SelecionarTodos()
                    .Select(v => new
                    {
                        v.VeiculoId,
                        v.Marca,
                        v.Modelo,
                        v.Ano,
                        v.Placa,
                        Cliente = v.Cliente.Nome
                    })
                    .ToList();

                dgvVeiculos.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar veículos: " + ex.Message);
            }
        }

        private void DgvVeiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvVeiculos.Rows[e.RowIndex];
            veiculoSelecionadoId = (int)row.Cells["VeiculoId"].Value;

            txtMarca.Text = row.Cells["Marca"].Value?.ToString();
            txtModelo.Text = row.Cells["Modelo"].Value?.ToString();
            txtAno.Text = row.Cells["Ano"].Value?.ToString();
            txtPlaca.Text = row.Cells["Placa"].Value?.ToString();

            string nomeCliente = row.Cells["Cliente"].Value?.ToString();
            cmbCliente.SelectedIndex = cmbCliente.FindString(nomeCliente);
        }

        private void LimparCampos()
        {
            txtMarca.Clear();
            txtModelo.Clear();
            txtAno.Clear();
            txtPlaca.Clear();
            veiculoSelecionadoId = -1;
            cmbCliente.SelectedIndex = -1;
        }

        private bool ValidarCampos()
        {
            if (cmbCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um cliente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                MessageBox.Show("Informe a marca do veículo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("Informe o modelo do veículo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
