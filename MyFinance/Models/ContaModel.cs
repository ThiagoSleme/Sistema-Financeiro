using Microsoft.AspNetCore.Http;
using MyFinance.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get;set; }
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public int Usuario_Id { get; set; }

        //
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        //CONSTRUTOR
        public ContaModel()
        {

        }
        //RECEBE O ACESSO PARA AS VARIAVEIS DE SESSÃO
        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        //RETORNAR UMA LISTA DE TODAS AS CONTAS
        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel item;

            //CONEXÃO COM O BANCO
            //ID_USUARIO LOGADO
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID,NOME,SALDO,USUARIO_ID FROM CONTA WHERE USUARIO_ID = {id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTtable(sql);

            //verificação
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Nome = dt.Rows[i]["NOME"].ToString();
                item.Saldo = double.Parse(dt.Rows[i]["SALDO"].ToString());
                item.Usuario_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());

                lista.Add(item);
            }
            return lista;
        }

        //METODO PARA CADASTRAR CONTA NO BANCO
        public void InsertNovaConta()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"INSERT INTO CONTA (NOME,SALDO,USUARIO_ID) VALUES ('{Nome}','{Saldo}','{id_usuario_logado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutaComandoSQL(sql);
        }

        //METODO EXLUIR
        public void Excluir(int id_conta)
        {
            new DAL().ExecutaComandoSQL($"DELETE FROM CONTA WHERE ID =('{id_conta}')" );
        }
    }
}
