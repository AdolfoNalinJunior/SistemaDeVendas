using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamadaNegocio;

namespace CamadaApresentacao
{
    public partial class frmProdutos : Form
    {
        public bool Verificacao { get; set; }
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
            this.ComboArpesentacao();
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
            //this.cbApresentacao.Text = string.Empty;
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
        private void OcultarColunas()
        {
            this.dataListar.Columns[0].Visible = false;
            this.dataListar.Columns[1].Visible = false;
            this.dataListar.Columns[2].Visible = false;
            this.dataListar.Columns[6].Visible = false;
            
        }

        // MOstrar no DataGrid
        private void Mostrar()
        {
            this.dataListar.DataSource = NProduto.Mostrar();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
            this.OcultarColunas();
        }

        // Buscar Pelo nome
        private void BuscarNome()
        {
            this.dataListar.DataSource = NProduto.BuscarNome(this.txtBuscar.Text);
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
            this.OcultarColunas();
        }

        private void ComboArpesentacao()
        {
            cbApresentacao.DataSource = NApresentacao.Mostrar();
            cbApresentacao.ValueMember = "idapresentacao";
            cbApresentacao.DisplayMember = "nome";
            /* Os objects que estão sendo chamados neste métodos fazem parte da CamadaDado
             * que remete os valores que estão dentro da camada 
             */
        }

        private void frmProdutos_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(true);
            this.Botoes();
        }

        /// <summary>
        /// Esse método é usado para salvar a imagem no banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCarregarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Parametro para salvar a imagem e ajusta-la na picturebox
                this.pxImagem.SizeMode = PictureBoxSizeMode.StretchImage;
                //Para pegar o nome do ar2quivo e armazenar-lo no banco de dados.
                this.pxImagem.Image= Image.FromFile(dialog.FileName);
            }
        }

        /// <summary>
        /// Esse método é para limpar o compo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimparImagem_Click(object sender, EventArgs e)
        {
            // Traz ela para estado atual, para que seja limpa.
            this.pxImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            // Traz a imagem que simboliza que está vazia, para subistituir a original  
            this.pxImagem.Image = global::CamadaApresentacao.Properties.Resources.NÃO_HÁ_IMAGEM;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.Botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtNome.Focus();
        }

        /// <summary>
        /// Método para salvar a insersão e as edições que podem ser feitas no objeto protudo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                Verificacao = this.txtNome.Text == string.Empty || txtIdCategoria.Text == string.Empty || this.txtCodigo.Text == String.Empty;
                if (Verificacao)
                {
                    MensagemErro("Preencha todos os campos!! ");
                    errorIcone.SetError(txtIdCategoria, "Insira a Categoria! ");
                    errorIcone.SetError(txtCodigo, "Insira o Código! ");
                    errorIcone.SetError(txtNome, "Insira o Nome! ");
                }
                else
                {
                    MemoryStream ms = new MemoryStream(); // lugar na mémoria que
                    this.pxImagem.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    // no método save ele vai separar um local na memoria para que a imagem seja salva
                    byte[] imagem = ms.GetBuffer(); // GetBuffer é um método que salva o nome e o local onde a imagens está.
                    if (this.eNovo)
                    {
                        resp = NProduto.Inserir(txtNome.Text.Trim(), txtDescricao.Text.Trim(),
                            txtCodigo.Text, imagem, Convert.ToInt32(this.txtIdCategoria.Text), Convert.ToInt32(this.cbApresentacao.SelectedValue));
                    }
                    else
                    {
                        resp = NProduto.Editar(Convert.ToInt32(txtId.Text), txtNome.Text.Trim(),
                            txtDescricao.Text.Trim(), txtCodigo.Text.Trim(), imagem, Convert.ToInt32(this.txtIdCategoria.Text),
                            Convert.ToInt32(this.cbApresentacao.SelectedValue));
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
                this.Limpar();
                this.Mostrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtId.Text.Equals(""))
            {
                this.MensagemErro("Selecione um registro para Editar.");
            }
            else
            {
                this.eEditar = true;
                this.Botoes();
                this.Habilitar(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eEditar = false;
            this.eNovo = false;
            this.Botoes();
            this.Habilitar(false);
            this.Limpar();
        }

        private void chkDeletar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeletar.Checked)
            {
                this.dataListar.Columns[0].Visible = true;
            }
            else
            {
                this.dataListar.Columns[0].Visible = false;
            }
        }

        private void dataListar_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["idapresentacao"].Value);
            this.txtNome.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["nome"].Value);
            this.txtDescricao.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["descricao"].Value);
            this.tabControl1.SelectedIndex = 1;
        }
    }
}
