using System.Text;
using Microsoft.Extensions.Configuration;

namespace Unsplash.Utilities
{
    public static class Encryptor
    {
        public static string EncryptName(string name, string key)
        {
            char[] nameBytes = name.ToCharArray();
            char[] keyBytes = key.ToCharArray();
            char[] encrypted = new char[nameBytes.Length];

            for (int i = 0; i < nameBytes.Length; i++)            
            {
                encrypted[i] = (char)(nameBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return new string(encrypted);
        }
    }
}