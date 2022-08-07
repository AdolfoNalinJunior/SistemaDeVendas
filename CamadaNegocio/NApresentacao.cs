using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CamadaDado;

namespace CamadaNegocio
{
    public class NApresentacao
    {
        // Função inserir
        public static string Inserir(string nome, string descricao)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            return Obj.Inserir(Obj);
        }

        // Função Editar
        public static string Editar(int idApresentacao, string nome, string descricao)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.Nome = nome;
            Obj.Descricao = descricao;
            Obj.IdApresentacao = idApresentacao;
            return Obj.Editar(Obj);
        }

        // Função Deletar
        public static string Deletar(int idApresentacao)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.IdApresentacao = idApresentacao;
            return Obj.Excluir(Obj);
        }

        // Função Mostrar
        public static DataTable Mostrar()
        {
            return new DApresentacao().Mostrar();
        }

        // Fumção Mostrar
        public static DataTable BuscarNome(string textoBuscar)
        {
            DApresentacao Obj = new DApresentacao();
            Obj.TextoBuscar = textoBuscar;
            return Obj.BuscarNome(Obj);
        }
    }
}
