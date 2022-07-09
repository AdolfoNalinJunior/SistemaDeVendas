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
    internal class DCategoria
    {
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string TextoBuscar { get; set; }

        // Construtor Vazio
        public DCategoria()
        {

        }

        // Construtor com Parâmetros
        public DCategoria(int idCategoria, string nome, string descricao, string textoBuscar)
        {
            // Criando relacionamento com os campos privados
            this.IdCategoria = idCategoria;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TextoBuscar = textoBuscar;
        }

        // Método Inserir
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
                 * O principal destes motivos é que, uma procedure éum código 
                 * independente que fica pré-compilado no servidor de banco de dados, 
                 * assim,retiramos a onerosidade de processamento da aplicação e a 
                 * passamos para o servidor sendo que, aaplicação, apenas recebe o 
                 * resultado do processamento destas procedures e continua seu fluxo ...
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
                // Si o status da minha conexao for igual a aberta, pode fechar
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
                else
                {
                    resp = "A conexão não foi aberta, não é possível ser fechada!! ";
                }
            }
        }

        // Método Editar
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
        }
        

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
    }
    
        // Método mostrar
        public DataTable Mostrar(DCategoria)
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
            catch (Exception e)
            {
                dtResultado = null;
                throw;
            }
        }

        // Método Buscar
        public DataTable Buscar(DCategoria categoria)
        {
            DataTable dtResultado = new DataTable();
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexao.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbucar_nome";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(SqlCmd);
                sqlDat.Fill(dtResultado);

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textoBuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = categoria.TextoBuscar;
                SqlCmd.Parameters.Add(parTextoBuscar);
            }
            catch (Exception e)
            {
                dtResultado = null;
                throw;
            }
        }
    }
}