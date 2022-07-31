using CamadaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamadaApresentacao
{
    public partial class frmCategoria : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;

        public frmCategoria()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome da categoria");
        }

        // Mostrar mensagem de Confirmação
        private void MensagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            /*
             * MessageBox é o nome da caixa de que vai ser lançado a mensagem\
             * Show é a classe que vai mosntar a estrutura com os detalhes
             * mesagem é o valor que vai ser passado, neste caso uma texto
             * MessagemBoxButton é o tipo de botão que vai aparecer para o usuário 
             * MessageBoxIncone é o tipo que é a informaçãom, neste caso é uma informação
             */
        }

        // Mostrar mensagem de Erro
        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Limpar
        private void limpar()
        {
            this.txtNome.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
        }

        // Habilitar text box
        private void Habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtIdCategoria.ReadOnly = !valor;
            this.txtDescricao.ReadOnly = !valor;
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
            // this.dataListar.Columns[1].Visible = false;
        }

        // MOstrar no DataGrid
        private void Mostrar()
        {
            this.dataListar.DataSource = NCategoria.Mostrar();
            this.OcultarColubas();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
        }

        // Buscar Pelo nome
        private void BuscarNome()
        {
            this.dataListar.DataSource = NCategoria.BuscarNome(this.txtBuscar.Text); 
            this.OcultarColubas();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
        }
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtNome.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos!! ");
                    errorIcone.SetError(txtNome, "Insira o nome! ");
                }
                else
                {
                    if (this.eNovo)
                    {
                        resp = NCategoria.Inserir(txtNome.Text.Trim(), txtDescricao.Text.Trim());
                    }
                    else
                    {
                        resp = NCategoria.Editar(Convert.ToInt32(txtIdCategoria.Text),txtNome.Text.Trim(), txtDescricao.Text.Trim());
                    }
                }

                if (resp.Equals("OK"))
                {
                    if (this.eNovo)
                    {
                        this.MensagemOk("Registro salvo com sucesso!! ");
                    }
                    else
                    {
                        this.MensagemOk("Registro editado com sucesso!! ");
                    }
                }
                else
                {
                    this.MensagemOk(resp);
                }

                this.eNovo = false;
                this.eEditar = false;
                this.Botoes();
                this.limpar();
                this.Mostrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);    
            }
        }

        // Mostrar tabControl Configurações de Itens
        private void frmCategoporia_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botoes();
      

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.Botoes();
            this.limpar();
            this.Habilitar(true);
            this.txtNome.Focus();
            // O método Focus é para chamar o cursor do mouse 
            this.txtIdCategoria.Enabled = false;
        }

        private void dataListar_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdCategoria.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["idCategoria"].Value);
            this.txtNome.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["nome"].Value);
            this.txtDescricao.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["descricao"].Value);
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtIdCategoria.Text.Equals(""))
            {
                this.MensagemErro("Selecione um registro para inserir!! ");
            }
                else
                {

                }
        }
    }
}
