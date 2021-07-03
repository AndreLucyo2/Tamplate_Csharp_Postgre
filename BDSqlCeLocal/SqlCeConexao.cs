using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Text;


/*===================================================================================================================================================
      Classe StrSqlCeConexao, camada que cria e define a conexao com a Base de Dados 
      Versao 2.0.0
      Data Criação: ABR/2019
      Autor/Editor: andre.lucyo.sylva@gmail.com
      Descrição:
          Esta classe se destina criar e definir a conexao com a base de dados,
          O mecanismo é composto de duas classes principais, uma classe define somente a conexao e a String de conexão
          e esta classe os comandos, "conectar" ... "desconectar"... "transactions" ...
          Quando for Transaction a conecxão é definida na camada VIEW (Cod Bihind)
          o restante na bamada DAL.
//===================================================================================================================================================*/


namespace BDSqlCeLocal
{
    /// <summary>
    /// Classe responsavel pela conexão com o banco
    /// </summary>
    public class SqlCeConexao
    {
        private static SqlCeConexao _conexaoSqlCe;

        /// <summary>
        /// Dados privado armazena a string de conexão
        /// </summary>
        private String _stringConexao;

        /// <summary>
        /// Dado Privado cria a comando para conexao
        /// </summary>
        private SqlCeConnection _conexao;

        // TRANSAÇÃO SQL Ações que envolvem mais de uma tabela em sequencia -------------------------------------------------------------------------
        /// <summary>
        /// TRANSAÇÃO SQL,criar movimentação no banco, garante integridade de dados
        /// </summary>
        private SqlCeTransaction _transaction;//criar movimentação no banco, garante integridade de dados

        //===========================================================================================================================================
        /// <summary>
        /// Construtor recebe a string da conexão ja definida
        /// </summary>
        public SqlCeConexao(String strConexao) //Construtor recebe a string da conexão ja definida
        {
            try
            {
                this._conexao = new SqlCeConnection();// Caomando  SQL que cria a conexao com o banco
                this.SetStringConection(strConexao); //recebe a string da conexão da classe DadosDaConexão
                this._conexao.ConnectionString = strConexao; //definir a estring que vai utilizar 
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao criar conexão! \n" + erro.Message);
            }
        }

        /// <summary>
        /// Retorna uma conexão definida para a DAL:
        /// </summary>
        public static SqlCeConexao GetConexaoSqlCe()
        {
            return _conexaoSqlCe;
        }

        /// <summary>
        /// Retorna uma conexão definida para a DAL:
        /// </summary>
        private static void SetConexaoSqlCe(SqlCeConexao value)
        {
            _conexaoSqlCe = value;
        }

        //===========================================================================================================================================
        /// <summary>
        /// Metodo recebe uma conexão definida na camada View para que a DAL possa utilizar
        /// </summary>
        public static void CriarSqlCeConection(SqlCeConexao ConexaoSQL)
        {
            try
            {
                SetConexaoSqlCe(ConexaoSQL);
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Defnir a conexão! \n" + erro.Message);
            }
        }

        //===========================================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public string GetStringConection()
        { 
            return this._stringConexao; 
        }

        //===========================================================================================================================================
        /// <summary>
        /// 
        /// </summary>
        public void SetStringConection(string value)
        { 
            this._stringConexao = value; 
        }

        //===========================================================================================================================================
        /// <summary>
        /// propriedade pegar o valor da conexão, setar e pegar  o valor ca conexão (Encapsular)
        /// </summary>
        public SqlCeConnection GetObjConection()
        { 
            return this._conexao; 
        }

        //===========================================================================================================================================
        /// <summary>
        /// propriedade pegar o valor da conexão, setar e pegar  o valor ca conexão (Encapsular)
        /// </summary>
        public void SetObjConection(SqlCeConnection value)
        { 
            this._conexao = value; 
        }

        //CONECTAR NO BANCO  =========================================================================================================================
        /// <summary>
        /// conectar
        /// </summary>
        public void Conectar()
        {
            try
            {
                this._conexao.Open();
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Conectar no BD! \n" + erro.Message);
            }

        }

        //DESCONECTAR BANCO  ========================================================================================================================
        /// <summary>
        /// desconectar
        /// </summary>
        public void Desconectar()
        {
            try
            {
                this._conexao.Close();
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao DesConectar do BD! \n" + erro.Message);
            }
        }

        //===========================================================================================================================================
        // TRANSAÇÃO SQL Ações que envolvem mais de uma tabela em sequencia -------------------------------------------------------------------------
        //===========================================================================================================================================

        //Objeto responsavel pela movimentação de dados entre tabela no banco  ======================================================================
        /// <summary>
        ///  Objeto responsavel pela movimentação de dados entre tabela no banco
        /// </summary>
        public SqlCeTransaction GetTransaction()
        { 
            return this._transaction; 
        }

        /// <summary>
        ///  Objeto responsavel pela movimentação de dados entre tabela no banco
        /// </summary>
        public void SetTransaction(SqlCeTransaction value)
        { 
            this._transaction = value; 
        }

        //INICIAR TRANSAÇÃO DO BANCO - Marca o ponto inicial de uma transação ========================================================================
        /// <summary>
        /// Marca o ponto inicial de uma transação
        /// </summary>
        public void IniciarTransacao()
        {
            try
            {
                this._transaction = _conexao.BeginTransaction();
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Iniciar Transaction SQL! \n" + erro.Message);
            }

        }

        //TERMINAR TRANSAÇÃO DO BANCO - efetivar a alterações no banco Marca o fim da transação  ======================================================
        /// <summary>
        /// efetivar a alterações no banco Marca o fim da transação
        /// </summary>
        public void TerminarTransacao()
        {
            try
            {
                this._transaction.Commit();
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Terminar a Transaction SQL! \n" + erro.Message);
            }

        }

        //CENCELAR TRANSAÇÃO - Desfaz todas as alterações caso der erro Reverte uma transação ========================================================
        /// <summary>
        /// Desfaz todas as alterações caso der erro Reverte uma transação
        /// </summary>
        public void CancelarTransacao()
        {
            try
            {
                this._transaction.Rollback();
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Cencelar a Transaction SQL! \n" + erro.Message);
            }

        }
    }
}
