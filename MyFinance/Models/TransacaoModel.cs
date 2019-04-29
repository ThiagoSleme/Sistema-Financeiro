using Microsoft.AspNetCore.Http;
using MyFinance.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public int Conta_Id { get; set; }
        public int Plano_Contas_Id { get; set; }
        public int Usuario_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        //RECEBER O CONTEXTO PARA ACESSAR A VARIAVEL DE SESSAO
        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        //METODO
        public TransacaoModel()
        {

        }


        //RETORNAR UMA LISTA DE TODAS AS CONTAS
        public List<TransacaoModel> ListaTransacao()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel item;

            //CONEXÃO COM O BANCO
            //ID_USUARIO LOGADO
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID,DATA,TIPO,VALOR, DESCRICAO, CONTA_ID FROM TRANSACOES WHERE USUARIO_ID = {id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTtable(sql);

            //verificação
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new TransacaoModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["DATA"].ToString());
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                item.Valor = double.Parse(dt.Rows[i]["VALOR"].ToString());
                item.Conta_Id = int.Parse(dt.Rows[i]["CONTA_ID"].ToString());
                item.Plano_Contas_Id = int.Parse(dt.Rows[i]["PLANO_CONTAS_ID"].ToString());
                item.Usuario_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());

                lista.Add(item);
            }
            return lista;
        }
    }
}
