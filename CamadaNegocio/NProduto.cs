using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaNegocio;
using CamadaDado;

namespace CamadaNegocio
{
    /// <summary>
    /// Classe de adiminstração dos Produtos
    /// </summary>
    public class NProduto
    {
        /// <summary>
        /// Função que realiza a inserção dos produtos
        /// </summary>
        /// <param name="nome">Nome do produto</param>
        /// <param name="descricao">Descrição do Produto</param>
        /// <param name="codigo">Código do proprio produto</param>
        /// <param name="imagem">Imagem do produto</param>
        /// <param name="idCategoria">Id da categoria, para buscar relacionar o tipo da cvat</param>
        /// <param name="idApresentacao">Id da apresentação, para relacionar uma apresentação ao produto</param>
        /// <returns></returns>
        public static string Inserir(string nome, string descricao, string codigo, byte[] imagem, int idCategoria, int idApresentacao)
        {
            DProduto Obj = new DProduto();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.Codigo = codigo;
            Obj.Imagem = imagem;
            Obj.IdApresentacao = idApresentacao;
            Obj.IdCategoria = idCategoria;

            return Obj.Inserir(Obj);
        }

        /// <summary>
        /// Função que realiza a Edição 
        /// </summary>
        /// <param name="id">Id produto, esse valor não editavel</param>
        /// <param name="nome">Nome do produto</param>
        /// <param name="descricao">Descrição do produto</param>
        /// <param name="codigo">Código do produto</param>
        /// <param name="imagem">Imagem do produto</param>
        /// <param name="idCategoria">Id da categoria</param>
        /// <param name="idApresentacao">Id da apresentação</param>
        /// <returns></returns>
        public static string Editar(int id,string nome, string descricao, string codigo,byte[] imagem, int idCategoria, int idApresentacao)
        {
            DProduto Obj = new DProduto();
            Obj.Id = id;
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.Codigo = codigo;
            Obj.Imagem = imagem;
            Obj.IdApresentacao = idApresentacao;
            Obj.IdCategoria = idCategoria;
            return Obj.Editar(Obj);
        }

        /// <summary>
        /// Função de deleção
        /// </summary>
        /// <param name="id">Chave de busca para a deleção</param>
        /// <returns></returns>
        public static string Deletar(int id)
        {
            DProduto Obj = new DProduto();
            Obj.Id= id;
            return Obj.Excluir(Obj);
        }

        /// <summary>
        /// Função para mostrar os produtos na tabela
        /// </summary>
        /// <returns></returns>
        public static DataTable Mostrar()
        {
            return new DProduto().Mostrar();
        }

        /// <summary>
        /// Função que Busca o nome do produto no banco de dados
        /// </summary>
        /// <param name="textoBuscar">Varável que busca pelo nome</param>
        /// <returns></returns>
        public static DataTable BuscarNome(string textoBuscar)
        {
            DProduto Obj = new DProduto();
            Obj.TextoBuscar = textoBuscar;
            return Obj.BuscarNome(Obj);
        }
    }
}
