using System;
using System.Security.Cryptography;
using System.Text;

namespace MyWorkWebsite
{
    public static class BCryptHelper
    {
        private const int HashSize = 32; // 哈希大小
        private const int Iterations = 10000; // 迭代次數，增加安全性
        private static readonly byte[] FixedSalt = Encoding.UTF8.GetBytes("MyFixedSalt"); // 固定鹽值，至少 8 字節

        /// <summary>
        /// 產生哈希密碼（使用固定鹽值，不儲存鹽）
        /// </summary>
        /// <param name="password">原始密碼</param>
        /// <returns>哈希後的字串（Base64 格式）</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            // 使用 PBKDF2 哈希，以固定鹽
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, FixedSalt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                // 直接轉成 Base64 儲存（無需組合鹽）
                return Convert.ToBase64String(hash);
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

            byte[] storedHash = Convert.FromBase64String(hashedPassword);

            // 重新計算哈希，使用相同固定鹽
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, FixedSalt, Iterations))
            {
                byte[] testHash = pbkdf2.GetBytes(HashSize);

                // 比較
                if (testHash.Length != storedHash.Length)
                    return false;

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