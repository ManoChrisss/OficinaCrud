using oficina.Models;
using oficina.Models.Repositories;
using Oficina.View.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Oficina.View
{
    public class FrmRelatorios : Form
    {
        private ComboBox cmbCliente;
        private ComboBox cmbVeiculo;
        private ComboBox cmbServico;
        private TextBox txtObservacoes;
        private DateTimePicker dtpData;
        private Button btnAdicionar;
        private Button btnAlterarObs;
        private Button btnExcluir;
        private DataGridView dgvRelatorio;

        private COrdemServico _control;
        private List<Cliente> clientes;
        private List<Veiculo> veiculos;
        private List<Servico> servicos;

        private int ordemSelecionadaId = -1;

        public FrmRelatorios()
        {
            _control = new COrdemServico();
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using var clienteRepo = new repositoryOficina();
            clientes = clienteRepo.SelecionarTodos();
            cmbCliente.DataSource = clientes;
            cmbCliente.DisplayMember = "Nome";
            cmbCliente.ValueMember = "ClienteId";

            using var veiculoRepo = new repositoryVeiculo();
            veiculos = veiculoRepo.SelecionarTodos();
            cmbVeiculo.DataSource = veiculos;
            cmbVeiculo.DisplayMember = "Placa"; // ou Marca + Modelo
            cmbVeiculo.ValueMember = "VeiculoId";

            using var servicoRepo = new ServicoRepository();
            servicos = servicoRepo.SelecionarTodos();
            cmbServico.DataSource = servicos;
            cmbServico.DisplayMember = "Descricao";
            cmbServico.ValueMember = "ServicoId";

            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            dgvRelatorio.DataSource = null;
            dgvRelatorio.DataSource = _control.SelecionarRelatorio();
        }

        private void InitializeComponent()
        {
            cmbCliente = new ComboBox { Location = new Point(20, 20), Width = 200 };
            cmbVeiculo = new ComboBox { Location = new Point(240, 20), Width = 200 };
            cmbServico = new ComboBox { Location = new Point(460, 20), Width = 200 };
            dtpData = new DateTimePicker { Location = new Point(680, 20), Width = 150 };
            txtObservacoes = new TextBox { Location = new Point(20, 60), Width = 810 };
            btnAdicionar = new Button { Location = new Point(20, 100), Text = "Adicionar Ordem", Width = 150, Height = 50 };
            btnAlterarObs = new Button { Location = new Point(180, 100), Text = "Alterar Observação", Width = 160, Height = 50 };
            btnExcluir = new Button { Location = new Point(360, 100), Text = "Excluir Ordem", Width = 120, Height = 50 };

            btnAdicionar.Click += BtnAdicionar_Click;
            btnAlterarObs.Click += BtnAlterarObs_Click;
            btnExcluir.Click += BtnExcluir_Click;

            dgvRelatorio = new DataGridView
            {
                Location = new Point(20, 150),
                Size = new Size(810, 400),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvRelatorio.CellClick += DgvRelatorio_CellClick;

            Controls.Add(cmbCliente);
            Controls.Add(cmbVeiculo);
            Controls.Add(cmbServico);
            Controls.Add(dtpData);
            Controls.Add(txtObservacoes);
            Controls.Add(btnAdicionar);
            Controls.Add(btnAlterarObs);
            Controls.Add(btnExcluir);
            Controls.Add(dgvRelatorio);

            ClientSize = new Size(860, 580);
            Text = "Relatório de Ordens de Serviço";
        }

        private void DgvRelatorio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvRelatorio.Rows[e.RowIndex];
            ordemSelecionadaId = Convert.ToInt32(row.Cells["OrdemId"].Value);
            txtObservacoes.Text = row.Cells["Observacoes"].Value?.ToString();
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedItem == null || cmbVeiculo.SelectedItem == null || cmbServico.SelectedItem == null)
            {
                MessageBox.Show("Selecione Cliente, Veículo e Serviço.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OrdensServico ordem = new OrdensServico
            {
                ClienteId = (int)cmbCliente.SelectedValue,
                VeiculoId = (int)cmbVeiculo.SelectedValue,
                ServicoId = (int)cmbServico.SelectedValue,
                DataServico = DateOnly.FromDateTime(dtpData.Value),
                Observacoes = txtObservacoes.Text
            };

            _control.Incluir(ordem);
            AtualizarGrid();
            txtObservacoes.Clear();
        }

        private void BtnAlterarObs_Click(object sender, EventArgs e)
        {
            if (ordemSelecionadaId == -1)
            {
                MessageBox.Show("Selecione uma ordem para alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _control.AlterarObservacao(ordemSelecionadaId, txtObservacoes.Text);
            AtualizarGrid();
            txtObservacoes.Clear();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (ordemSelecionadaId == -1)
            {
                MessageBox.Show("Selecione uma ordem para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Deseja realmente excluir esta ordem?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                bool sucesso = _control.Excluir(ordemSelecionadaId);
                if (!sucesso)
                    MessageBox.Show("Não é possível excluir esta ordem, pois existem dependências no banco.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                AtualizarGrid();
                txtObservacoes.Clear();
                ordemSelecionadaId = -1;
            }
        }
    }
}
