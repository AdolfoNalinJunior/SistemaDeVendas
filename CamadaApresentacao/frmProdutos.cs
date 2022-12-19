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
    /// <summary>
    /// frmProduto é a tela da configuração dos produtos, sendo essas: 
    /// </summary>
    public partial class frmProdutos : Form
    {
        /// <summary>
        /// Parametro de verificação de validade dos botões 
        /// </summary>
        /// <param name="Verificacao"></param>
        /// <param name="eNovo">Parametro de vericação do <paramref name="btnNovo"</param>
        /// <param name="eEditar">Parametro de verificação do <paramref name="btnEdidar"</param>
        public bool Verificacao { get; set; }
        bool eNovo = false;
        bool eEditar = false;

        /// <summary>
        /// Construtor com as mesagens de ToolTip, para orientar o usuário na inicialisão do sistema
        /// </summary>
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

        /// <summary>
        /// Método de menssagem de informaçãp, menssagem do tipo MessageBox com icone de Informação
        /// </summary>
        /// <param name="mensagem">Menssagem que vai aparecer na MessageBox</param>
        private void MensagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Método de menssagem erro, menssagem do tipo MassageBox com icone de Error 
        /// </summary>
        /// <param name="mensagem">Menssagem que vai aparecer no MessageBox</param>
        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        ///  Método privado para realizar a limpeza dos campos
        /// </summary>
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

        /// <summary>
        /// Método de habilitação dos botões
        /// </summary>
        /// <param name="valor">Valor de true or false</param>
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

        /// <summary>
        /// Método de verificação dos botões <paramref name="btnNovo"> e <paramref name="btnEditar"
        /// </summary>
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
        
        /// <summary>
        /// Método que realiza a seleção das colunas do dataGrid
        /// </summary>
        private void OcultarColunas()
        {
            this.dataListar.Columns[0].Visible = false;
            this.dataListar.Columns[1].Visible = false;
            this.dataListar.Columns[2].Visible = false;
            this.dataListar.Columns[6].Visible = false;
            
        }

        /// <summary>
        /// Método que tráz os dados da tabela 
        /// </summary>
        private void Mostrar()
        {
            this.dataListar.DataSource = NProduto.Mostrar();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
            this.OcultarColunas();
        }

        /// <summary>
        /// Método que realiza a busca do nome do produto baseado no parametro passado no <paramref name="txtBuscar">Caixa de texto que recebe os valores de busca</paramref>
        /// </summary>
        private void BuscarNome()
        {
            this.dataListar.DataSource = NProduto.BuscarNome(this.txtBuscar.Text);
            this.lblTotal.Text = $"Total de Registros: {Convert.ToString(dataListar.Rows.Count)}";
            this.OcultarColunas();
        }

        /// <summary>
        /// Combo da apresentação na tela do produto
        /// </summary>
        private void ComboArpesentacao()
        {
            cbApresentacao.DataSource = NApresentacao.Mostrar();
            cbApresentacao.ValueMember = "idapresentacao";
            cbApresentacao.DisplayMember = "nome";
            /* Os objects que estão sendo chamados neste métodos fazem parte da CamadaDado
             * que remete os valores que estão dentro da camada 
             */
        }

        /// <summary>
        /// Método de inicialicão da tela, indicando o local da tela onde ele ira aparecer e quais os métodos que vão iniciar com a tela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            this.dataListar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataListar_DoubleClick);
            this.txtId.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["idproduto"].Value);
            this.txtNome.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["nome"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["codigo"].Value);
            this.txtDescricao.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["descricao"].Value);
            byte[] imageBuffer = (byte[]) this.dataListar.CurrentRow.Cells["imagem"].Value;
            MemoryStream ms = new MemoryStream(imageBuffer);
            /*
             * ms é o espaço na memeoria que a imagem vai ser gurdada e o System.drawing.Imaging.ImagemFormat.png 
             * é a definição de qual o tipo de formato que vai ser gurdaddo
             */
            this.pxImagem.Image = Image.FromStream(ms);
            this.pxImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            this.txtIdCategoria.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["idcategoria"].Value);
            this.txtNomeCategoria.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["Categoria"].Value);
            this.cbApresentacao.SelectedValue = Convert.ToString(this.dataListar.CurrentRow.Cells["idapresentacao"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void dataListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListar.Columns["Deletar"].Index)
            {
                // Validação check box Deletar
                DataGridViewCheckBoxCell ChkDeletar = (DataGridViewCheckBoxCell)dataListar.Rows[e.RowIndex].Cells["Deletar"];
                ChkDeletar.Value = !Convert.ToBoolean(ChkDeletar.Value);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcao;
                Opcao = MessageBox.Show("Realmente deseja apagar os Registros?", "Sistema Comercio", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcao == DialogResult.OK)
                {
                    string codigo;
                    string resp;

                    foreach (DataGridViewRow row in dataListar.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToString(row.Cells[1].Value);
                            resp = NProduto.Deletar(Convert.ToInt32(codigo));
                            if (resp.Equals("OK"))
                            {
                                this.MensagemOk("Registros excluido com sucesso.");
                            }
                            else
                            {
                                this.MensagemErro(resp);
                            }
                        }
                    }
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {

        }
    }
}
