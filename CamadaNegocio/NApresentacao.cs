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
    /// NApresentacao e relacionada a DApresentacao, é uma negociação entre C# e SQL
    /// </summary>
    public class NApresentacao
    {
        /// <summary>
        /// Função para inserir a apresentação
        /// </summary>
        /// <param name="nome">Nome da apresentação</param>
        /// <param name="descricao">Descrição da apresentação</param>
        /// <returns></returns>
        public static string Inserir(string nome, string descricao)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            return Obj.Inserir(Obj);
        }

        /// <summary>
        /// Função que realiza a Edição da apresentação
        /// </summary>
        /// <param name="idApresentacao">Id da apresentação</param>
        /// <param name="nome">Nome da apresentação</param>
        /// <param name="descricao">Descrição da apresentação</param>
        /// <returns></returns>
        public static string Editar(int idApresentacao, string nome, string descricao)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.IdApresentacao = idApresentacao;
            return Obj.Editar(Obj);
        }

        /// <summary>
        /// Função de deleção da apresentação
        /// </summary>
        /// <param name="idApresentacao">chave da deleção</param>
        /// <returns></returns>
        public static string Deletar(int idApresentacao)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.IdApresentacao = idApresentacao;
            return Obj.Excluir(Obj);
        }

        /// <summary>
        /// Função que pega os dados do banco de dados e trás para mostrar no dataGrid
        /// </summary>
        /// <returns></returns>
        public static DataTable Mostrar()
        {
            return new DApresentacao().Mostrar();
        }

        /// <summary>
        /// Função que realiza a busca das apresentações no banco de dados
        /// </summary>
        /// <param name="textoBuscar"></param>
        /// <returns></returns>
        public static DataTable BuscarNome(string textoBuscar)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.TextoBuscar = textoBuscar;
            return Obj.BuscarNome(Obj);
        }
    }
}
