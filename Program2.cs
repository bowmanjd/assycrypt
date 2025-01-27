using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        // Generate RSA key pair
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            try
            {
                // Export the public and private keys
                string publicKey = rsa.ToXmlString(false); // Public key
                string privateKey = rsa.ToXmlString(true); // Private key

                Console.WriteLine("Public Key:\n" + publicKey);
                Console.WriteLine("\nPrivate Key:\n" + privateKey);

                // Example encrypted password (replace with the actual encrypted password from JavaScript)
                string encryptedPasswordBase64 = "ENCRYPTED_PASSWORD_FROM_JAVASCRIPT";

                // Decrypt the password
                string decryptedPassword = DecryptPassword(encryptedPasswordBase64, privateKey);
                Console.WriteLine("\nDecrypted Password: " + decryptedPassword);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }

    static string DecryptPassword(string encryptedPasswordBase64, string privateKey)
    {
        using (var rsa = new RSACryptoServiceProvider())
        {
            // Import the private key
            rsa.FromXmlString(privateKey);

            // Convert the base64 encrypted password to bytes
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPasswordBase64);

            // Decrypt the password using RSA-OAEP
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);

            // Convert the decrypted bytes to a string
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
