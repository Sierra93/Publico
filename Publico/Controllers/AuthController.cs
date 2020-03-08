using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Publico.Models;
using Microsoft.EntityFrameworkCore;
using Publico.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Publico.Controllers {
    // Авторизация и регистрация пользователей
    [ApiController, Route("api/odata/auth")]
    public class AuthController : Controller {
        private ApplicationDbContext db;
        public AuthController(ApplicationDbContext context) {
            db = context;
        }
        /// <summary>
        /// Метод добавляет пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user) {
            if (user.Login == null || user.Password == null || user.Email == null) {
                return ErrorViewModel.Error();
            }
            // Добавляет нового пользователя
            User regUser = new User { Login = user.Login, Email = user.Email, Password = user.Password };
            db.Users.AddRange(regUser);
            // Сохраняет изменения в БД
            await db.SaveChangesAsync();
            return Ok(regUser);
        }
        /// <summary>
        /// Метод проверяет существование пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost, Route("signin")]
        public IActionResult GetUserFromDb([FromBody] User user) {
            User userobj = new User();  // Объект пользователя, из которого возьмем только ID для возврата фронту
            if (user.Login == null || user.Password == null) {
                return ErrorViewModel.Error(); 
            }
            // Проверяет, есть ли пользователь в БД
            var identity = GetIdentity(user.Login, user.Password);
            // Если пользователь найден, то получаем его ID 
            if (identity != null) {
                userobj = db.Users.FirstOrDefault(x => x.Login == user.Login);
            }
            var now = DateTime.UtcNow;
            if (identity == null) { return ErrorViewModel.ErrorToken(); }
            // Создание JWT-токена
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            // Объект анонимного типа с токеном, который отсылается на фронт
            var response = new {
                access_token = encodedJwt,
                userName = identity.Name,
                id = userobj.Id
            };
            return Json(response);
        }
        /// <summary>
        /// Метод выбирает пользователя из БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ClaimsIdentity GetIdentity(string login, string password) {
            var checkUser = db.Users.FirstOrDefault(l => l.Login == login && l.Password == password);
            if (checkUser != null) {
                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
        /// <summary>
        /// Метод проверяет есть ли в БД такой логин
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet, Route("checklogin")]
        public async Task<IActionResult> GetLogin([FromQuery] string login) {
            if (login == "") { ErrorViewModel.IsEmptyUser(); }                            
            var isLogin = await db.Users.FirstOrDefaultAsync(l => l.Login == login);
            if (isLogin != null) { ErrorViewModel.LoginNotEmpty(); }
            return Ok("Логин свободен.");
        } 
    }
}