using System;

namespace Atividade1.Cripto
{
    public class CesarKey
    {
        public int PublicKey { get; private set; }
        public int PrivateKey { get; private set; }

        public CesarKey()
        {
            GenerateKeys();
        }

        private void GenerateKeys()
        {
            // A cifra de César normalmente usa a mesma chave para criptografar e descriptografar,
            // mas vamos criar um sistema onde a chave privada é o complemento de 26 da chave pública.
            Random random = new Random();
            PublicKey = random.Next(1, 25); // Escolhe uma chave entre 1 e 25
            PrivateKey = 26 - PublicKey; // A chave privada é o complemento de 26
        }
    }
}
