using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDado;

namespace CamadaNegocio
{
    /// <summary>
    /// NCategoria é relacionada com DCategoria, para fazeer a negóciação entre C# e SQL
    /// </summary>
    public class NCategoria
    {
        /// <summary>
        /// Função que realiza a inserção das categorias
        /// </summary>
        /// <param name="nome">Nome de input</param>
        /// <param name="descricao">Descrição de input</param>
        /// <returns></returns>
        public static string Inserir(string nome, string descricao)
        {
            DCategoria Obj = new DCategoria();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            return Obj.Inserir(Obj);
        }

        /// <summary>
        /// Função que realiza a Edição das categorias
        /// </summary>
        /// <param name="idCategoria">ID da cetegoria, esse campo não é editavel. Essa variável é altoemplente da próprio banco de dados</param>
        /// <param name="nome">Novo nome da categoria que vai ser editada</param>
        /// <param name="descricao">Nova descrição da categoria que vai ser editada</param>
        /// <returns></returns>
        public static string Editar(int idCategoria, string nome, string descricao)
        {
            DCategoria Obj = new DCategoria();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.IdCategoria = idCategoria;
            return Obj.Editar(Obj);
        }

        /// <summary>
        /// Função que realiza a deleção das categoria
        /// </summary>
        /// <param name="idCategoria">Chave da deleção</param>
        /// <returns></returns>
        public static string Deletar(int idCategoria)
        {
            DCategoria Obj = new DCategoria();
            Obj.IdCategoria = idCategoria;
            return Obj.Excluir(Obj);
        }
        
        /// <summary>
        /// Função que relciona o dataGrid com o banco de dados
        /// </summary>
        /// <returns></returns>
        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();
        }

        /// <summary>
        /// Função que realiza a busca da categoria no banco de dados
        /// </summary>
        /// <param name="textoBuscar">Nome da chave de busca</param>
        /// <returns></returns>
        public static DataTable BuscarNome(string textoBuscar)
        {
            DCategoria Obj = new DCategoria();
            Obj.TextoBuscar = textoBuscar;
            return Obj.BuscarNome(Obj);
        }
    }
}
