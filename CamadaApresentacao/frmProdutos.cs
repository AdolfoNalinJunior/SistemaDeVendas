using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamadaNegocio;

namespace CamadaApresentacao
{
    public partial class frmProdutos : Form
    {
        bool eNovo = false;
        bool eEditar = false;

        public frmProdutos()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.lblNome, "Insira o nome do produto");
            this.ttMensagem.SetToolTip(this.pxImagem, "Insira uma imagem para produto");
            this.ttMensagem.SetToolTip(this.txtNomeCategoria, "Selecione a Categoria");
            this.ttMensagem.SetToolTip(this.cbApresentacao, "Selecione a apresentação do produto");
            this.txtIdCategoria.Enabled = false;
            this.txtNomeCategoria.Enabled = false;
        }

        // Mostrar mensagem de Confirmação
        private void MensagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Mostrar mensagem de Erro
        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Limpar
        private void Limpar()
        {
            this.txtNome.Text = string.Empty;
            this.txtId.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
            this.txtCodigo.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtNomeCategoria.Text = string.Empty;
            this.cbApresentacao.Text = string.Empty;
            this.pxImagem.Image = global::CamadaApresentacao.Properties.Resources.BUSCA;
        }

        // Habilitar text box
        private void Habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtId.ReadOnly = !valor;
            this.txtDescricao.ReadOnly = !valor;
            this.txtCodigo.ReadOnly = !valor;
            this.cbApresentacao.Enabled = valor;
            this.btnBuscarCategoria.Enabled = valor;
            this.btnCarregarImagem.Enabled = valor;
            this.btnLimparImagem.Enabled = valor;
        }

        // Habilitar Botões
        private void Botoes()
        {
            if (this.eNovo || this.eEditar)
            {
                Habilitar(true);
                this.btnNovo.Enabled = false;
                this.btnSalvar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        // Ocultar as Colunas do Grid
        private void OcultarColubas()
        {
            this.dataListar.Columns[0].Visible = false;
            this.dataListar.Columns[1].Visible = false;
            this.dataListar.Columns[6].Visible = false;
            this.dataListar.Columns[8].Visible = false;
        }

        // MOstrar no DataGrid
        private void Mostrar()
        {
            this.dataListar.DataSource = NProduto.Mostrar();
            this.OcultarColubas();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
        }

        // Buscar Pelo nome
        private void BuscarNome()
        {
            this.dataListar.DataSource = NProduto.BuscarNome(this.txtBuscar.Text);
            this.OcultarColubas();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
        }

        private void frmProdutos_Load(object sender, EventArgs e)
        {

        }
    }
}
