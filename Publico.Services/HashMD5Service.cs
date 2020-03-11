using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Publico.Services {
    // Класс хэширует пароль
    public class HashMD5Service {
        /// <summary>
        /// Метод реализации хэширования
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<string> HashPassword(string password) {
            var getHash = await GetHash(password);
            return getHash;
        }
        /// <summary>
        /// Хэширование пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static async Task <string> GetHash(string password) {
            byte[] hash = Encoding.ASCII.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc) {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}
