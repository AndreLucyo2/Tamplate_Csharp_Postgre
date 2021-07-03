using System;
using System.Data.SqlServerCe;
using System.IO;

namespace BDSqlCeLocal
{
    /// <summary>
    /// Cria Banco define string de Conexao
    /// </summary>
    public class SQLCeServer
    {
        //Esta propriedade pode ser acessada por todas as instancias da classe ja com valore definido.
        public static string StrConnBD01 = null;

        //MONTAR StringCom BD01 ===============================================================================================================================
        /// <summary>
        /// Definir e retorna a StringCom do BD01 SqlCe, Cria o *.sqdf caso nao exista
        /// a String de conexao definida aqui vale para toda a aplicacao, pois é um metodo Estatico
        /// </summary>
        /// <returns></returns>
        public static String GetStrCon()
        { 
            try
            {
                //definir o Nome e o caminho completo da base de dados:[+ senha de bloqueio]
                StrConnBD01 = $"Data Source= {BD.pastaBD}{BD.bancoBD01}; encryption mode = platform default; Password={BD.senhaBD01}";

                //Metodo para cria o arquivo *.sdf do banco:(Caso nao exista)
                DBSqlCeCreator_BD01(StrConnBD01, BD.pastaBD, BD.bancoBD01);

            }
            catch (Exception er)
            {
                return null;
                throw new Exception($"Falha na conexao Verifique com desenvolvedor! \nERRO: {er.Message}");               
            }
            return StrConnBD01;
        }

        /// <summary>
        /// Cria o arquivo do banco de dados zerado .sdf:
        /// </summary>
        private static void DBSqlCeCreator_BD01(string StrConnBD, string pastaBD, string nomeBD)
        {
            try
            {
                //Verificar se o ArquivO da base de dados existe so criar se nao existir:
                if (!File.Exists(pastaBD + nomeBD))
                {
                    //criar o Base de dados SQLServerCE:
                    SqlCeEngine V_Motor = new SqlCeEngine(StrConnBD);
                    V_Motor.CreateDatabase(); // Cria a base zerada vazia
                    V_Motor.Dispose();

                    //Criar as Tabelas:
                    BD01_Criar.Criar_Tabelas_Inserts_BD01(StrConnBD);
                }
            }
            catch (Exception er)
            {
                throw new Exception($"Falha na conexao Verifique com desenvolvedor! \nERRO: {er.Message}");                
            }

        }

        //METODO QUE FAZ teste de conexão =====================================================================================================================
        /// <summary>
        /// Testar Status da Conexao, recebe uma String de Conexao
        /// </summary>
        /// <param name="Strconn"></param>
        public static void TestarStatusConexao(string Strconn)
        {
            try
            {
                //Executa teste de conexão valida Strincon!
                SqlCeConnection conexao = new SqlCeConnection();
                conexao.ConnectionString = Strconn;
                conexao.Open();
                conexao.Close();

                //MessageBox.Show("Conexao ok!");
            }
            catch (Exception er)
            {             
                throw new Exception($"Falha na conexao Verifique com desenvolvedor! \nERRO: {er.Message}");
            }

            return;

        }

        //===  METODO PARA EXECUTAR COMANDO SEM RETORNO DE DADOS ==========================================================================================
        /// <summary>
        /// Metodo para executar querys direto na base de dados  SEM RETORNO DE DADOS, Recebe uma String de conexao e a Query
        /// </summary>
        /// <param name="StrConn"></param>
        /// <param name="Query"></param>
        public void ExecuteQuery(string StrConn, string Query)
        {
            //--------------------------------------------------------------------------------------------------
            //CRIAR TEBELAS: criar as tabelas e parametros SQL 
            //--------------------------------------------------------------------------------------------------
            //estabelece a conexão na base criada: para criacao das tabelas
            SqlCeConnection conexao = new SqlCeConnection(StrConn);

            try
            {
                conexao.Open();//abre a conexao:

                //Criar comando Sql:
                SqlCeCommand ComandoSQL = new SqlCeCommand();
                ComandoSQL.Connection = conexao;

                //FECHA A CONSTRUÇÃO DA QUERY E EXECUTA-LA:
                ComandoSQL.CommandText = Query;
                ComandoSQL.ExecuteNonQuery();//injeta a QUERY
                

            }
            catch (Exception Er)
            {
                conexao.Close();
                conexao.Dispose();

                throw new Exception($"Erro SqlCe Server!\n {Er.Message}");
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }

        /// <summary>
        /// Retorna a data do banco de dados
        /// </summary>
        /// <param name="StrConn"></param>
        /// <returns></returns>
        public static DateTime DataServerBD(string StrConn) 
        {
            DateTime retorno = DateTime.Now;

            try
            {
                using (SqlCeCommand ComandoSQL = new SqlCeCommand())
                {
                    SqlCeConnection conexao = new SqlCeConnection(StrConn);
                    ComandoSQL.Connection = conexao;
                    ComandoSQL.CommandText = "SELECT GETDATE() AS DataBD";
                    retorno = Convert.ToDateTime(ComandoSQL.ExecuteScalar());
                }
            }
            catch (Exception Er)
            {
                throw new Exception($"Erro ao buscar data no SqlCe Server!\n {Er.Message}");
            }

            return retorno;
        }
    }

}
