using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AULA01.WindowsForms
{
    public partial class Frm04 : Form
    {
        public Frm04()
        {
            MontarInterface();
        }

        private void MontarInterface()
        {
            // === CONFIGURAÇÃO DA JANELA ===
            this.Text = "Ordens de Serviço";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // === LABELS E COMBOBOXES ===
            Label lblCliente = new Label()
            {
                Text = "Cliente:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            ComboBox cmbCliente = new ComboBox()
            {
                Name = "cmbCliente",
                Location = new Point(120, 16),
                Size = new Size(560, 26),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Label lblVeiculo = new Label()
            {
                Text = "Veículo:",
                Location = new Point(20, 60),
                AutoSize = true
            };
            ComboBox cmbVeiculo = new ComboBox()
            {
                Name = "cmbVeiculo",
                Location = new Point(120, 56),
                Size = new Size(560, 26),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Label lblServico = new Label()
            {
                Text = "Serviço:",
                Location = new Point(20, 100),
                AutoSize = true
            };
            ComboBox cmbServico = new ComboBox()
            {
                Name = "cmbServico",
                Location = new Point(120, 96),
                Size = new Size(360, 26),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Label lblDataServico = new Label()
            {
                Text = "Data do Serviço:",
                Location = new Point(500, 100),
                AutoSize = true
            };
            DateTimePicker dtpDataServico = new DateTimePicker()
            {
                Name = "dtpDataServico",
                Location = new Point(620, 96),
                Size = new Size(120, 26),
                Format = DateTimePickerFormat.Short
            };

            Label lblObservacoes = new Label()
            {
                Text = "Observações:",
                Location = new Point(20, 140),
                AutoSize = true
            };
            TextBox txtObservacoes = new TextBox()
            {
                Name = "txtObservacoes",
                Location = new Point(120, 136),
                Size = new Size(620, 80),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // === BOTÕES ===
            Button btnIncluir = new Button()
            {
                Text = "Incluir",
                Location = new Point(120, 230),
                Size = new Size(100, 34)
            };
            Button btnAlterar = new Button()
            {
                Text = "Alterar",
                Location = new Point(240, 230),
                Size = new Size(100, 34)
            };
            Button btnExcluir = new Button()
            {
                Text = "Excluir",
                Location = new Point(360, 230),
                Size = new Size(100, 34)
            };
            Button btnConsultar = new Button()
            {
                Text = "Consultar",
                Location = new Point(480, 230),
                Size = new Size(110, 34)
            };

            // === DATAGRIDVIEW ===
            DataGridView dgvOrdens = new DataGridView()
            {
                Name = "dgvOrdens",
                Location = new Point(20, 280),
                Size = new Size(840, 280),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvOrdens.Columns.Add("OrdemID", "OrdemID");
            dgvOrdens.Columns["OrdemID"].Visible = false;
            dgvOrdens.Columns.Add("Cliente", "Cliente");
            dgvOrdens.Columns.Add("Veiculo", "Veículo");
            dgvOrdens.Columns.Add("Servico", "Serviço");
            dgvOrdens.Columns.Add("DataServico", "Data");
            dgvOrdens.Columns.Add("Observacoes", "Observações");

            // === ADICIONAR TUDO NA TELA ===
            this.Controls.Add(lblCliente);
            this.Controls.Add(cmbCliente);
            this.Controls.Add(lblVeiculo);
            this.Controls.Add(cmbVeiculo);
            this.Controls.Add(lblServico);
            this.Controls.Add(cmbServico);
            this.Controls.Add(lblDataServico);
            this.Controls.Add(dtpDataServico);
            this.Controls.Add(lblObservacoes);
            this.Controls.Add(txtObservacoes);
            this.Controls.Add(btnIncluir);
            this.Controls.Add(btnAlterar);
            this.Controls.Add(btnExcluir);
            this.Controls.Add(btnConsultar);
            this.Controls.Add(dgvOrdens);
        }
    }
}
