using AppView.View;
using BDSqlCeLocal;
using BDSqlPostGres;
using BDSqlPostGres.Cod;
using System;
using System.Windows;

namespace AppView
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        //este Metodo controla a tela inicial do app.
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {

                #region CRIAR BDs Locais E/OU TESTA CONEXAO

                //1° - verificar conexão com banco: CAPTURA DADOS DA CONEXAO DO ARQUIVO **.CONFIG
                //---------------------------------------------------------------------------------
                //Cria e Testa a Conexao com Banco de Dados BD01 Local:
                //SQLCeServer.TestarStatusConexao(SQLCeServer.GetStrCon());


                //---------------------------------------------------------------------------------
                //Inicia a verifica a conexao com o Banco:
                SqlPostGresServer.LoadConection();


                #endregion


                //Valida se abre ou nao a tela home apos login:
                bool openhome = false;

                #region LOGIN
                //---------------------------------------------------------------------------------
                bool AtivaLogin = true; //Desativar para testes!   
                if (AtivaLogin == true)
                {
                    //Abre tela de LOG IN
                    MessageBox.Show("Aqui sera chamado tela LogIn!");
                    
                    //login ok!
                    openhome = true;

                }
                else
                {
                    openhome = false;
                }
                //--------------------------------------------------------------------------------------
                #endregion

                //Valida se abre ou nao a tela home
                if (openhome)
                {
                    //Abre home:
                    HomeView wHome = new HomeView();
                    //Setar Home como MainWindow
                    Application.Current.MainWindow = wHome;
                    //Enqunto tela home estiver aberta aplicação esta em execução
                    wHome.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Ops. Algo deu errado! \n Aplicação sera encerrada!");

                    //Fechar a aplicação
                    this.Shutdown();

                    //Encerrar totalmente a aplicação:
                    Application.Current.Shutdown();

                }


            }
            catch (Exception er) //Trata Caso der erro no login
            {
                MessageBox.Show($"Erro ao iniciar! \n Err: {er.Message}");
            }
            finally //Se Erro encerra a aplicação:
            {
                //Fechar a aplicação
                this.Shutdown();

                if (System.Windows.Application.Current != null)
                {
                    //Encerrar totalmente a aplicação:
                    Application.Current.Shutdown();
                }

            }
        }

    }
}
