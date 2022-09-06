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
    public class NProduto
    {
        // Função inserir
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

        // Função Editar
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

        // Função Deletar
        public static string Deletar(int id)
        {
            DProduto Obj = new DProduto();
            Obj.Id= id;
            return Obj.Excluir(Obj);
        }

        // Função Mostrar
        public static DataTable Mostrar()
        {
            return new DProduto().Mostrar();
        }

        // Fumção Mostrar
        public static DataTable BuscarNome(string textoBuscar)
        {
            DProduto Obj = new DProduto();
            Obj.TextoBuscar = textoBuscar;
            return Obj.BuscarNome(Obj);
        }
    }
}
