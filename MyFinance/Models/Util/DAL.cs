using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.Util
{
    public class DAL
    {
        //DADDOS PARA CONEXÃO NO BANCO CONNECTION STRING
        private static string server = "localhost";
        private static string dataBase = "financeiro";
        private static string user = "root";
        private static string password = "Thiaguinhos3leme";

        //REALIZAR A CONEXAO
        private string connectionString = $"Server={server};DataBase={dataBase};Uid={user};Pwd={password}";

        //REALIZA O COMANDO DE CONEXÃO
        private MySqlConnection connection;

        //CONSTRUTOR PARA CONEXÃO
        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            //ABRIR CONEXÃO
            connection.Open();
        }

        //METODO PARA PASSAR OS DADOS PARA TABELA
        //RETORNANDO UMA CONSULTA SQL - SELECT
        public DataTable RetDataTtable(string sql)
        {
            DataTable dataTable = new DataTable();
            //Busca informação no banco de dados (comando + conexão)
            MySqlCommand command = new MySqlCommand(sql, connection);
            //Adaptar os dados (comando que vai adaptar = commando do sql)
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            //meto que preenche
            da.Fill(dataTable);
            return dataTable;
        }

        //METODO QUE IRAR EXECUTAR OS COMANDOS NO BANCO DE DADOS
        //INSERT, UPDATE , DELET
        public void ExecutaComandoSQL(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

    }
}
