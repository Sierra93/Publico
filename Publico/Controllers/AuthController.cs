using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Publico.Models;
using Microsoft.EntityFrameworkCore;
using Publico.Data;

namespace Publico.Controllers {
    // Авторизация и регистрация пользователей
    [ApiController, Route("api/odata/auth")]
    public class AuthController : ControllerBase {
        private ApplicationDbContext db;
        public AuthController(ApplicationDbContext context) {
            db = context;
        }
        // Метод добавляет пользователя в БД
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user) { 
            if (user.Login == null || user.Password == null || user.Email == null) {
                return Error();
            }
            // Добавляет нового пользователя
            User regUser = new User { Login = user.Login, Email = user.Email, Password = user.Password };
            db.Users.AddRange(regUser);
            // Сохраняет изменения в БД
            await db.SaveChangesAsync();
            return Ok(regUser); 
        }
        private IActionResult Error() {
            throw new Exception("Проблемы с моделью");
        }
        // Метод проверяет существование пользователя в БД
        [HttpPost, Route("signin")]       
        public async Task<IActionResult> GetUserFromDb(User user) {
            if (user.Login == null || user.Password == null) {
                return Error();
            }
            await GetIdentity(user);
            return Ok("Пользователь успешно авторизован");
        }
        // Метод выбирает пользователя из БД
        public async Task <List<User>> GetIdentity(User user) {
            var checkUser = db.Users.FirstOrDefault(l => l.Login == user.Login && l.Password == l.Password);
            if (checkUser != null) {
                var newUser = new List<User>();
                newUser.Add(checkUser);
                return newUser;
            }
            return null;
        }
    }
}