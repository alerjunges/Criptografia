using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Atividade1.Cripto
{
    class Program
    {
        static void Main(string[] args)
        {
            CesarCypher cesarCypher = new CesarCypher();
            CesarKey cesarKeys = new CesarKey();
            RSAEncryption rsaEncryption = new RSAEncryption();
            RSAKeyPair rsaKeys = new RSAKeyPair();

            Console.WriteLine("Programa de Criptografia");
            Console.WriteLine("1 - Cifra de César (Simétrico)");
            Console.WriteLine("2 - RSA (Assimétrico)");
            Console.WriteLine("Escolha o método de criptografia:");

            string method = Console.ReadLine();

            if (method == "1")
            {
                Console.WriteLine($"Método Simétrico: Cifra de César");
                Console.WriteLine($"Chave Pública: {cesarKeys.PublicKey}");
                Console.WriteLine($"Chave Privada: {cesarKeys.PrivateKey}");
            }
            else if (method == "2")
            {
                Console.WriteLine($"Método Assimétrico: RSA");
                Console.WriteLine("Chave Pública: " + Convert.ToBase64String(rsaKeys.PublicKey.Modulus));
                Console.WriteLine("Chave Privada: " + Convert.ToBase64String(rsaKeys.PrivateKey.D));
            }
            else
            {
                Console.WriteLine("Método inválido. Encerrando.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1 - Criptografar uma mensagem");
                Console.WriteLine("2 - Descriptografar uma mensagem");
                Console.WriteLine("3 - Criptografar um arquivo");
                Console.WriteLine("4 - Descriptografar um arquivo");
                Console.WriteLine("5 - Sair");

                string option = Console.ReadLine();

                if (option == "5")
                {
                    break;
                }

                try
                {
                    switch (option)
                    {
                        case "1":
                            Console.WriteLine("Digite a mensagem:");
                            string message = Console.ReadLine();

                            if (method == "1")
                            {
                                string encryptedMessage = cesarCypher.Crypt(message, cesarKeys.PublicKey);
                                Console.WriteLine($"Mensagem Criptografada: {encryptedMessage}");
                            }
                            else if (method == "2")
                            {
                                byte[] encryptedMessage = rsaEncryption.Encrypt(message, rsaKeys.PublicKey);
                                Console.WriteLine($"Mensagem Criptografada: {Convert.ToBase64String(encryptedMessage)}");
                            }
                            break;

                        case "2":
                            Console.WriteLine("Digite a mensagem criptografada:");

                            if (method == "1")
                            {
                                string cryptedMessage = Console.ReadLine();
                                string decryptedMessage = cesarCypher.Decrypt(cryptedMessage, cesarKeys.PrivateKey);
                                Console.WriteLine($"Mensagem Descriptografada: {decryptedMessage}");
                            }
                            else if (method == "2")
                            {
                                byte[] cryptedMessage = Convert.FromBase64String(Console.ReadLine());
                                string decryptedMessage = rsaEncryption.Decrypt(cryptedMessage, rsaKeys.PrivateKey);
                                Console.WriteLine($"Mensagem Descriptografada: {decryptedMessage}");
                            }
                            break;

                        case "3":
                            Console.WriteLine("Digite o caminho do arquivo de entrada:");
                            string inputFileEncrypt = Console.ReadLine();
                            Console.WriteLine("Digite o caminho do arquivo de saída:");
                            string outputFileEncrypt = Console.ReadLine();

                            if (method == "1")
                            {
                                cesarCypher.CryptFile(inputFileEncrypt, outputFileEncrypt, cesarKeys.PublicKey);
                            }
                            else if (method == "2")
                            {
                                byte[] content = File.ReadAllBytes(inputFileEncrypt);
                                byte[] encryptedContent = rsaEncryption.Encrypt(Encoding.UTF8.GetString(content), rsaKeys.PublicKey);
                                File.WriteAllBytes(outputFileEncrypt, encryptedContent);
                            }
                            Console.WriteLine("Arquivo criptografado com sucesso!");
                            break;

                        case "4":
                            Console.WriteLine("Digite o caminho do arquivo de entrada:");
                            string inputFileDecrypt = Console.ReadLine();
                            Console.WriteLine("Digite o caminho do arquivo de saída:");
                            string outputFileDecrypt = Console.ReadLine();

                            if (method == "1")
                            {
                                cesarCypher.DecryptFile(inputFileDecrypt, outputFileDecrypt, cesarKeys.PrivateKey);
                            }
                            else if (method == "2")
                            {
                                byte[] encryptedContent = File.ReadAllBytes(inputFileDecrypt);
                                string decryptedContent = rsaEncryption.Decrypt(encryptedContent, rsaKeys.PrivateKey);
                                File.WriteAllText(outputFileDecrypt, decryptedContent);
                            }
                            Console.WriteLine("Arquivo descriptografado com sucesso!");
                            break;

                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }

            Console.WriteLine("Programa encerrado.");
        }
    }
}
