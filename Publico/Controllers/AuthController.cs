﻿using System;
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
using Publico.Services;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

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
                throw new ArgumentNullException();
            }
            // Хэширует пароль
            var hashString = await HashMD5Service.HashPassword(user.Password);
            // Подтверждение по почте
            await EmailService.SendToEmail(user);
            // Добавляет нового пользователя
            User regUser = new User { Login = user.Login, Email = user.Email, Password = hashString };
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
        public async Task<IActionResult> GetUserFromDb([FromBody] UserSignIn user) {
            User userobj = new User();
            if (user.Login == null || user.Password == null) {
                throw new ArgumentNullException();
            }
            // Хэширует пароль для сравнения зашифрованных паролей
            string checkHashString = await HashMD5Service.HashPassword(user.Password);
            // Сравнивает хэши
            var isEqual = await EqualsHash(user.Login);
            if (isEqual != checkHashString) {
                return ErrorViewModel.ErrorEqualHash();
            }
            // Проверяет, есть ли пользователь в БД
            var identity = await GetIdentity(user.Login, isEqual);            
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
        public async Task <ClaimsIdentity> GetIdentity(string login, string password) {
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
        /// <summary>
        /// Метод проверяет есть ли в БД такая почта
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet, Route("checkemail")]
        public async Task<IActionResult> GetEmail([FromQuery] string email) {
            if (email == "") { ErrorViewModel.IsEmptyEmail(); }
            var isEmail = await db.Users.FirstOrDefaultAsync(e => e.Email == email);
            if (isEmail != null) { ErrorViewModel.EmailNotEmpty(); }
            return Ok("email свободен.");
        }
        /// <summary>
        /// Метод проверяет, есть ли в БД пользователь по такому логину или email, смотря что пришло
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet, Route("remember")]
        public async Task<IActionResult> RememberPassword(string param) {
            User sResult;
            if (param != "") {
                if (param.IndexOf("@") >= 0) {
                    sResult = await db.Users.FirstOrDefaultAsync(p => p.Email == param);
                    return Ok();
                }
                else {
                    sResult = await db.Users.FirstOrDefaultAsync(p => p.Login == param);
                    return Ok();
                }
            }
            return ErrorViewModel.NotFoundUser();
        }
        /// <summary>
        /// Метод изменяет пароль пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("changepassword")]
        public async Task<IActionResult> ChangePassword(string login, string password) {
            if (login != "" && password != "") {
                // Находит пользователя с таким логином
                var user = await db.Users.FirstOrDefaultAsync(u => u.Login == login);
                // Хэширует новый пароль
                var hashPassword = HashMD5Service.HashPassword(password);
                // Изменяет пароль в модели пользователя
                user.Password = await hashPassword;                
                await db.SaveChangesAsync();
                return Ok();
            }
            return ErrorViewModel.ErrorChangePassword();
        }
        public async Task<string> EqualsHash(string login) {
            string hashDb = "";
            var getHashPassword = await (from u in db.Users
                                         where u.Login == login
                                         select u.Password).ToListAsync();
            foreach (var el in getHashPassword) {
                hashDb = el;
            }
            return hashDb;
        }
    }
}