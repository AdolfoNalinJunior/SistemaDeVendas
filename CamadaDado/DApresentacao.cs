using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CamadaDado
{
    public class DApresentacao
    {
        public int IdApresentacao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string TextoBuscar { get; set; }

        public DApresentacao()
        {

        }

        public DApresentacao(int idApresentacao, string nome, string descricao, string textoBuscar)
        {
            this.IdApresentacao = idApresentacao;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TextoBuscar = textoBuscar;
        }

        // Método Inserir
        public string Inserir(DApresentacao apresentacao)
        {
            SqlConnection SqlCon = new SqlConnection();
            string resp = "";
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand(); // objeto Comando Sql 
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinserir_apresentacao";
                
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdApresentacao = new SqlParameter();
                parIdApresentacao.ParameterName = "idapresentacao"; 
                parIdApresentacao.SqlDbType = SqlDbType.Int; 
                parIdApresentacao.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(parIdApresentacao); 

                SqlParameter parNome = new SqlParameter();
                parNome.ParameterName = "@nome";
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50; // Limite de Caracter
                parNome.Value = this.Nome;
                SqlCmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter();
                parDescricao.ParameterName = "@descricao";
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 150;
                parDescricao.Value = this.Descricao;
                SqlCmd.Parameters.Add(parDescricao);

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
        public string Editar(DApresentacao apresentacao)
        {
            SqlConnection SqlCon = new SqlConnection();
            string resp = "";
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speditar_apresentacao";

                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdApresentacao = new SqlParameter();
                parIdApresentacao.ParameterName = "idapresentacao";
                parIdApresentacao.SqlDbType = SqlDbType.Int;
                parIdApresentacao.Value = apresentacao.IdApresentacao;
                SqlCmd.Parameters.Add(parIdApresentacao);

                SqlParameter parNome = new SqlParameter();
                parNome.ParameterName = "@nome";
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50;
                parNome.Value = this.Nome;
                SqlCmd.Parameters.Add(parNome);

                SqlParameter parDescricao = new SqlParameter();
                parDescricao.ParameterName = "@descricao";
                parDescricao.SqlDbType = SqlDbType.VarChar;
                parDescricao.Size = 150;
                parDescricao.Value = this.Descricao;
                SqlCmd.Parameters.Add(parDescricao);

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
        public string Excluir(DApresentacao apresentacao)
        {
            SqlConnection SqlCon = new SqlConnection();
            string resp = "";
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdeletar_apresentacao";

                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdApresentacao = new SqlParameter();
                parIdApresentacao.ParameterName = "idapresentacao";
                parIdApresentacao.SqlDbType = SqlDbType.Int;
                parIdApresentacao.Value = apresentacao.IdApresentacao;
                SqlCmd.Parameters.Add(parIdApresentacao);

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
                SqlCmd.CommandText = "spmostrar_apresentacao";
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
        public DataTable BuscarNome(DApresentacao apresentacao)
        {
            DataTable dtResultado = new DataTable();
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textoBuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;   
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = apresentacao.TextoBuscar;

                SqlCmd.Parameters.Add(parTextoBuscar);
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_apresentacao_nome";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(dtResultado);

                // É muito importante se atentar as ondem que vai ser realizado o código
            }
            catch (Exception ex)
            {
                return dtResultado;
                throw;
            }

            return dtResultado;
        }
    }
}
