using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Atividade1.Cripto
{
    public class RSAKeyPair
    {
        public RSAParameters PublicKey { get; private set; }
        public RSAParameters PrivateKey { get; private set; }

        public RSAKeyPair()
        {
            GenerateKeys();
        }

        private void GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                PublicKey = rsa.ExportParameters(false);  // Export only the public key
                PrivateKey = rsa.ExportParameters(true);  // Export both public and private keys
            }
        }
    }
}
