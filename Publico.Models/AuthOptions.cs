using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Publico.Models {
    // Класс генерации токена
    public class AuthOptions {
        public const string ISSUER = "MyAuthServer"; // Издатель токена
        public const string AUDIENCE = "MyAuthClient"; // Потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // Ключ для шифрования
        public const int LIFETIME = 1; // Время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
