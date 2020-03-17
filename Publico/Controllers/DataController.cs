using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Publico.Models;
using Publico.Data;
using System.Collections;

namespace Publico.Controllers {
    /// <summary>
    /// Контроллер для обработки данных
    /// </summary>
    [ApiController, Route("api/odata/data")]
    public class DataController : Controller {
        ApplicationDbContext db;
        public DataController(ApplicationDbContext _context) {
            db = _context;
        }
        /// <summary>
        /// Метод добавляет друга
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        [HttpPost, Route("addfriend")]
        public async Task<IActionResult> AddFriend([FromBody] UsersRelations relation) {
            // Если модель корректна, то добавляет данные в БД
            if (relation.UserId == null || relation.ToUserId == null) {
                throw new ArgumentNullException();
            }
            UsersRelations fr = new UsersRelations {
                UserId = relation.UserId,
                ToUserId = relation.ToUserId,
                Type = relation.Type
            };
            // Добавляет в БД
            await db.UsersRelations.AddRangeAsync(fr);
            await db.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Метод получает список друзей пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("getfriends")]
        public async Task<IActionResult> GetFriendsList([FromQuery] int id) {
            // Выборка списка друзей
            var oFriends = await (from f1 in db.UsersRelations
                                  join f2 in db.Users on f1.ToUserId equals f2.Id
                                  where f1.UserId == id
                                  select new { friends = f2.Login }).ToListAsync();
            return Json(oFriends);
        }
        /// <summary>
        /// Метод записывает сообщение в таблицу сообщений
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("sendmessage")]
        public async Task<IActionResult> SendMessage([FromBody] Messages msg) {
            // Если модель не корректна
            if (msg.ToUserId == null || msg.FromUserId == null || msg.Message == "") {
                throw new ArgumentNullException();
            }
            var timeNew = DateTime.UtcNow;  // Получает текущее время
            var objMsg = new Messages {
                FromUserId = msg.FromUserId,
                ToUserId = msg.ToUserId,
                Message = msg.Message,
                Time = timeNew,
                AttachFileName = msg.AttachFileName
            };
            await db.Messages.AddAsync(objMsg);
            await db.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Проверяет, есть ли пользователь в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet, Route("getuser")]
        public async Task<IActionResult> GetUser([FromQuery] string user) {
            if (user == null) {
                return ErrorViewModel.IsEmptyUser();
            }
            var checkUser = await GetIdentityUser(user);
            if (checkUser == null) {
                return ErrorViewModel.NotFoundUser();
            }
            var resultCheck = new {
                id = checkUser.Id,
                foundUser = checkUser.Login
            };
            return Json(resultCheck);
        }
        [HttpPost, Route("getuserpost")]
        public async Task<IActionResult> GetUser([FromBody] UserCheckModel user) {
            string userNameTo = user.UserNameTo;
            if (userNameTo == "") {
                return ErrorViewModel.IsEmptyUser();
            }
            var checkUser = await GetIdentityUser(userNameTo);
            if (checkUser == null) {
                return ErrorViewModel.NotFoundUser();
            }
            var resultCheck = new {
                id = checkUser.Id,
                foundUser = checkUser.Login
            };
            return Json(resultCheck);
        }
        /// <summary>
        /// Ищет пользователя в БД
        /// </summary>
        /// <param name="user"></param> 
        /// <returns></returns>
        public async Task<User> GetIdentityUser(string userNameTo) {
            var sUser = await db.Users.FirstOrDefaultAsync(u => u.Login == userNameTo);
            return sUser;
        }
        /// <summary>
        /// Метод возвращает список сообщений
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost, Route("getmessages")]
        public async Task<IActionResult> GetMessages([FromBody] Messages msg) {
            // Если модель не корректна
            if (msg.ToUserId == null || msg.FromUserId == null || msg.Message == "") {
                throw new ArgumentNullException();
            }
            // Получает список сообщений
            var messagesObject = await (from m1 in db.Messages
                                        join m2 in db.Users on m1.FromUserId equals m2.Id
                                        where m1.ToUserId == msg.ToUserId
                                        && m1.FromUserId == msg.FromUserId
                                        || m1.ToUserId == msg.FromUserId
                                        && m1.FromUserId == msg.ToUserId
                                        select new {
                                            messages = m1.Message,
                                            login = m2.Login
                                        }).ToListAsync();
            return Json(messagesObject);
        }
        /// <summary>
        /// Метод удаляет друга
        /// </summary>
        /// <param name="friend"></param>
        /// <returns></returns>
        [HttpPost, Route("deletefriend")]
        public async Task<IActionResult> DeleteFriend([FromBody] UserDelete user) {
            if (user.UserFrom == null && user.UserTo == null) { ErrorViewModel.IsEmptyUser(); }
            var objFriend = await db.Users.FirstOrDefaultAsync(n => n.Login == user.UserTo);
            // Узнает id друга которого нужно удалить
            int idFriend = objFriend.Id;
            // Выбирает удаляемого друга
            UsersRelations deleteUser = await db.UsersRelations.FirstOrDefaultAsync(u => u.ToUserId == idFriend);
            // Удаляет друга
            if (deleteUser != null) {
                db.UsersRelations.Remove(deleteUser);
                await db.SaveChangesAsync();
                return Ok();
            }
            return ErrorViewModel.RemoveError();
        }
    }
}