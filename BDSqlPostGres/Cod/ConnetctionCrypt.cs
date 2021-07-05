using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BDSqlPostGres.Cod
{
    public static class ConnetctionCrypt
    {
        //KEY :16/24/32
        const string KEY = "AEDFGT84W4G4Y4H4";

        //VETOR:16
        const string VET = "AEDFGT84W4G4Y4H4";


        // CriarInstanciaRijndael: responsável pela criação de novas instâncias da
        // classe Rijndael (namespace System.Security.Cryptography)
        private static Rijndael CriarInstanciaRijndael(string chave, string vetorInicializacao)
        {
            if (!(chave != null &&
                  (chave.Length == 16 ||
                   chave.Length == 24 ||
                   chave.Length == 32)))
            {
                throw new Exception("A chave de criptografia deve possuir 16, 24 ou 32 caracteres.");
            }

            if (vetorInicializacao == null ||
                vetorInicializacao.Length != 16)
            {
                throw new Exception("O vetor de inicialização deve possuir 16 caracteres.");
            }

            Rijndael algoritmo = Rijndael.Create();
            algoritmo.Key = Encoding.ASCII.GetBytes(chave);
            algoritmo.IV = Encoding.ASCII.GetBytes(vetorInicializacao);

            return algoritmo;
        }

        //Encriptar: realiza a criptografia de texto normal, devolvendo como resultado uma string
        //em que os diferentes bytes encriptados estão representados no formato hexadecimal
        public static string Encriptar(string textoNormal)
        {
            string chave = KEY;
            string vetorInicializacao = VET;

            if (String.IsNullOrWhiteSpace(textoNormal))
            {
                throw new Exception("BD Config esta vazio ou inesistente vazia.");
            }

            using (Rijndael algoritmo = CriarInstanciaRijndael(chave, vetorInicializacao))
            {
                ICryptoTransform encryptor = algoritmo.CreateEncryptor(algoritmo.Key, algoritmo.IV);

                using (MemoryStream streamResultado = new MemoryStream())
                {
                    using (CryptoStream csStream = new CryptoStream(streamResultado, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(csStream))
                        {
                            writer.Write(textoNormal);
                        }
                    }

                    return ArrayBytesToHexString(streamResultado.ToArray());
                }
            }
        }

        //ArrayBytesToHexString: converte um array de bytes em uma string, com cada elemento tendo sido
        //transformado em um valor correspondente em texto hexadecimal
        private static string ArrayBytesToHexString(byte[] conteudo)
        {
            string[] arrayHex = Array.ConvertAll(conteudo, b => b.ToString("X2"));
            return string.Concat(arrayHex);
        }

        //Decriptar: efetua a decriptação de uma string, partindo do pressuposto que o valor a ser convertido
        //é formado por uma sequência de caracteres representando o equivalente hexadecimal de um conjunto de bytes
        public static string Decriptar(string textoEncriptado)
        {
            string chave = KEY;
            string vetorInicializacao = VET;

            if (String.IsNullOrWhiteSpace(textoEncriptado))
            {
                throw new Exception("BD.Config esta vazio ou inesistente.");
            }

            if (textoEncriptado.Length % 2 != 0)
            {
                throw new Exception("BD.Config é inválido.");
            }


            using (Rijndael algoritmo = CriarInstanciaRijndael(
                chave, vetorInicializacao))
            {
                ICryptoTransform decryptor =
                    algoritmo.CreateDecryptor(
                        algoritmo.Key, algoritmo.IV);

                string textoDecriptografado = null;
                using (MemoryStream streamTextoEncriptado =
                    new MemoryStream(
                        HexStringToArrayBytes(textoEncriptado)))
                {
                    using (CryptoStream csStream = new CryptoStream(
                        streamTextoEncriptado, decryptor,
                        CryptoStreamMode.Read))
                    {
                        using (StreamReader reader =
                            new StreamReader(csStream))
                        {
                            textoDecriptografado =
                                reader.ReadToEnd();
                        }
                    }
                }

                return textoDecriptografado;
            }
        }

        //HexStringToArrayBytes: converte uma string com valores hexadecimais em um array cujos itens serão do tipo byte.
        private static byte[] HexStringToArrayBytes(string conteudo)
        {
            int qtdeBytesEncriptados =
                conteudo.Length / 2;
            byte[] arrayConteudoEncriptado =
                new byte[qtdeBytesEncriptados];
            for (int i = 0; i < qtdeBytesEncriptados; i++)
            {
                arrayConteudoEncriptado[i] = Convert.ToByte(
                    conteudo.Substring(i * 2, 2), 16);
            }
            return arrayConteudoEncriptado;
        }
    }
}
