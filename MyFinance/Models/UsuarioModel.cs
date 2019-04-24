using MyFinance.Models.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {

        public int Id { get; set; }

        //UTILIZAÇÃO DO DATA ANOTATION
        //[Required(ErrorMessage ="asasas")]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Data_Nascimento { get; set; }


        //METODO PARA VERIFICAR SE EXISTE O USUARIO NO BANCO DE DADOS
        public bool ValidarLogin()
        {
            string sql = $"SELECT ID, NOME, DATA_NASCIMENTO FROM USUARIO WHERE EMAIL='{Email}' AND SENHA='{Senha}'";           
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTtable(sql);

            if (dt !=null)
            {
                if (dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["ID"].ToString());
                    Nome = (dt.Rows[0]["NOME"].ToString());
                    Data_Nascimento = dt.Rows[0]["DATA_NASCIMENTO"].ToString();
                    return true;
                }
            }
            return false;
        }

        //METODO PARA CADASTRAR USUARIO
        public void CadastrarUsuario()
        {
            //FORMATAR A DATA PARA GRAVAR NO BANCO
            string dataNascimento = DateTime.Parse(Data_Nascimento).ToString("yyyy/MM/dd");

            string sql = $"INSERT INTO USUARIO (NOME, EMAIL, SENHA, DATA_NASCIMENTO) VALUES ('{Nome}','{Email}','{Senha}','{dataNascimento}' )";
            DAL ObjDAL = new DAL();
            ObjDAL.ExecutaComandoSQL(sql);
        }
    }
}
