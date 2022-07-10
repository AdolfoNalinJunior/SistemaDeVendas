using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDado;

namespace CamadaNegocio
{
    public class NCategoria
    {
        // Função inserir
        public static string Inserir(string nome, string descricao)
        {
            DCategoria Obj = new DCategoria();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            return Obj.Inserir(Obj);
        }

        // Função Editar
        public static string Editar(int idCategoria, string nome, string descricao)
        {
            DCategoria Obj = new DCategoria();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.IdCategoria = idCategoria;
            return Obj.Editar(Obj);
        }

        // Função Deletar
        public static string Deletar(int idCategoria)
        {
            DCategoria Obj = new DCategoria();
            Obj.IdCategoria = idCategoria;
            return Obj.Excluir(Obj);
        }

        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();
        }

        public static DataTable Buscar(string textoBuscar)
        {
            DCategoria Obj = new DCategoria();
            Obj.TextoBuscar = textoBuscar;
            return Obj.Buscar(Obj);
        }
    }
}
