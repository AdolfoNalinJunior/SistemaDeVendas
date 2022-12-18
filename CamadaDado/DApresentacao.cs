using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CamadaDado
{
    /// <summary>
    /// Classe responsavél por realizar a conexão do banco de dados com a tabela apesentação
    /// </summary>
    public class DApresentacao
    {
        /// <summary>
        /// Membros resposaveis da classe
        /// </summary>
        public int IdApresentacao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string TextoBuscar { get; set; }

        /// <summary>
        /// Construtor vazio
        /// </summary>
        public DApresentacao()
        {

        }

        /// <summary>
        /// Construtor com parametros
        /// </summary>
        /// <param name="idApresentacao">Parametro de entrada do <paramref name="idApresentacao"/></param>
        /// <param name="nome">Parametro de entrada do <paramref name="nome"/> da apresentação adicionada</param>
        /// <param name="descricao">Parametor de entrada da <paramref name="descricao"/></param>
        /// <param name="textoBuscar">Parametro de pesquisa na tabela apresentacao</param>
        public DApresentacao(int idApresentacao, string nome, string descricao, string textoBuscar)
        {
            this.IdApresentacao = idApresentacao;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TextoBuscar = textoBuscar;
        }

        /// <summary>
        /// Função de inserção das apresentações
        /// </summary>
        /// <param name="apresentacao">Parametro chave para a inserção do apresentação</param>
        /// <exception cref="ArgumentException"
        /// <exception cref="ArgumentNullException"
        /// <returns>Se a inserção for feita o valor sera ok, se acaso não for o valor é </returns>
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
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A apresentação não foi inseridaw";
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

        /// <summary>
        /// Função que realiza a edição da Apresentação no banco de dados
        /// </summary>
        /// <param name="apresentacao">Parametro chave para realizar a edição <paramref name="DApresentacao"</param>
        /// <returns></returns>
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

        /// <summary>
        /// Função que realiza a deleção da tabela <paramref name="apresentacao"
        /// </summary>
        /// <param name="apresentacao">Parametro chave para realizar a deleção</param>
        /// <exception cref="ArgumentException"
        /// <exception cref="ArgumentNullException"
        /// <returns></returns>
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

        /// <summary>
        /// Função que pega os parametros da tabela <paramref name="apresentacao">tabela do banco de dados</paramref>e mostra no dataGrid
        /// </summary>
        /// <exception cref="ArgumentException"
        /// <exception cref="ArgumentNullException"
        /// <returns></returns>
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
                return dtResultado;
            }
            return dtResultado;
        }

        /// <summary>
        /// Função que realiza a tradução do procedimento de busca pelo nome, da tabela <paramref name="apresentacao">Tabela do banco de dados db.comercio</paramref>
        /// </summary>
        /// <param name="apresentacao">Parametro chave para realizar a busca </param>
        /// <exception cref="ArgumentException"
        /// <exception cref="ArgumentNullException"
        /// <returns></returns>
        public DataTable BuscarNome(DApresentacao apresentacao)
        {
            DataTable dtResultado = new DataTable("apresentação");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_nome_apresentacao";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textobuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;   
                parTextoBuscar.Size = 100;
                parTextoBuscar.Value = apresentacao.TextoBuscar;
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
