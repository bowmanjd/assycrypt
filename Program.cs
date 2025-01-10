using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

public string DecryptPassword(string encryptedPassword, AsymmetricKeyParameter privateKey)
{
    var cipher = new OaepEncoding(new RsaEngine());
    cipher.Init(false, privateKey);

    byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
    byte[] decryptedBytes = cipher.ProcessBlock(encryptedBytes, 0, encryptedBytes.Length);

    return Encoding.UTF8.GetString(decryptedBytes);
}

