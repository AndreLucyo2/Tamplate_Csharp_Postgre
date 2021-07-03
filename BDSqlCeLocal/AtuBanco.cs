using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BDSqlCeLocal
{
    /// <summary>
    /// 
    /// </summary>
    public static class AtuBanco
    {
        //===  METODO PARA CRIAR AS TABELAS DO BANCO DE DADOS SQLCe =====================================================================================
        /// <summary>
        /// Executa SQL de criação das tabelas
        /// </summary>
        /// <param name="strconnBD"></param>
        /// <param name="listInstrucoesSQL"></param>
        public static void ExecutaQueryCreateTable(string strconnBD, List<string> listInstrucoesSQL)
        {
            try
            {
                //Teste se tabela existe:
                bool tabelaJaExiste = false;
                String tabela = "";

                //--------------------------------------------------------------------------------------------------
                //CRIAR TEBELAS: criar as tabelas e parametros SQL 
                //--------------------------------------------------------------------------------------------------
                //estabelece a conexão na base criada: para criacao das tabelas
                SqlCeConnection connectionSQL = new SqlCeConnection(strconnBD);
                connectionSQL.Open();//abre a conexao:

                //Criar comando Sql:
                SqlCeCommand ComandoSQL = new SqlCeCommand();
                ComandoSQL.Connection = connectionSQL;

                //Executar instruções para criar as tabelas: conforme lista de parametros criada ao instanciar o objeto                              
                StringBuilder str = new StringBuilder(); //criar objeto para concatenar as partes da string.

                //faz um loop para montar q query
                foreach (string linhaSQL in listInstrucoesSQL)
                {
                    if (linhaSQL.StartsWith("CREATE TABLE") || linhaSQL.StartsWith("ALTER TABLE")) //se iniciar com ...
                    {
                        tabela = ExtraiNomeTabelaDaQuery(linhaSQL);
                        //Validar se a tabela existir:
                        tabelaJaExiste = !ValidarSeTabelaExiste(strconnBD,tabela );

                        //Reseta query:
                        str = new StringBuilder();
                        str.Append(linhaSQL);

                    }
                    else if (linhaSQL == "GO" || linhaSQL == "FIM") // se ITEM for igual a GO ou FIM!, indica que terminou as instruções
                    {
                        try
                        {
                            //FECHA A CONSTRUÇÃO DA QUERY E EXECUTA-LA:
                            
                            ComandoSQL.CommandText = str.ToString();

                            //Executa se a tabela nao existir:
                            if (tabelaJaExiste)
                            {
                                ComandoSQL.ExecuteNonQuery();//injeta a QUERY
                                
                                //DEBUG
                                //MessageBox.Show($"Tabela {tabela} criada com sucesso!");
                            }

                        }
                        catch (Exception Er)
                        {
                            //DEBUG
                            MessageBox.Show($"Erro ao criar a Tabela {tabela}! \n {str.ToString()}");

                            //Fechar o camando e a ligação:
                            ComandoSQL.Dispose();
                            connectionSQL.Close();
                            connectionSQL.Dispose();
                            throw new Exception($"Erro de Comando SQL! \n {Er.Message}");
                        }
                    }
                    else //caso nao for nenhuma das opções acima: nem fim 
                    {
                        //se a linha nao começa com "CREATE TABLE..."adiciona a string da instrução
                        str.Append(linhaSQL);
                    }

                }

                //fechar o camando e a ligação:
                ComandoSQL.Dispose();
                connectionSQL.Close();
                connectionSQL.Dispose();

            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao Executa Query Create Table SqlCe! \n {erro.Message}");
            }
        }

        #region METODOS PARA MONTAR A ATUALIZAÇÃO DO BD COM IMPORTAÇÃO DE ARQUIVO .SQL

        //===  Atualiza Extrutura do banco - importa arquivo SQL externo ================================================================================
        /// <summary>
        /// Atualiza Extrutura do banco com importação de arquivo SQL externo
        /// </summary>
        public static void AtualizaEstruturaBD(string strconnBD)
        {
            try
            {
                //Busca o arquivo .SQL
                String caminhoArquivo = CaminhoArquivoScript();

                //Lê e importa a lista do arquivo .SQL
                List<string> listInstrucoesSQL = ListScriptArquivoSQL(caminhoArquivo);

                //executa a lista .SQL
                ExecutaQueryCreateTable(strconnBD, listInstrucoesSQL);

                //  >>>>>>>>>>>>>>>>>>>> PAREI AQUI 28/12/2019  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                //Se der sucesso, salvar este aqruivo em um banco de Migrações de versao do BD
                //Criar metodo para controlar as versões do BD em produção
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao atualizar estruturas do BD SqlCe! \n {erro.Message}");
            }
        }

        /// <summary>
        /// Retorna o caminho de uma arquivo .sql
        /// </summary>
        private static string CaminhoArquivoScript()
        {
            string arquivo = "";

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Seleciona Script SQL de Atualização(Ja tratado - FIM ) ";
                openFileDialog.InitialDirectory = BD.pastaAtuBD; // @"c:\Program Files"; //Se ja quiser em abrir em um diretorio especifico
                openFileDialog.Filter = "Script files (*.sql,*.txt)|*.sql;*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == true)//Retorna false quando o botão Cancelar é clicado.
                {
                    arquivo = openFileDialog.FileName;
                }

                //valida se o arquivo existe:
                if (String.IsNullOrEmpty(arquivo))
                {
                    MessageBox.Show("Arquivo Invalido", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);

                    //Encerrar totalmente a aplicação:
                    //Application.Current.Shutdown();

                }
            }
            catch (Exception er)
            {
                //Caso nao encontrado:
                MessageBox.Show($"Arquivo Invalido Err:\n{er.Message}","Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return arquivo;
        }

        /// <summary>
        /// Retorna uma list com a rotina do banco vinda de um arquivo sql:
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        private static List<string> ListScriptArquivoSQL(string caminhoArquivo)
        {
            try
            {
                string txtLinha = "";
                List<string> mensagemLinha = new List<string>();

                //valida se o arquivo existe:
                if (String.IsNullOrEmpty(caminhoArquivo))
                {
                    MessageBox.Show("Arquivo Invalido", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

                    //Encerrar totalmente a aplicação:
                   // Application.Current.Shutdown();

                }
                else
                {
                    //Faz a leitura do arquivo linha por linha.
                    using (StreamReader texto = new StreamReader(caminhoArquivo))
                    {
                        //Fa leitura de todo o arquivo:
                        while ((txtLinha = texto.ReadLine()) != null)
                        {
                            mensagemLinha.Add(txtLinha);
                        }
                    }
                }

                return mensagemLinha;

            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao importar arquivo para ListScriptArquivoSQL! \n{erro.Message}");
            }

        }

        #endregion

        #region UTILITARIOS

        /// <summary>
        /// Extrai o nome da tabela: exemplo :"CREATE TABLE SistemModulo (" 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static string ExtraiNomeTabelaDaQuery(string query)
        {
            try
            {
                //Criar um array de palavras separado pelo espaço
                string[] texto = query.Split(' ');
                query = texto[2];
            }
            catch (Exception erro)
            {
                return query = "";
                throw new Exception($"Erro ao Extrair o nome da tabela! \n{erro.Message}");
            }

            return query.Trim();
        }

        //VALIDAR SE UMA TABELA EXISTE ANTES DE TENTAR CRIA-LA ======================================================
        /// <summary>
        /// Validar se uma tabela existe antes de tentar criar, o SQLCe nao aceita "CREATE TABLE IF NOT EXISTS..."
        /// </summary>
        private static bool ValidarSeTabelaExiste(string strconnBD01, string tabela)
        {
            try
            {
                //Estabelece a conexão
                SqlCeConnection connectionSQL = new SqlCeConnection(strconnBD01);
                //abre a conexao:
                connectionSQL.Open();

                //Criar comando Sql:
                SqlCeCommand ComandoSQL = new SqlCeCommand();
                ComandoSQL.Connection = connectionSQL;

                //Query SQL:
                ComandoSQL.CommandText = "SELECT TOP 1 Count(*) as Cont FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @Tabela";

                //Definir os PARAMETROS DA QUERY
                ComandoSQL.Parameters.AddWithValue("@Tabela", tabela);

                //Ler/Passar os dados retornado da Query:
                //SqlCeDataReader registro = ComandoSQL.ExecuteReader();//SQL Seerver...
                SqlCeDataReader sdr = ComandoSQL.ExecuteResultSet(ResultSetOptions.Scrollable);//SQLCe Correção de erro de Cursores

                //retornou algum registro?
                if (sdr.HasRows)
                {
                    //LER OS DADOS RETORNADOS
                    sdr.Read();

                    //PASSAR OS DADOS DO BANCO PARA O MODELO:
                    int cont = Convert.ToInt32(sdr["Cont"]);
                    sdr.Close();

                    if (cont > 0)
                    {
                        return true;
                    }
                }

                //Fechar o camando e a ligação:
                ComandoSQL.Dispose();
                connectionSQL.Close();
                connectionSQL.Dispose();

                //Fechar o Reader:
                sdr.Close();
                return false;

            }
            catch (Exception Ex)
            {
                throw new Exception($"Erro ao validar se Tabela {tabela}!\n Erro: {Ex.Message}");
            }
            finally
            {

            }
        }

        #endregion
    }
}
