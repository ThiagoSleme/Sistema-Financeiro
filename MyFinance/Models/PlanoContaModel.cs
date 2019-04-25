using Microsoft.AspNetCore.Http;
using MyFinance.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }
        public string Descracao { get; set; }
        public string Tipo { get; set; }
        public int Usuario_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        //CONSTRUTOR
        public PlanoContaModel()
        {

        }
        //CONSTRUTOR RECEBE O ACESSO PARA AS VARIAVEIS DE SESSÃO
        public PlanoContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        //RETORNAR UMA LISTA DE TODAS AS CONTAS
        public List<PlanoContaModel> ListaPlanoConta()
        {
            List<PlanoContaModel> lista = new List<PlanoContaModel>();
            PlanoContaModel item;

            //CONEXÃO COM O BANCO
            //ID_USUARIO LOGADO
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID,DESCRICAO,TIPO,USUARIO_ID FROM PLANO_CONTAS WHERE USUARIO_ID = {id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTtable(sql);

            //verificação
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new PlanoContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Descracao = dt.Rows[i]["DESCRICAO"].ToString();
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                item.Usuario_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());

                lista.Add(item);
            }
            return lista;
        }        
        //METODO PARA CADASTRAR CONTA NO BANCO
        public void CadastrarPlanoCota()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"INSERT INTO PLANO_CONTAS (DESCRICAO,TIPO,USUARIO_ID) VALUES ('{Descracao}','{Tipo}','{id_usuario_logado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutaComandoSQL(sql);
        }

        //METODO EXLUIR
        public void Excluir(int id_conta)
        {
            new DAL().ExecutaComandoSQL($"DELETE FROM PLANO_CONTAS WHERE ID =('{id_conta}')");
        }
    }
}
