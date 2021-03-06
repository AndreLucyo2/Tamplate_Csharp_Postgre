using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// Criar as tabelas do banco usando SQL padrão ANSI
/// </summary>
namespace BDSqlCeLocal
{
    /// <summary>
    /// Criar o BD01, conform instruções do modelo de dados do Powearchitect
    /// </summary>
    public class BD01_Criar
    {
        // =============================================================================================================
        /// <summary>
        /// Criar as tabelas e inserts do banco o SQL padrão ANSI
        /// </summary>
        /// <param name="strconnBD01"></param>
        public static void Criar_Tabelas_Inserts_BD01(string strconnBD01)
        {
            try
            {
                //int numCmd = 5;
                int cont = 1;

                //Cria as Tabelas no banco BD01:
                AtuBanco.ExecutaQueryCreateTable(strconnBD01, ListScript_CreateTable_Interno_BD01());
                //MessageBox.Show($"[{cont} DE {numCmd}] \n Tebelas BD01x criadas com sucesso!");
                cont += 1;

                //Insert banco BD01:
                BD01_Insert.Executa_Query_Insert_InfBD(strconnBD01);
                //MessageBox.Show($"[{cont} DE {numCmd}] \n Insert Metadados BD com sucesso!");
                cont += 1;

                BD01_Insert.Executa_Query_Insert_Paises(strconnBD01);
                //MessageBox.Show($"[{cont} DE {numCmd}] \n Insert Paises executado com sucesso!");
                cont += 1;

                BD01_Insert.Executa_Query_Insert_Estados(strconnBD01);
                //MessageBox.Show($"[{cont} DE {numCmd}] \n Insert Estados executado com sucesso!");
                cont += 1;

                BD01_Insert.Executa_Query_Insert_Cidades(strconnBD01);
                //MessageBox.Show($"[{cont} DE {numCmd}] \n Insert Cidades executado com sucesso!");
                cont += 1;

                MessageBox.Show("Banco de dados Criado com suecesso!\nInserts Padrão executado com sucesso!");

            }
            catch (Exception Ex)
            {
                //Teste:
                MessageBox.Show($"Erro ao Tentar criar TABELAS na BaseDados SQLCe:-> \n{Ex.Message}");

                //Fazer !! Caso der algum erro apagar o arquivo do banco de dados!!!!

                throw new Exception($"Erro ao Tentar criar TABELAS na BaseDados SQLCe:-> \n{Ex.Message}");
            }
        }

        /// <summary>
        /// Retorna uma lista de instruções das tabelas: Modelar dados no PowerQuery 
        /// </summary>
        private static List<string> ListScript_CreateTable_Interno_BD01()
        {
            try
            {
                //--------------------------------------------------------------------------------------------------
                //criar lista de instruções das tabelas: Modelar dados no PowerQuery 
                //--------------------------------------------------------------------------------------------------
                List<string> listInstrucoesSQL = new List<string>()
                {
                    //ATUALIZADO DIA: 21/12/2020 - 22:45:58
                    "CREATE TABLE GrupoDeCliente ( ",
                    "                idGrupoCli INT IDENTITY NOT NULL, ",
                    "                idSubGrupoCli INT DEFAULT 0 NOT NULL, ",
                    "                grupo NVARCHAR(100) NOT NULL, ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(1000), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT GrupoDeCliente_pk PRIMARY KEY (idGrupoCli) ",
                    ") ",
                    "GO",
                    "CREATE TABLE SystemInfoBD ( ",
                    "                idInfoBD INT IDENTITY NOT NULL, ",
                    "                nome NVARCHAR(100), ",
                    "                versao NVARCHAR(20), ",
                    "                dtCriacao NVARCHAR(25) DEFAULT GETDATE() NOT NULL, ",
                    "                dtUpEstruturas DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                CONSTRAINT SystemInfoBD_pk PRIMARY KEY (idInfoBD) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Sistem ( ",
                    "                idSistem INT IDENTITY NOT NULL, ",
                    "                idInfoBD INT NOT NULL, ",
                    "                nome NVARCHAR(100), ",
                    "                versao NVARCHAR(20), ",
                    "                CONSTRAINT Sistem_pk PRIMARY KEY (idSistem) ",
                    ") ",
                    "GO",
                    "CREATE TABLE LogSystem ( ",
                    "                idLogSis INT IDENTITY NOT NULL, ",
                    "                idSistem INT DEFAULT 0 NOT NULL, ",
                    "                idUsuario NVARCHAR NOT NULL, ",
                    "                tipoAcao INT, ",
                    "                descricaoLog NVARCHAR(100), ",
                    "                log NVARCHAR(4000), ",
                    "                dataLog DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT LogSystem_pk PRIMARY KEY (idLogSis) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Setor ( ",
                    "                idSetor INT IDENTITY NOT NULL, ",
                    "                setor NVARCHAR NOT NULL, ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(1000), ",
                    "                emailSetor NVARCHAR(500), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Setor_pk PRIMARY KEY (idSetor) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Cargo ( ",
                    "                idCargo INT IDENTITY NOT NULL, ",
                    "                cargo NVARCHAR(50) NOT NULL, ",
                    "                salarioBase DECIMAL, ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(1000), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Cargo_pk PRIMARY KEY (idCargo) ",
                    ") ",
                    "GO",
                    "CREATE TABLE CarteiraDeCliente ( ",
                    "                idCarteira INT IDENTITY NOT NULL, ",
                    "                carteira NVARCHAR(100) NOT NULL, ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(1000), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT CarteiraDeCliente_pk PRIMARY KEY (idCarteira) ",
                    ") ",
                    "GO",
                    "CREATE TABLE EnderecoPais ( ",
                    "                idPais INT IDENTITY NOT NULL, ",
                    "                nome NVARCHAR(100) NOT NULL, ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR, ",
                    "                CONSTRAINT EnderecoPais_pk PRIMARY KEY (idPais) ",
                    ") ",
                    "GO",
                    "CREATE TABLE EnderecoEstado ( ",
                    "                idEstado INT IDENTITY NOT NULL, ",
                    "                idPais INT DEFAULT 0 NOT NULL, ",
                    "                codIBGE NVARCHAR(10), ",
                    "                sigla NVARCHAR(2) NOT NULL, ",
                    "                nome NVARCHAR(100) NOT NULL, ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR, ",
                    "                CONSTRAINT EnderecoEstado_pk PRIMARY KEY (idEstado) ",
                    ") ",
                    "GO",
                    "CREATE TABLE EnderecoCidade ( ",
                    "                idCidade INT IDENTITY NOT NULL, ",
                    "                idEstado INT DEFAULT 0 NOT NULL, ",
                    "                nome NVARCHAR(100) NOT NULL, ",
                    "                codIBGE NVARCHAR(10), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR, ",
                    "                CONSTRAINT EnderecoCidade_pk PRIMARY KEY (idCidade) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Endereco ( ",
                    "                idEndereco INT IDENTITY NOT NULL, ",
                    "                idCidade INT DEFAULT 0 NOT NULL, ",
                    "                bairro NVARCHAR(150) NOT NULL, ",
                    "                cep NVARCHAR(12), ",
                    "                rua NVARCHAR(300), ",
                    "                numero NVARCHAR(12), ",
                    "                complemento NVARCHAR(300), ",
                    "                pontoReferencia NVARCHAR(100), ",
                    "                observacao NVARCHAR(500), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Endereco_pk PRIMARY KEY (idEndereco) ",
                    ") ",
                    "GO",
                    "CREATE TABLE EnderecoTipo ( ",
                    "                idTipoEndereco INT IDENTITY NOT NULL, ",
                    "                idEndereco INT DEFAULT 0 NOT NULL, ",
                    "                tipoEndereco INT, ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(500), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT EnderecoTipo_pk PRIMARY KEY (idTipoEndereco) ",
                    ") ",
                    "GO",
                    "CREATE TABLE PessoaJuridica ( ",
                    "                idDgPJ INT IDENTITY NOT NULL, ",
                    "                idTipoEndereco INT DEFAULT 0 NOT NULL, ",
                    "                nomeFantasia NVARCHAR(150) NOT NULL, ",
                    "                razaoSocial NVARCHAR(200), ",
                    "                cnpj NVARCHAR(20) NOT NULL, ",
                    "                inscricaoEstadual NVARCHAR(20) DEFAULT 'ISENTO' NOT NULL, ",
                    "                inscricaoMunicipal NVARCHAR(20) DEFAULT 'ISENTO' NOT NULL, ",
                    "                tributacao INT DEFAULT 0 NOT NULL, ",
                    "                email NVARCHAR(500), ",
                    "                homePage NVARCHAR(250), ",
                    "                numWhatsapp NVARCHAR(20), ",
                    "                telefone NVARCHAR(20), ",
                    "                celular NVARCHAR(20), ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(1000), ",
                    "                logoA NVARCHAR(350), ",
                    "                logoB NVARCHAR(350), ",
                    "                logoC NVARCHAR(350), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT PessoaJuridica_pk PRIMARY KEY (idDgPJ) ",
                    ") ",
                    "GO",
                    "CREATE TABLE PessoaFisica ( ",
                    "                idPF INT IDENTITY NOT NULL, ",
                    "                idTipoEndereco INT DEFAULT 0 NOT NULL, ",
                    "                nome NVARCHAR(150) NOT NULL, ",
                    "                sobreNome NVARCHAR(150), ",
                    "                genero INT, ",
                    "                cpf NVARCHAR(18) NOT NULL, ",
                    "                rg NVARCHAR(20) DEFAULT 'NAO_INFORMADO' NOT NULL, ",
                    "                dtNascimento DATETIME, ",
                    "                email NVARCHAR(500) NOT NULL, ",
                    "                numWhatsapp NVARCHAR(20), ",
                    "                telefone NVARCHAR(20), ",
                    "                celular NVARCHAR(20), ",
                    "                fotoA NVARCHAR(350), ",
                    "                fotoB NVARCHAR(350), ",
                    "                fotoC NVARCHAR(350), ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(1000), ",
                    "                ativo BIT NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT PessoaFisica_pk PRIMARY KEY (idPF) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Colaborador ( ",
                    "                idColaborador INT IDENTITY NOT NULL, ",
                    "                idSetor INT DEFAULT 0 NOT NULL, ",
                    "                idCargo INT DEFAULT 0 NOT NULL, ",
                    "                idFuncao INT DEFAULT 0 NOT NULL, ",
                    "                idPF INT DEFAULT 0 NOT NULL, ",
                    "                acessoSistema BIT NOT NULL, ",
                    "                funcao NVARCHAR(500), ",
                    "                emailCorporativo NVARCHAR(500), ",
                    "                dtAdmissao DATETIME, ",
                    "                dtDemissao DATETIME, ",
                    "                salarioBase DECIMAL, ",
                    "                observacao NVARCHAR(300), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Colaborado PRIMARY KEY (idColaborador) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Usuario ( ",
                    "                idUser INT IDENTITY NOT NULL, ",
                    "                idColaborador INT DEFAULT 0 NOT NULL, ",
                    "                hexLoGin NVARCHAR(1000), ",
                    "                hexSenha NVARCHAR(1000), ",
                    "                hash NVARCHAR(1000), ",
                    "                descricao NVARCHAR(100), ",
                    "                observacao NVARCHAR(1000), ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Usuario_pk PRIMARY KEY (idUser) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Empresa ( ",
                    "                idEmpresa INT IDENTITY NOT NULL, ",
                    "                idPF INT DEFAULT 0 NOT NULL, ",
                    "                idDgPJ INT DEFAULT 0 NOT NULL, ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Empresa_pk PRIMARY KEY (idEmpresa) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Fornecedor ( ",
                    "                idFornecedor INT IDENTITY NOT NULL, ",
                    "                idEmpresa INT NOT NULL, ",
                    "                idPF INT DEFAULT 0 NOT NULL, ",
                    "                idDgPJ INT DEFAULT 0 NOT NULL, ",
                    "                idMatrizFornecedor INT DEFAULT 0 NOT NULL, ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000) NOT NULL, ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Fornecedor_pk PRIMARY KEY (idFornecedor) ",
                    ") ",
                    "GO",
                    "CREATE TABLE SetorEmpresa ( ",
                    "                idSetorEmpresa INT IDENTITY NOT NULL, ",
                    "                idEmpresa INT DEFAULT 0 NOT NULL, ",
                    "                idSetor INT DEFAULT 0 NOT NULL, ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                CONSTRAINT SetorEmpresa_pk PRIMARY KEY (idSetorEmpresa) ",
                    ") ",
                    "GO",
                    "CREATE TABLE Cliente ( ",
                    "                idCliente INT IDENTITY NOT NULL, ",
                    "                idEmpresa INT DEFAULT 0 NOT NULL, ",
                    "                idPF INT DEFAULT 0 NOT NULL, ",
                    "                idDgPJ INT DEFAULT 0 NOT NULL, ",
                    "                idMatriz INT DEFAULT 0 NOT NULL, ",
                    "                idCarteira INT DEFAULT 0 NOT NULL, ",
                    "                idGrupoCli INT DEFAULT 0 NOT NULL, ",
                    "                ativo BIT DEFAULT 0 NOT NULL, ",
                    "                logX NVARCHAR(4000), ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                dtUltimoUpdate DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                registroAdmin NVARCHAR(4000), ",
                    "                CONSTRAINT Cliente_pk PRIMARY KEY (idCliente) ",
                    ") ",
                    "GO",
                    "CREATE TABLE ContatoCliente ( ",
                    "                idContato INT IDENTITY NOT NULL, ",
                    "                idCliente INT DEFAULT 0 NOT NULL, ",
                    "                idPF INT DEFAULT 0 NOT NULL, ",
                    "                dtCadastro DATETIME DEFAULT GETDATE() NOT NULL, ",
                    "                CONSTRAINT ContatoCliente_pk PRIMARY KEY (idContato) ",
                    ") ",
                    "GO",
                    "ALTER TABLE Cliente ADD CONSTRAINT GrupoDeCliente_CarteiraDeClientes_fk ",
                    "FOREIGN KEY (idGrupoCli) ",
                    "REFERENCES GrupoDeCliente (idGrupoCli) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE GrupoDeCliente ADD CONSTRAINT GrupoDeCliente_GrupoDeCliente_fk ",
                    "FOREIGN KEY (idSubGrupoCli) ",
                    "REFERENCES GrupoDeCliente (idGrupoCli) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Sistem ADD CONSTRAINT SysInfoBD_System_fk ",
                    "FOREIGN KEY (idInfoBD) ",
                    "REFERENCES SystemInfoBD (idInfoBD) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE LogSystem ADD CONSTRAINT Sistem_LogSystem_fk ",
                    "FOREIGN KEY (idSistem) ",
                    "REFERENCES Sistem (idSistem) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE SetorEmpresa ADD CONSTRAINT Setor_SetorEmpresa_fk ",
                    "FOREIGN KEY (idSetor) ",
                    "REFERENCES Setor (idSetor) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Colaborador ADD CONSTRAINT Setor_PessoaColaborador_fk ",
                    "FOREIGN KEY (idSetor) ",
                    "REFERENCES Setor (idSetor) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Colaborador ADD CONSTRAINT UserCargo_PessoaFuncionario_fk ",
                    "FOREIGN KEY (idCargo) ",
                    "REFERENCES Cargo (idCargo) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Cliente ADD CONSTRAINT TipoDaCateira_CarteiraDeClientes_fk ",
                    "FOREIGN KEY (idCarteira) ",
                    "REFERENCES CarteiraDeCliente (idCarteira) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE EnderecoEstado ADD CONSTRAINT Pais_Estado_fk ",
                    "FOREIGN KEY (idPais) ",
                    "REFERENCES EnderecoPais (idPais) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE EnderecoCidade ADD CONSTRAINT Estado_Cidade_fk ",
                    "FOREIGN KEY (idEstado) ",
                    "REFERENCES EnderecoEstado (idEstado) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Endereco ADD CONSTRAINT Cidade_Endereco_fk ",
                    "FOREIGN KEY (idCidade) ",
                    "REFERENCES EnderecoCidade (idCidade) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE EnderecoTipo ADD CONSTRAINT Endereco_EnderecoEmpresa_fk ",
                    "FOREIGN KEY (idEndereco) ",
                    "REFERENCES Endereco (idEndereco) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE PessoaFisica ADD CONSTRAINT TipoEndereco_DadosGEraisPF_fk ",
                    "FOREIGN KEY (idTipoEndereco) ",
                    "REFERENCES EnderecoTipo (idTipoEndereco) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE PessoaJuridica ADD CONSTRAINT TipoEndereco_DadosGeraisPJ_fk ",
                    "FOREIGN KEY (idTipoEndereco) ",
                    "REFERENCES EnderecoTipo (idTipoEndereco) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Empresa ADD CONSTRAINT DadosGeraisPJ_Empresa_fk ",
                    "FOREIGN KEY (idDgPJ) ",
                    "REFERENCES PessoaJuridica (idDgPJ) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Cliente ADD CONSTRAINT DadosGeraisPJ_CarteiraDeClientes_fk ",
                    "FOREIGN KEY (idDgPJ) ",
                    "REFERENCES PessoaJuridica (idDgPJ) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Fornecedor ADD CONSTRAINT DadosGeraisPJ_Fornecedor_fk ",
                    "FOREIGN KEY (idDgPJ) ",
                    "REFERENCES PessoaJuridica (idDgPJ) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Empresa ADD CONSTRAINT DadosGEraisPF_Empresa_fk ",
                    "FOREIGN KEY (idPF) ",
                    "REFERENCES PessoaFisica (idPF) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Cliente ADD CONSTRAINT DadosGEraisPF_CarteiraDeClientes_fk ",
                    "FOREIGN KEY (idPF) ",
                    "REFERENCES PessoaFisica (idPF) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Colaborador ADD CONSTRAINT DadosGEraisPF_PessoaColaborador_fk ",
                    "FOREIGN KEY (idPF) ",
                    "REFERENCES PessoaFisica (idPF) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE ContatoCliente ADD CONSTRAINT DadosGEraisPF_ContatoCliente_fk ",
                    "FOREIGN KEY (idPF) ",
                    "REFERENCES PessoaFisica (idPF) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Fornecedor ADD CONSTRAINT DadosGEraisPF_Fornecedor_fk ",
                    "FOREIGN KEY (idPF) ",
                    "REFERENCES PessoaFisica (idPF) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Usuario ADD CONSTRAINT Colaborador_Usuario_fk ",
                    "FOREIGN KEY (idColaborador) ",
                    "REFERENCES Colaborador (idColaborador) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Cliente ADD CONSTRAINT Empresa_CarteiraDeClientes_fk ",
                    "FOREIGN KEY (idEmpresa) ",
                    "REFERENCES Empresa (idEmpresa) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE SetorEmpresa ADD CONSTRAINT Empresa_SetorEmpresa_fk ",
                    "FOREIGN KEY (idEmpresa) ",
                    "REFERENCES Empresa (idEmpresa) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Fornecedor ADD CONSTRAINT Empresa_Fornecedor_fk ",
                    "FOREIGN KEY (idEmpresa) ",
                    "REFERENCES Empresa (idEmpresa) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Fornecedor ADD CONSTRAINT Fornecedor_Fornecedor_fk ",
                    "FOREIGN KEY (idMatrizFornecedor) ",
                    "REFERENCES Fornecedor (idFornecedor) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE Cliente ADD CONSTRAINT CarteiraDeClientes_CarteiraDeClientes_fk ",
                    "FOREIGN KEY (idMatriz) ",
                    "REFERENCES Cliente (idCliente) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",
                    "GO",
                    "ALTER TABLE ContatoCliente ADD CONSTRAINT CarteiraDeClientes_ContatoCliente_fk ",
                    "FOREIGN KEY (idCliente) ",
                    "REFERENCES Cliente (idCliente) ",
                    "ON DELETE NO ACTION ",
                    "ON UPDATE NO ACTION ",



                    "FIM" //*******************************************************************************************

                    //-------------------------------------------------------------------------------------------------
                };

                return listInstrucoesSQL;

            }
            catch (Exception Ex)
            {
                throw new Exception($"Erro ao Tentar criar list SQL:-> \n {Ex.Message}");
            }
        }
    }
}
