using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Text;

namespace BDSqlCeLocal
{
    /// <summary>
    /// vai retornar os parametros para criação e configuração do BD SqlCe
    /// </summary>
    public class BD
    {
        #region CONFIGURAÇÕES E PARAMETROS DO BD

        //vai retornar a senha do banco: ======================================================================================================================
        /// <summary>
        /// Senha BD01
        /// </summary>        
        public static String senhaBD01 { get; private set; } = "BD01";

        //vai retornar a senha do banco: ======================================================================================================================
        /// <summary>
        /// Senha BD02
        /// </summary>
        public static String senhaBD02 { get; private set; } = "BD02";

        //vai retornar a senha do banco: ======================================================================================================================
        /// <summary>
        /// Senha BD03
        /// </summary>
        public static String senhaBD03 { get; private set; } = "BD03";

        //vai retornar a senha do banco: ======================================================================================================================
        /// <summary>
        /// Senha BD04
        /// </summary>
        public static String senhaBD04 { get; private set; } = "BD04";

        //vai retornar o nome do banco: =======================================================================================================================
        /// <summary>
        /// Define e retorna o nome do arquivo do BancoBD01
        /// </summary>
        public static String bancoBD01 { get; private set; } = "BDSqlCe_Bd01.sdf";

        //vai retornar o nome do banco: =======================================================================================================================
        /// <summary>
        /// Define e retorna o nome do arquivo do BancoBD02
        /// </summary>
        public static String bancoBD02 { get; private set; } = "BDSqlCe_Bd02.sdf";

        //vai retornar o nome do banco: =======================================================================================================================
        /// <summary>
        /// Define e retorna o nome do arquivo do BancoBD03
        /// </summary>
        public static String bancoBD03 { get; private set; } = "BDSqlCe_Bd03.sdf";

        //vai retornar o nome do banco: =======================================================================================================================
        /// <summary>
        /// Define e retorna o nome do arquivo do BancoBD04
        /// </summary>
        public static String bancoBD04 { get; private set; } = "BDSqlCe_Bd04.sdf";

        //vai retornar o endereço da pasta para bkp da base: ==================================================================================================
        /// <summary>
        /// Define e retorna o endereço da pasta para bkp da base
        /// </summary>
        //private static String PastaBDBKP { get; } = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"BD_BKP\";
        public static String pastaBDBKP { get; private set; } = ConfigPasta(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"BD_BKP\");

        //Monta endereço do Pasta da Base SqlCE:  =============================================================================================================
        /// <summary>
        /// Define e retorna a endereço da pasta da Base SqlCE pasta BD
        /// </summary>
        //public  String PastaBD { get; private set; } = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"BD\";
        public static String pastaBD { get; private set; } = ConfigPasta(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"BD\");

        //Monta endereço do Pasta sql AtuBanco SqlCE:  =============================================================================================================
        /// <summary>
        /// Define e retorna a endereço da pasta de Scrip do AtuBanco
        /// </summary>
        //public String PastaAtuBD { get; private set; } = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"AtuBD\";
        public static String pastaAtuBD { get; private set; } = ConfigPasta(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"AtuBD\");

        //Monta endereço do Pasta sql AtuBanco SqlCE:  =============================================================================================================
        /// <summary>
        /// Define e retorna a endereço da pasta de Imagens para o BD01
        /// </summary>        
        public static String pastaImg { get; private set; } = ConfigPasta(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"ImgBD1\");

        #endregion

        /// <summary>
        /// verificar se a pasta existe, se nao existir, cria a pasta:
        /// </summary>
        /// <param name="caminho"></param>
        /// <returns></returns>
        private static string ConfigPasta(string caminho)
        {
            try
            {
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

            }
            catch (Exception er)
            {
                throw new Exception($"Erro ao configurar pasta! \n {er.Message}");

                //Caso nao encontrado:
                //BLL_MsgPopupPersonalizada.Mostrar("Erro ao cria a pasta! \nVerifique com desenvolvedor! \nERRO: " + er.Message, BLL_MsgPopupPersonalizada.ErrorOK);
            }

            return caminho;
        }

    }
}
