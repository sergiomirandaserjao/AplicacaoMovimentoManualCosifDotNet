using System;
using System.Threading.Tasks;
using BLL.Services;
using DAL.Implementations;
using Domain.Entities;

namespace UI.WebForms
{
    public partial class MovimentoManutencao : System.Web.UI.Page
    {
        private MovimentoService _service;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_service == null) _service = new MovimentoService(new MovimentoRepositoryAdo());
            if (!IsPostBack) LoadGrid();
        }

        protected async void LoadGrid()
        {
            var lista = await _service.Listar(null, null);
            gvLista.DataSource = lista;
            gvLista.DataBind();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            txtMes.Enabled = true; txtAno.Enabled = true; ddlProduto.Enabled = true; ddlCosif.Enabled = true;
            txtValor.Enabled = true; txtDescricao.Enabled = true;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtMes.Text = txtAno.Text = txtValor.Text = txtDescricao.Text = string.Empty;
        }

        protected async void btnIncluir_Click(object sender, EventArgs e)
        {
            var mov = new MovimentoManual
            {
                DatMes = int.Parse(txtMes.Text),
                DatAno = int.Parse(txtAno.Text),
                CodProduto = ddlProduto.SelectedValue,
                CodCosif = ddlCosif.SelectedValue,
                ValValor = decimal.Parse(txtValor.Text),
                DesDescricao = txtDescricao.Text,
                CodUsuario = User?.Identity?.Name ?? Environment.UserName
            };

            await _service.InserirMovimentoAsync(mov);
            LoadGrid();
            txtMes.Enabled = txtAno.Enabled = ddlProduto.Enabled = ddlCosif.Enabled = txtValor.Enabled = txtDescricao.Enabled = false;
        }
    }
}
