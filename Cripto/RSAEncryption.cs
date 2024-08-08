using System;
using System.Security.Cryptography;
using System.Text;

namespace Atividade1.Cripto
{
    public class RSAEncryption
    {
        public byte[] Encrypt(string message, RSAParameters publicKey)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "Mensagem está Nula.");
            }

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(message);
            byte[] encryptedData;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }

            return encryptedData;
        }

        public string Decrypt(byte[] encryptedMessage, RSAParameters privateKey)
        {
            if (encryptedMessage == null)
            {
                throw new ArgumentNullException(nameof(encryptedMessage), "Mensagem criptografada está Nula.");
            }

            byte[] decryptedData;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                decryptedData = rsa.Decrypt(encryptedMessage, false);
            }

            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}
