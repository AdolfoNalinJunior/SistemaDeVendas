using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDado
{
    public class DProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Imagem { get; set; }
        public string Codigo { get; set; }
        public int IdCategoria { get; set; }
        public int IdApresentacao { get; set; }
        public string TextoBuscar { get; set; }

        public DProduto()
        {

        }

        public DProduto(int id, string nome, string descricao, byte[] imagem, string codigo, int idCodigo, int idAprencia, string textoBuscar)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Imagem = imagem;
            this.Codigo = codigo;
            this.IdApresentacao = idAprencia;
            this.IdCategoria = idCodigo;
            this.TextoBuscar = textoBuscar;
        }

        public void MenssagemOk(string menssagem)
        {
            MessageBox.Show(menssagem, "O sistema está funcionando!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // Método Inserir
        public string Inserir(DProduto produto)
        {
            SqlConnection SqlCon = new SqlConnection();
            string resp = "";
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand(); // objeto Comando Sql 
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinserir_produto";

                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parId = new SqlParameter();
                parId.ParameterName = "id";
                parId.SqlDbType = SqlDbType.Int;
                parId.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(parId);

                SqlParameter parCodigo = new SqlParameter();
                parCodigo.ParameterName = "codigo";
                parCodigo.SqlDbType = SqlDbType.VarChar;
                parCodigo.Size = 50;
                parCodigo.Value = produto.Nome;
                SqlCmd.Parameters.Add(parCodigo);

                SqlParameter parNome = new SqlParameter();
                parNome.ParameterName = "@nome";
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50; // Limite de Caracter
                parNome.Value = produto.Nome;
                SqlCmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter();
                parDescricao.ParameterName = "@descricao";
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 150;
                parDescricao.Value = produto.Descricao;
                SqlCmd.Parameters.Add(parDescricao);

                SqlParameter parImagem = new SqlParameter();
                parImagem.ParameterName = "@imagem";
                parImagem.SqlDbType = SqlDbType.Image;
                parImagem.Value = produto.Imagem;
                SqlCmd.Parameters.Add(parImagem);

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@idcategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = produto.IdCategoria;
                SqlCmd.Parameters.Add(parIdCategoria);

                SqlParameter parIdArpesentacao = new SqlParameter();
                parIdArpesentacao.ParameterName = "@idapresentacao";
                parIdArpesentacao.SqlDbType = SqlDbType.Int;
                parIdArpesentacao.Value = produto.IdCategoria;
                SqlCmd.Parameters.Add(parIdArpesentacao);

                // Verificação da inserção
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Registro não foi registrado";
                return resp;
            }
            catch (Exception e)
            {
                resp = e.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
                else
                {
                    resp = "A conexão não foi aberta, não é possível ser fechada!! ";
                }
            }
            return resp;
        }


        // Método Editar
        public string Editar(DProduto produto)
        {
            SqlConnection SqlCon = new SqlConnection();
            string resp = "";
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speditar_produto";

                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parId = new SqlParameter();
                parId.ParameterName = "id";
                parId.SqlDbType = SqlDbType.Int;
                parId.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(parId);

                SqlParameter parCodigo = new SqlParameter();
                parCodigo.ParameterName = "codigo";
                parCodigo.SqlDbType = SqlDbType.VarChar;
                parCodigo.Size = 50;
                parCodigo.Value = produto.Nome;
                SqlCmd.Parameters.Add(parCodigo);

                SqlParameter parNome = new SqlParameter();
                parNome.ParameterName = "@nome";
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50; // Limite de Caracter
                parNome.Value = produto.Nome;
                SqlCmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter();
                parDescricao.ParameterName = "@descricao";
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 150;
                parDescricao.Value = produto.Descricao;
                SqlCmd.Parameters.Add(parDescricao);

                SqlParameter parImagem = new SqlParameter();
                parImagem.ParameterName = "@imagem";
                parImagem.SqlDbType = SqlDbType.Image;
                parImagem.Value = produto.Imagem;
                SqlCmd.Parameters.Add(parImagem);

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@idcategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = produto.IdCategoria;
                SqlCmd.Parameters.Add(parIdCategoria);

                SqlParameter parIdArpesentacao = new SqlParameter();
                parIdArpesentacao.ParameterName = "@idapresentacao";
                parIdArpesentacao.SqlDbType = SqlDbType.Int;
                parIdArpesentacao.Value = produto.IdCategoria;
                SqlCmd.Parameters.Add(parIdArpesentacao);

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A edição não foi feita ";
                return resp;
            }
            catch (Exception e)
            {
                resp = e.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
                else
                {
                    resp = "A conexão não foi aberta, não é possível ser fechada!! ";
                }
            }
            return resp;
        }

        // Método de Excluir
        public string Excluir(DProduto produto)
        {
            SqlConnection SqlCon = new SqlConnection();
            string resp = "";
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdeletar_produto";

                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parId = new SqlParameter();
                parId.ParameterName = "id";
                parId.SqlDbType = SqlDbType.Int;
                parId.Value = produto.Id;
                SqlCmd.Parameters.Add(parId);

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A exclusão não foi feita ";
                return resp;
            }
            catch (Exception e)
            {
                resp = e.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
                else
                {
                    resp = "A conexão não foi aberta, não é possível ser fechada!! ";
                }
            }
            return resp;
        }

        // Método mostrar
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_produto";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                return dtResultado = null;
            }
            return dtResultado;
        }

        // Método BuscarNome
        public DataTable BuscarNome(DProduto produtos)
        {
            DataTable dtResultado = new DataTable("apresentacao");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();

                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_nome_produto";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textobuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 100;
                parTextoBuscar.Value = produtos.TextoBuscar;
                SqlCmd.Parameters.Add(parTextoBuscar);
                

                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(dtResultado);

                // É muito importante se atentar as ondem que vai ser realizado o código
            }
            catch (Exception ex)
            {
                return dtResultado = null;
            }

            return dtResultado;
        }
    }
}
