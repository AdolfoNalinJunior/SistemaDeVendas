using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CamadaDado
{
    /// <summary>
    /// Classe que é responsável por trazer os dados do banco de dados e mander a conexão da tabela db.categoria
    /// </summary>
    public class DCategoria
    {
        /// <summary>
        /// Membros da classe 
        /// </summary>
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string TextoBuscar { get; set; }

        /// <summary>
        /// Construtor vazio
        /// </summary>
        public DCategoria()
        {

        }

        /// <summary>
        /// Construtor com os parametros
        /// </summary>
        /// <param name="idCategoria">Id de entrada da categoria</param>
        /// <param name="nome">Nome de entrada da categoria</param>
        /// <param name="descricao">Descrição de entrada da categoria</param>
        /// <param name="textoBuscar">Parametro de entrada para que seja realizada a busca da categoria no banco de dados</param>
        public DCategoria(int idCategoria, string nome, string descricao, string textoBuscar)
        {
            this.IdCategoria = idCategoria;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TextoBuscar = textoBuscar;
        }

        /// <summary>
        /// Função de inserção da categoria no banco de dados
        /// </summary>
        /// <param name="categoria">Parametro de entrada, para a inserção da categoria</param>
        /// <exception cref="ArgumentException"
        /// <exception cref="ArgumentNullException"
        /// <returns></returns>
        public string Inserir(DCategoria categoria)
        {
            // Criando o Objeto Connection e dando o nome de SqlCon (Con = Conexão)
            SqlConnection SqlCon = new SqlConnection();
            // Tratamento de erro
            string resp = "";
            try
            {
                /*
                 * Passando a conexão da calsse Conexão para SqlCon para
                 * conseguir Setar dentro de uma variável que recebe 
                 * parêmetos de conexão Conexão
                 */
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                // Criação do Objeto de comando SQL (CMD = Comando)
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinserir_categoria";
                /*
                 * Quando o objeto já existente você vai passar ele entre " "
                 * como que se fosse uma string. Isso se chama procedures
                 * 
                 * O principal motivo é que, uma procedure é um código 
                 * independente que fica pré-compilado no servidor de banco de dados, 
                 * assim,retiramos a onerosidade de processamento da aplicação e a 
                 * passamos para o servidor sendo que, a aplicação, apenas recebe o 
                 * resultado do processamento dessa procedures e continua seu fluxo ...
                 */
                SqlCmd.CommandType = CommandType.StoredProcedure;

                // Criando variável e modelagem das variáveis do banco de dados
                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "idcategoria"; // Name na consulta
                parIdCategoria.SqlDbType = SqlDbType.Int; // Type na consulta
                parIdCategoria.Direction = ParameterDirection.Output; 
                SqlCmd.Parameters.Add(parIdCategoria); // Para adicionar ao banco de dados

                SqlParameter parNome = new SqlParameter();
                parNome.ParameterName = "@nome"; 
                parNome.SqlDbType = SqlDbType.VarChar;
                parNome.Size = 50; // Limite de Caracter
                parNome.Value = this.Nome;
                // Quando o valor do objeto não é alto encremente é necessário passar um valor
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
                // Si o status da minha conexao estiver aberta, ela pode ser fechada
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
                else
                {
                    // Se ele estiver aberta, retornara a messagem para o usuário
                    resp = "A conexão não foi aberta, não é possível ser fechada!! ";
                }
            }
            return resp;
        }

        /// <summary>
        /// Função que realiza a edição da categoria, e tráz as informações do banco de dados
        /// </summary>
        /// <param name="categoria">Parametro de entrada para localizar a categoria no banco de dados</param>
        /// <excepiton cref="ArgumentException"
        /// <exception cref="ArgumentNullException"
        /// <returns></returns>
        public string Editar(DCategoria categoria)
        {
            SqlConnection SqlCon = new SqlConnection();
            string resp = "";
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speditar_categoria";
               
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "idcategoria"; 
                parIdCategoria.SqlDbType = SqlDbType.Int; 
                parIdCategoria.Value = categoria.IdCategoria;
                SqlCmd.Parameters.Add(parIdCategoria); 

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
        /// Função de exclusão das categoria do banco de dados
        /// </summary>
        /// <param name="categoria">Parametro de entrada para localizar as categoria no banco de dados</param>
        /// <exception cref="ArgumentNullException"
        /// <exception cref="ArgumentException"
        /// <returns></returns>
        public string Excluir(DCategoria categoria)
        {
        SqlConnection SqlCon = new SqlConnection();
        string resp = "";
        try
        {
            SqlCon.ConnectionString = Conexao.Cn;
            SqlCon.Open();

            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = SqlCon;
            SqlCmd.CommandText = "spdeletar_categoria";

            SqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parIdCategoria = new SqlParameter();
            parIdCategoria.ParameterName = "idcategoria";
            parIdCategoria.SqlDbType = SqlDbType.Int;
            parIdCategoria.Value = categoria.IdCategoria;
            SqlCmd.Parameters.Add(parIdCategoria);

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
        /// Função que busca os dados dentro do banco de dados para mostrar no dataGrid 
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
                SqlCmd.CommandText = "spmostrar_categoria";
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
        /// Função que realiza a busca da categoria dentro do banco de dados
        /// </summary>
        /// <param name="Categoria">Parametros de entrada para realizar a busca</param>
        /// <exception cref="ArgumentException"
        /// <exception cref="ArgumentNullException"
        /// <returns></returns>
        public DataTable BuscarNome(DCategoria Categoria)
        {
            DataTable dtResultado = new DataTable("categoria");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_nome_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textobuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = Categoria.TextoBuscar;
                SqlCmd.Parameters.Add(parTextoBuscar);

                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(dtResultado);

              // É muito importante se atentar as ondem que vai ser realizado o código
            }
            catch (Exception ex)
            {
                return dtResultado =null;
            }

            return dtResultado;
        }
    }
}