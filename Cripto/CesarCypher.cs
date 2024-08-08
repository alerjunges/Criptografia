using System;
using System.IO;
using System.Text;

namespace Atividade1.Cripto
{
    public class CesarCypher
    {
        const string _alfabeto = "abcdefghijklmnopqrstuvwxyz";
        readonly int _alfabetoLength = _alfabeto.Length;

        public string Crypt(string message, int publicKey)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "Mensagem está Nula.");
            }
            else if (message == "")
            {
                return "";
            }
            else
            {
                message = message.ToLower();
                char invalido = GetTextoValido(message);
                if (invalido != ' ')
                {
                    throw new ArgumentOutOfRangeException(nameof(invalido), "Caracter inválido: " + invalido);
                }
            }

            return GetTextoCifrado(message, publicKey);
        }

        public string Decrypt(string cryptedMessage, int privateKey)
        {
            if (cryptedMessage == null)
            {
                throw new ArgumentNullException(nameof(cryptedMessage), "Mensagem está Nula.");
            }
            else if (cryptedMessage == "")
            {
                return "";
            }
            else
            {
                cryptedMessage = cryptedMessage.ToLower();
                char invalido = GetTextoValido(cryptedMessage);
                if (invalido != ' ')
                {
                    throw new ArgumentOutOfRangeException(nameof(invalido), "Caracter inválido: " + invalido);
                }
            }

            return GetTextoDecifrado(cryptedMessage, privateKey);
        }

        public void CryptFile(string inputFilePath, string outputFilePath, int publicKey)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException("Arquivo de entrada não encontrado.", inputFilePath);
            }

            string content = File.ReadAllText(inputFilePath);
            string encryptedContent = Crypt(content, publicKey);
            File.WriteAllText(outputFilePath, encryptedContent);
        }

        public void DecryptFile(string inputFilePath, string outputFilePath, int privateKey)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException("Arquivo de entrada não encontrado.", inputFilePath);
            }

            string content = File.ReadAllText(inputFilePath);
            string decryptedContent = Decrypt(content, privateKey);
            File.WriteAllText(outputFilePath, decryptedContent);
        }

        private char GetTextoValido(string message)
        {
            foreach (char c in message)
            {
                if (!_alfabeto.Contains(c) && c != ' ' && !Char.IsNumber(c))
                {
                    return c; // Retorna o primeiro caractere inválido encontrado
                }
            }
            return ' '; // Retorna espaço indicando que todos os caracteres são válidos
        }

        private string GetTextoCifrado(string texto, int chave)
        {
            StringBuilder textoCifrado = new StringBuilder();

            foreach (char c in texto)
            {
                textoCifrado.Append(GetLetraCifrada(c, chave));
            }

            return textoCifrado.ToString();
        }

        private string GetTextoDecifrado(string textoCriptografado, int chave)
        {
            StringBuilder textoDecifrado = new StringBuilder();

            foreach (char c in textoCriptografado)
            {
                textoDecifrado.Append(GetLetraDecifrada(c, chave));
            }

            return textoDecifrado.ToString();
        }

        private char GetLetraCifrada(char letra, int chave)
        {
            int index = _alfabeto.IndexOf(letra);

            if (index >= 0)
            {
                index = (index + chave) % _alfabetoLength;
                return _alfabeto[index];
            }
            return letra;
        }

        private char GetLetraDecifrada(char letraCifrada, int chave)
        {
            int index = _alfabeto.IndexOf(letraCifrada);

            if (index >= 0)
            {
                index = (index - chave + _alfabetoLength) % _alfabetoLength;
                return _alfabeto[index];
            }
            return letraCifrada;
        }
    }
}
