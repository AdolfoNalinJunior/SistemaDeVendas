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
    public partial class frmApresentacao : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;

        public frmApresentacao()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome da Apresentação! ");
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
            this.txtIdApresentacao.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
        }

        // Habilitar text box
        private void Habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtIdApresentacao.ReadOnly = !valor;
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
            this.dataListar.DataSource = NApresentacao.Mostrar();
            this.OcultarColubas();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
        }

        // Buscar Pelo nome
        private void BuscarNome()
        {
            this.dataListar.DataSource = NApresentacao.BuscarNome(this.txtBuscar.Text);
            this.OcultarColubas();
            this.lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListar.Rows.Count);
        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        // Arquivo design
        private void frmApresentacao_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(true);
            this.Botoes();
        }

        // Botão Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarNome();
        }

        // textBox de Buscartexto 
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        // botão novo
        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.Botoes();
            this.Limpar();
            this.Habilitar(true);
            this.txtNome.Focus();
            this.txtIdApresentacao.Enabled = false;
        }

        // Botão Salvar
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
                        resp = NApresentacao.Inserir(txtNome.Text.Trim(), txtDescricao.Text.Trim());
                    }
                    else
                    {
                        resp = NApresentacao.Editar(Convert.ToInt32(txtIdApresentacao.Text), txtNome.Text.Trim(), txtDescricao.Text.Trim());
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

        // Dois clicks no dataListar 
        private void dataListar_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdApresentacao.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["idapresentacao"].Value);
            this.txtNome.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["nome"].Value);
            this.txtDescricao.Text = Convert.ToString(this.dataListar.CurrentRow.Cells["descricao"].Value);
            this.tabControl1.SelectedIndex = 1;
        }

        // Botão Editar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtIdApresentacao.Text.Equals(""))
            {
                this.MensagemErro("Selecione um registro para inserir.");
            }
            else
            {
                this.eEditar = true;
                this.Botoes();
                this.Habilitar(true);
            }
        }

        // Checkbox Deletar
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

        // Validação do checkBox para ativar a valitação do deletar 
        private void dataListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListar.Columns["Deletar"].Index)
            {
                // Validação check box Deletar
                DataGridViewCheckBoxCell ChkDeletar = (DataGridViewCheckBoxCell)dataListar.Rows[e.RowIndex].Cells["Deletar"];
                ChkDeletar.Value = !Convert.ToBoolean(ChkDeletar.Value);
            }
         }

        // Botão Deletar
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
                            resp = NApresentacao.Deletar(Convert.ToInt32(codigo));
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eEditar = false;
            this.eNovo = false;
            this.Botoes();
            this.Habilitar(false);
            this.Limpar();
        }

    }
}