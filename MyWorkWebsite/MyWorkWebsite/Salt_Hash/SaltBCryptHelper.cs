using System;
using System.Security.Cryptography;
using System.Text;

namespace MyWorkWebsite
{
    public static class SaltBCryptHelper
    {
        private const int SaltSize = 8; // 鹽值大小
        private const int HashSize = 32; // 哈希大小
        private const int Iterations = 10000; // 迭代次數，增加安全性

        /// <summary>
        /// 產生哈希密碼（包含鹽值）
        /// </summary>
        /// <param name="password">原始密碼</param>
        /// <returns>哈希後的字串（Base64 格式，包含鹽值）</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            // 產生隨機鹽值
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // 使用 PBKDF2 哈希
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // 組合鹽值 + 哈希
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // 轉成 Base64 儲存
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// 驗證密碼是否匹配哈希
        /// </summary>
        /// <param name="password">輸入密碼</param>
        /// <param name="hashedPassword">儲存的哈希</param>
        /// <returns>是否匹配</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // 取出鹽值
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // 取出哈希
            byte[] storedHash = new byte[HashSize];
            Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);

            // 重新計算哈希
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] testHash = pbkdf2.GetBytes(HashSize);

                // 比較
                for (int i = 0; i < HashSize; i++)
                {
                    if (storedHash[i] != testHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}