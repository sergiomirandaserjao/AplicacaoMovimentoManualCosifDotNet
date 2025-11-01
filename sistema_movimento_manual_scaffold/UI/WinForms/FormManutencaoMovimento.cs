using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Services;
using DAL.Implementations;
using Domain.Entities;

namespace UI.WinForms
{
    public partial class FormManutencaoMovimento : Form
    {
        private MovimentoService _service;

        public FormManutencaoMovimento()
        {
            InitializeComponent();
            _service = new MovimentoService(new MovimentoRepositoryAdo());
            LoadGrid().ConfigureAwait(false);
        }

        private async Task LoadGrid()
        {
            var list = await _service.Listar(null, null);
            // bind to DataGridView named dgvLista
            dgvLista.DataSource = list;
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            var mov = new MovimentoManual
            {
                DatMes = int.Parse(txtMes.Text),
                DatAno = int.Parse(txtAno.Text),
                CodProduto = cboProduto.SelectedValue.ToString(),
                CodCosif = cboCosif.SelectedValue.ToString(),
                ValValor = decimal.Parse(txtValor.Text),
                DesDescricao = txtDescricao.Text,
                CodUsuario = Environment.UserName
            };

            await _service.InserirMovimentoAsync(mov);
            await LoadGrid();
        }
    }
}
