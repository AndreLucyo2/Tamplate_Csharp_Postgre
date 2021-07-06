using BDSqlPostGres.Cod;
using Npgsql;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace BDSqlPostGres.View
{
    public partial class FrmBDConfig : Form
    {
        public FrmBDConfig()
        {
            InitializeComponent();
        }

        private void FrmBDConfig_Load(object sender, EventArgs e)
        {
            //Arquivo *.config:
            string ArquivoBDConfig = ServidorPostGres.GetPathArquivoBDConfig;

            //Pasta Para BKP:
            string pastaBkp = ServidorPostGres.GetPathPastaBkp;

            try
            {
                //Verfica se *.config Existe:
                if (!File.Exists(ArquivoBDConfig))
                {
                    //se a pasta nao existe, cria pasta
                    if (!Directory.Exists(pastaBkp))
                        Directory.CreateDirectory(pastaBkp);

                    //Passa o endereço para pasta bkp:
                    TxtPastaPadraoBkp.Text = pastaBkp;

                    return;
                }

                //ler o arquivo BD.config
                StreamReader arquivo = new StreamReader(ArquivoBDConfig);

                //faz a leitura do arquivo BD.config e guarda em variaveis
                string _servidor = arquivo.ReadLine();
                string _porta = arquivo.ReadLine();
                string _banco = arquivo.ReadLine();                
                string _usuario = arquivo.ReadLine();
                string _senha = arquivo.ReadLine();
                string _pastaBkp = arquivo.ReadLine(); 

                //teste de tem algum dado vazio ou null:
                if (_servidor != "" && _banco != "" && _usuario != "" && _senha != "" && _pastaBkp != "" &&
                    _servidor != null && _banco != null && _usuario != null && _senha != null && _pastaBkp != null)
                {
                    //-------------------------------------------------------------------------------------
                    //CRIPTOGRAFIA TIPO 02 - Passa para os dados para a tela
                    //-------------------------------------------------------------------------------------
                    TxtServidor.Text = ConnetctionCrypt.Decriptar(_servidor);
                    TxtPorta.Text = ConnetctionCrypt.Decriptar(_porta);
                    TxtBanco.Text = ConnetctionCrypt.Decriptar(_banco);                    
                    TxtUsuario.Text = ConnetctionCrypt.Decriptar(_usuario);
                    TxtSenha.Text = ConnetctionCrypt.Decriptar(_senha);
                    TxtPastaPadraoBkp.Text = ConnetctionCrypt.Decriptar(_pastaBkp);

                    //fechar o arquivo
                    arquivo.Close();
                }
                else
                {
                    //se a pasta nao existe, cria pasta
                    if (!Directory.Exists(pastaBkp))
                        Directory.CreateDirectory(pastaBkp);

                    //Passa o endereço para pasta bkp:
                    TxtPastaPadraoBkp.Text = pastaBkp;
                    return;
                }

                //fechar o arquivo
                arquivo.Close();

            }
            catch (Exception erro)
            {
                //trata erro ao Decriptar:
                System.Windows.MessageBox.Show("Erro: " + erro.Message);

                //se a pasta nao existe, cria pasta
                if (!Directory.Exists(pastaBkp))
                    Directory.CreateDirectory(pastaBkp);

                //Passa o enredeço para a tela:
                TxtPastaPadraoBkp.Text = pastaBkp;
            }
        }

        private void BtnTestar_Click(object sender, EventArgs e)
        {
            //verificar conexão com banco:
            try
            {
                //teste se tem algum campo vazio:
                if (TxtServidor.Text == "" || TxtBanco.Text == "" || TxtUsuario.Text == "" || TxtSenha.Text == "")
                {
                    System.Windows.MessageBox.Show("Todos os campos são obrigatórios!");
                    return;
                }

                //as informação ja estarão na tela: passa as informações da tela para a conexao
                ServidorPostGres.servidor = TxtServidor.Text;
                ServidorPostGres.porta = TxtPorta.Text;
                ServidorPostGres.banco = TxtBanco.Text;               
                ServidorPostGres.usuario = TxtUsuario.Text;
                ServidorPostGres.senha = TxtSenha.Text;

                //testar conexao: usa a strig conforme dados da tela:
                NpgsqlConnection conexao = new NpgsqlConnection();
                conexao.ConnectionString = ServidorPostGres.GetStrConn;
                conexao.Open();
                conexao.Close();

                System.Windows.MessageBox.Show("Conexao efetuada com sucesso!");

            }
            catch (SqlException erroBanco)//caso der erro de conexção
            {
                
                //caso der erro ao testar conexao mostrar mensagem de erro: o "\n" indica nova linha na messagebox
                System.Windows.MessageBox.Show("Não foi possivel conectar! \n" +
                                 "Verifique os dados informados \n Erro: " + erroBanco.Message);
                return;
            }
            catch (Exception erroS)//caso der erro com dados informados:
            {
                
                //caso der erro ao testar conexao mostrar mensagem de erro: 
                System.Windows.MessageBox.Show("Não foi possivel conectar! \n" +
                                "Verifique os dados informados\n Erro:" + erroS.Message);
                return;
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //teste se tem algum campo esta vazio:
                if (TxtServidor.Text != "" && TxtBanco.Text != "" && TxtPorta.Text != "" && TxtUsuario.Text != "" && TxtSenha.Text != "")
                {
                    ServidorPostGres.SalvarDbConfig(TxtServidor.Text, TxtPorta.Text, TxtBanco.Text, TxtUsuario.Text, TxtSenha.Text);

                    if (TxtPastaPadraoBkp.Text != "")
                    {
                        //se a pasta nao existe, cria pasta
                        if (!Directory.Exists(TxtPastaPadraoBkp.Text))
                        {
                            Directory.CreateDirectory(TxtPastaPadraoBkp.Text);
                        }
                    }
                    else
                    {
                        //Passa o enredeço para a tela:
                       TxtPastaPadraoBkp.Text = ServidorPostGres.pastaBkp;
                    }

                    //fechar a tela:
                    this.Close();
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

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult d = System.Windows.Forms.MessageBox.Show("Encerrar aplicação?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (d.ToString() == "Yes")
            {
                //Encerrar a aplicação:
                System.Windows.Application.Current.Shutdown();
                return;
            }

            System.Windows.MessageBox.Show("Clique em salvar para fechar este tela!");
            return;
        }

        private void BtnBuscaPasta_Click(object sender, EventArgs e)
        {
            //----------------------------------------------------------------------
            //TESTE:
            //var dialog = new System.Windows.Forms.FolderBrowserDialog();
            //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            //------------------------------------------------------------------------

            //Pasta Para BKP:
            string pastaBkp = ServidorPostGres.GetPathPastaBkp;

            //Captura o caminha de uma pasta para sarvar o arquivo de backup, se for digerente da configurada:
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowNewFolderButton = true;//Mostrar BTN criar nova pasta
            folder.ShowDialog();//Mostra janela para busca a pasta      

            //passa o caminho para o texbox:            
            TxtPastaPadraoBkp.Text = folder.SelectedPath;

            //se cancelar vi por standar meus documentos/***
            if (TxtPastaPadraoBkp.Text == "" || TxtPastaPadraoBkp.Text == null)
            {
                //se a pasta nao existe, cria pasta
                if (!Directory.Exists(pastaBkp))
                    Directory.CreateDirectory(pastaBkp);

                TxtPastaPadraoBkp.Text = pastaBkp;

            }
            else if (TxtPastaPadraoBkp.Text.Length <= 4) //nao permitir raiz de driver
            {
                System.Windows.MessageBox.Show("Local inválido! \n \n Selecione outro local para backup!");
                BtnBuscaPasta_Click(sender, e);
            }
        }
    }
}
