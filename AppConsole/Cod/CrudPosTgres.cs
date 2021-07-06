using BDSqlPostGres.Cod;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConsole.Cod
{
    public class CrudPosTgres
    {
        public static void MainCRUD()
        {
            //Definir os dados da conexao:
            BdConfig();
            //Cria uma nova conexao:
            ServidorPostGres.SetNewsqlConnection();
            ServidorPostGres.AbreConexao();

            Console.WriteLine(ServidorPostGres.StatusConexaoMsg());

            ServidorPostGres.FecharConexao();

            Console.WriteLine(ServidorPostGres.StatusConexaoMsg());


        }

        private static void BdConfig()
        {
            //Dados para conectar o Servidor:
            ServidorPostGres.servidor = "192.168.1.120";
            ServidorPostGres.porta = "5432";
            ServidorPostGres.banco = "dbOSManager";
            ServidorPostGres.usuario = "postgres";
            ServidorPostGres.senha = "12345678";
            //ServidorPostGres.pastaBkp = "";

            Console.WriteLine(ServidorPostGres.GetStrConn);

        }


    }
}
