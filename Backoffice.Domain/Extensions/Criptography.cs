using System.Security.Cryptography;
using System.Text;

namespace Backoffice.Domain.Extensions;

public static class Criptography
{
    public static string EncryptUsingBcrypt(this string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return senhaCriptografada;
    }

    public static string EncryptUsingSHA256(this string objeto)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(objeto);
        byte[] hash = sha256.ComputeHash(bytes);

        string hashString = BitConverter.ToString(hash).Replace("-", "")[..50];

        return hashString;
        
    }
}
