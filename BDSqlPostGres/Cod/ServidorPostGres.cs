using BDSqlPostGres.View;
using Npgsql;
using System;
using System.IO;
using System.Windows;

namespace BDSqlPostGres.Cod
{
    public static class ServidorPostGres
    {
        //Dado do Arquivo de configurações *.config:
        private static string _arquivoBDConfig = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "BD.config";

        //pasta meus documentos Geral:
        private static string _pastaBkp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BKP_Banco";

        //Campos da conexão:
        public static string servidor { get; set; }   //IP Servidor
        public static string porta { get; set; }      //porta
        public static string banco { get; set; }      //nome do banco de dados
        public static string usuario { get; set; }   //nome do administrador
        public static string senha { get; set; }     //senha do administrador
        public static string pastaBkp { get; set; }  //Pasta para gerar bkp do banco

        //Objetos da conexao:
        private static string StrConn { get; set; }
        private static NpgsqlTransaction SqlTransaction { get; set; }
        public static NpgsqlConnection Conexao { get; private set; }
        public static NpgsqlCommand SqlCmd { get; private set; }
        public static NpgsqlDataReader SqlDtReader { get; private set; }
        public static NpgsqlDataAdapter SqlDtAdapter { get; private set; }
        public static DateTime DataServerBD { get; set; } // = DateTime.MinValue;

        //Monta  e retorna a String da conexao:
        public static string GetStrConn
        {
            get
            {
                //vai retornar a string conexão com o banco de dados:
                //montar a string e conexão usando os campos do arquivo config
                StrConn = "Server=" + servidor + ";Port=" + porta + ";User Id=" + usuario + ";Password=" + senha + ";Database=" + banco;
                return StrConn;
            }
        }

        /// <summary>
        /// Inicia uma nova conexao se ja nao tiver uma em andamento
        /// </summary>
        public static void SetNewsqlConnection()
        {
            try
            {
                if (Conexao == null)
                {
                    Conexao = new NpgsqlConnection();
                    Conexao.ConnectionString = StrConn;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar conexão!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }
        }

        public static void AbreConexao()
        {
            try
            {
                //Garante a conexao:
                SetNewsqlConnection();

                if (Conexao != null && Conexao.State == System.Data.ConnectionState.Closed)
                {
                    Conexao.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Conectar no BD!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }
        }

        public static void FecharConexao()
        {
            try
            {
                //DEVE existir uma conexão
                if (Conexao != null)
                {
                    //a transação DEVE estar encerrada e a conexao deve estar aberta...
                    if (SqlTransaction == null && Conexao.State == System.Data.ConnectionState.Open)
                    {
                        Conexao.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao DesConectar do BD!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }
        }

        /// <summary>
        /// Retorna uma String com o status da conexao
        /// </summary>
        /// <returns></returns>
        public static string StatusConexaoMsg()
        {
            if (Conexao == null)
            {
                return "Conexão null!";
            }

            if (SqlTransaction != null)
            {
                if (SqlTransaction.Connection.State == System.Data.ConnectionState.Open)
                {
                    return "Conexão com Transação Open!";
                }
            }

            if (Conexao.State == System.Data.ConnectionState.Open)
            {
                return "Conexão Open!";
            }

            return "Conexão Close!";
        }

        /// <summary>
        /// Definir um novo SQLcomand
        /// </summary>
        public static NpgsqlCommand GetNewSqlCmd()
        {
            SqlCmd = new NpgsqlCommand();
            return SqlCmd;
        }

        /// <summary>
        /// Definir um novo DataReader
        /// </summary>
        public static NpgsqlDataReader GetNewSqlDtReader(NpgsqlCommand cmd)
        {
            try
            {
                //Se for transação add comand na fila
                AddComandConectionOrTransaction(cmd);

                //SQLCe: Correção de erro de Cursores/ponteiros
                NpgsqlDataReader sdr = cmd.ExecuteReader();
                SqlDtReader = sdr;
                return sdr;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter DataReader!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }
        }

        /// <summary>
        /// Definir um novo DataAdapter
        /// </summary>
        public static NpgsqlDataAdapter GetNewSqlDtAdapter(string sql)
        {
            try
            {
                //Garante a conexao
                AbreConexao();

                NpgsqlDataAdapter sda = new NpgsqlDataAdapter(sql,Conexao);
                return sda;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter DataAdapter!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }
        }

        /// <summary>
        /// Abre conexao e Inicia uma tranzação faz Conexao.BeginTransaction()
        /// </summary>
        public static void IniciarTransacao()
        {
            try
            {
                //Garante a conexao
                AbreConexao();

                if (SqlTransaction == null)
                {
                    // AbreConexao();
                    //PodeCommitar(false);
                    SqlTransaction = Conexao.BeginTransaction();
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Iniciar Transaction SQL!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }

        }

        /// <summary>
        /// Se CommitTransaction = true Commita a transaction e fecha a conexão
        /// </summary>
        public static void TerminarTransacao()
        {
            try
            {
                if (SqlTransaction == null)
                {
                    throw new Exception($"O objeto da transação estava null!");
                }
                //Validar se a transação pode ser finalizada
                if (SqlTransaction.Connection.State == System.Data.ConnectionState.Open)
                {
                    SqlTransaction.Commit();
                    SqlTransaction.Dispose();
                    SqlTransaction = null;
                    //PodeCommitar(false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Terminar a Transaction SQL!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }

        }

        /// <summary>
        /// Faz Rollback da transaction
        /// </summary>
        public static void CancelarTransacao()
        {
            try
            {
                if (SqlTransaction != null && SqlTransaction.Connection.State == System.Data.ConnectionState.Open)
                {
                    SqlTransaction.Rollback();
                    SqlTransaction.Dispose();
                    //PodeCommitar(false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Cencelar a Transaction SQL!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }
        }


        /// <summary>
        /// Adiciona um comando na tranaction corrente ou define a conexao para o comando
        /// </summary>
        public static void AddComandConectionOrTransaction(NpgsqlCommand cmd)
        {    //ValidarSeUsaTransactionSQL
            try
            {
                //Garante a conexao
                AbreConexao();

                if (SqlTransaction != null && SqlTransaction.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection = Conexao;
                    cmd.Transaction = SqlTransaction;
                }
                else
                {
                    cmd.Connection = Conexao;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro adicionar sqlComand na Transaction SQL!\nVerifique com desenvolvedor!\nException: {ex.GetType().FullName}\n{ex.Message}");
            }

        }


        //Monta e retorna o endereço do Arquivo da config:
        public static string GetPathArquivoBDConfig
        {
            //vai retornar a endereço do arquivo da Base SqlCE:
            get { return _arquivoBDConfig; }
        }

        //Monta e retorna o endereço da pasta padrão de bkp:
        public static string GetPathPastaBkp
        {
            //vai retornar a endereço do arquivo da Base SqlCE:
            get { return _pastaBkp; }
        }

        //faz a leitura dos dados da conexão:
        public static void LoadConection()
        {
            try
            {
                //Verfica se *.config Existe:
                if (!File.Exists(ServidorPostGres.GetPathArquivoBDConfig))
                {
                    //Caso nao encontrado: 
                    MessageBox.Show("Configure dados de conexao com banco!");

                    //Abre tela para criar BD.config
                    FrmBDConfig frm = new FrmBDConfig();
                    frm.ShowDialog();
                }

                //Faz leitura do arquivo config
                StreamReader arquivo = new StreamReader(ServidorPostGres.GetPathArquivoBDConfig);

                //faz a leitura do arquivo BD.config e guarda em variaveis: Cuidado com a ordem das linhas
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
                    ServidorPostGres.servidor = ConnetctionCrypt.Decriptar(_servidor);
                    ServidorPostGres.porta = ConnetctionCrypt.Decriptar(_porta);
                    ServidorPostGres.banco = ConnetctionCrypt.Decriptar(_banco);
                    ServidorPostGres.usuario = ConnetctionCrypt.Decriptar(_usuario);
                    ServidorPostGres.senha = ConnetctionCrypt.Decriptar(_senha);
                    ServidorPostGres.pastaBkp = ConnetctionCrypt.Decriptar(_pastaBkp);

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
                conexao.ConnectionString = ServidorPostGres.GetStrConn;
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

        public static void SalvarDbConfig(string servidor, string porta, string banco, string usuario, string senha)
        {
            //Arquivo *.config:
            string ArquivoBDConfig = ServidorPostGres.GetPathArquivoBDConfig;
            //Pasta Para BKP:
            string pastaBkp = ServidorPostGres.GetPathPastaBkp;

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
                    ServidorPostGres.LoadConection();

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
