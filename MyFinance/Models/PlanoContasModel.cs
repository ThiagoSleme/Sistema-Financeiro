using Microsoft.AspNetCore.Http;
using MyFinance.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class PlanoContasModel
    {
        public int Id { get; set; }
        public string Descracao { get; set; }
        public string Tipo { get; set; }
        public int Usuario_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        //CONSTRUTOR
        public PlanoContasModel()
        {

        }
        //CONSTRUTOR RECEBE O ACESSO PARA AS VARIAVEIS DE SESSÃO
        public PlanoContasModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        //RETORNAR UMA LISTA DE TODAS AS CONTAS
        public List<PlanoContasModel> ListaPlanoConta()
        {
            List<PlanoContasModel> lista = new List<PlanoContasModel>();
            PlanoContasModel item;

            //CONEXÃO COM O BANCO
            //ID_USUARIO LOGADO
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID,DESCRICAO,TIPO,USUARIO_ID FROM PLANO_CONTAS WHERE USUARIO_ID = {id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTtable(sql);

            //verificação
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new PlanoContasModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Descracao = dt.Rows[i]["DESCRICAO"].ToString();
                item.Tipo = dt.Rows[i]["SALDO"].ToString();
                item.Usuario_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());

                lista.Add(item);
            }
            return lista;
        }

        //METODO PARA CADASTRA PLANO DE CONTAS
        public void CadastrarPlanoCota()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"INSET INTO PLANO_CONTAS (DESCRICAO,TIPO,USUARIO_ID) VALUES ('{Descracao}','{Tipo}','{id_usuario_logado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutaComandoSQL(sql);
        }
    }
}
