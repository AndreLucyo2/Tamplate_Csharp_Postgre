using BDSqlPostGres.View;
using Npgsql;
using System;
using System.IO;
using System.Windows;

namespace BDSqlPostGres.Cod
{
    public static class SqlPostGresServer
    {
        //Campos da conexão:
        public static String servidor;   //IP Servidor
        public static String porta;      //porta
        public static String banco;      //nome do banco de dados
        public static String usuario ;   //nome do administrador
        public static String senha;      //senha do administrador
        public static String pastaBkp;

        //Dado do Arquivo de configurações *.config:
        private static String _arquivoBDConfig = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "BD.config";

        //pasta meus documentos Geral:
        private static String _pastaBkp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BKP_Banco";


        //Monta  e retorna a String da conexao:
        public static String StringDeConexao
        {
            get
            {
                //vai retornar a string conexão com o banco de dados:
                //montar a string e conexão usando os campos do arquivo config
                return "Server=" + servidor + ";Port=" + porta + ";User Id=" + usuario + ";Password=" + senha + ";Database=" + banco;
            }

        }

        //Monta e retorna o endereço do Arquivo da config:
        public static String ArquivoBDConfig
        {
            //vai retornar a endereço do arquivo da Base SqlCE:
            get { return _arquivoBDConfig; }
        }

        //Monta e retorna o endereço da pasta padrão de bkp:
        public static String PastaBkp
        {
            //vai retornar a endereço do arquivo da Base SqlCE:
            get { return _pastaBkp; }
        }

        //faz a leitura dos dados da conexão:
        public static void LoadConection()
        {
            try
            {
                //Arquivo *.config:
                string ArquivoBDConfig = SqlPostGresServer.ArquivoBDConfig;

                //Verfica se *.config Existe:
                if (!File.Exists(ArquivoBDConfig))
                {
                    //Caso nao encontrado: 
                    MessageBox.Show("Configure dados de conexao com banco!");

                    //Abre tela para criar BD.config
                    FrmBDConfig frm = new FrmBDConfig();
                    frm.ShowDialog();
                }

                //Faz leitura do arquivo config
                StreamReader arquivo = new StreamReader(ArquivoBDConfig);

                //faz a leitura do arquivo BD.config e guarda em variaveis
                string _servidor = arquivo.ReadLine();
                string _porta = arquivo.ReadLine();
                string _banco = arquivo.ReadLine();
                string _usuario = arquivo.ReadLine();
                string _senha = arquivo.ReadLine();
                string _pastaBkp = arquivo.ReadLine();

                //Validar se tem algum dado vazio ou null:
                if (_servidor != "" && _porta != "" && _banco != "" && _usuario != "" && _senha != "" && _pastaBkp != "" &&
                    _servidor != null && _porta != null && _banco != null && _usuario != null && _senha != null && _pastaBkp != null)
                {
                    //---------------------------------------------------------------------------------
                    //CRIPTOGRAFIA - Passa  os dados da conexão monta string
                    //---------------------------------------------------------------------------------            
                    SqlPostGresServer.servidor = ConnetctionCrypt.Decriptar(_servidor);
                    SqlPostGresServer.porta = ConnetctionCrypt.Decriptar(_porta);
                    SqlPostGresServer.banco = ConnetctionCrypt.Decriptar(_banco);
                    SqlPostGresServer.usuario = ConnetctionCrypt.Decriptar(_usuario);
                    SqlPostGresServer.senha = ConnetctionCrypt.Decriptar(_senha);
                    SqlPostGresServer.pastaBkp = ConnetctionCrypt.Decriptar(_pastaBkp);

                    //fecha o arquivo BD.Config
                    arquivo.Close();
                }
                else
                {
                    //Caso nao encontrado: 
                    MessageBox.Show("Configure dados de conexao com banco!");

                    //Abre tela para criar BD.config
                    FrmBDConfig frm = new FrmBDConfig();
                    frm.ShowDialog();
                }

                //testar conexao:
                NpgsqlConnection conexao = new NpgsqlConnection();
                conexao.ConnectionString = SqlPostGresServer.StringDeConexao;
                conexao.Open();
                conexao.Close();

                MessageBox.Show("Conexo realizada com sucesso!");

            }
            catch (NpgsqlException erroBanco)
            {
                //caso der erro ao testar conexao mostrar mensagem de erro: o "\n" indica nova linha na messagebox
                MessageBox.Show("Não foi possivel conectar com o Banco de Dados! \n" +
                                 "Informe os parametros da conexão. \nErro:" + erroBanco.Message);

                //Abre tela para criar BD.config
                FrmBDConfig frm = new FrmBDConfig();
                frm.ShowDialog();
            }
            catch (Exception erroS)//caso der erro com dados informados:
            {
                MessageBox.Show("Não foi possivel conectar com o Banco de Dados! \n" +
                                "Informe os parametros da conexão. \nErro:" + erroS.Message);

                //Abre tela para criar BD.config
                FrmBDConfig frm = new FrmBDConfig();
                frm.ShowDialog();
            }
        }

        // EM ANDAMENTO!!!! 
        public static void SalvarDbConfig(string servidor, string porta, string banco, string usuario, string senha)
        {
            //Arquivo *.config:
            string ArquivoBDConfig = SqlPostGresServer.ArquivoBDConfig;
            //Pasta Para BKP:
            string pastaBkp = SqlPostGresServer.PastaBkp;

            try
            {
                //teste se tem algum campo esta vazio vazio:
                if (servidor != "" && porta != "" && banco != "" && usuario != "" && senha != "")
                {

                    #region CRIPTOGRAFIA - ESCREVE OS DADOS NO BD .CONFIG

                    //criar ou abrir uma arquivo no mesmo local do executavel
                    // para guardar as configurações do banco:(Ver para criptogravar a gravação)
                    StreamWriter arquivo = new StreamWriter(ArquivoBDConfig); //TODA VEZ ELE LIMPA O TEXTO ANTIGO  

                    //-------------------------------------------------------------------------------------
                    //CRIPTOGRAFIA -- le as os campos gravar mp comfig:
                    //-------------------------------------------------------------------------------------
                    arquivo.WriteLine(ConnetctionCrypt.Encriptar(servidor)); //escreve a linha como o servidor
                    arquivo.WriteLine(ConnetctionCrypt.Encriptar(porta));
                    arquivo.WriteLine(ConnetctionCrypt.Encriptar(banco));
                    arquivo.WriteLine(ConnetctionCrypt.Encriptar(usuario));
                    arquivo.WriteLine(ConnetctionCrypt.Encriptar(senha));

                    //caminha indicado onde sera salvo o bkp
                    if (pastaBkp != "")
                    {
                        //se a pasta nao existe, cria pasta
                        if (!Directory.Exists(pastaBkp ))
                            Directory.CreateDirectory(pastaBkp);

                        //criptografar o caminho da pasta:
                        arquivo.WriteLine(ConnetctionCrypt.Encriptar(pastaBkp));//pasta padrão bkp
                    }
                    else
                    {
                    }

                    #endregion

                    //libera o arquivo .config:
                    arquivo.Close();

                    //verificar conexão com banco: CAPTURA DADOS DA CONEXAO DO ARQUIVO **.CONFIG
                    SqlPostGresServer.LoadConection();

                    // INSERT DADOS INCIAIS:
                    //Realizar inserts de dados (Dados Iniciais)
                    //SQL_Insert01.Met_ExecutaInsertPadrao();

                    //mostrar mensagem de sucesso
                    System.Windows.MessageBox.Show("Configurações Salvas com sucesso!!");

                }
                else
                {
                    System.Windows.MessageBox.Show("Todos os campos são obrigatorios!!");
                    return;
                }
            }
            catch (Exception erro)
            {
                System.Windows.MessageBox.Show("Erro ao salvar dados da conexao! \n " + erro.Message);
            }
        }
    }
}
