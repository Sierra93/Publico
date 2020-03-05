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
            if (relation.UserId == null && relation.ToUserId == null) {
                return ErrorViewModel.Error();
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
        public async Task<IActionResult> GetFriendsList([FromQuery] string id) {
            // Выборка списка друзей
            var oFriends = await db.UsersRelations.Join(db.Users,
                f1 => f1.UserId,
                f2 => f2.Id,
                (f1, f2) => new {
                    friends = f2.Login
                }).ToListAsync();
            return Json(oFriends);
        }
        /// <summary>
        /// Метод записывает сообщение в таблицу сообщений
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("sendmessage")]
        public async Task<IActionResult> SendMessage() {
            //Dialogs objMsg = new Dialogs {
            //    FromMessageUserId = msg.FromMessageUserId,
            //    MessageBody = msg.MessageBody
            //};
            //if (msg.ChatId != null) {
            //    objMsg.ChatId = msg.ChatId;
            //}
            //else {
            //    objMsg.ChatId = null;
            //}
            //await db.Messages.AddAsync(objMsg);
            //await db.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Проверяет, есть ли пользователь в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet, Route("getuser")]
        public async Task<IActionResult> GetUser([FromQuery] string user) {
            if (user == null || user == "") {
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
        /// <summary>
        /// Ищет пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> GetIdentityUser(string user) {
            var sUser = await db.Users.FirstOrDefaultAsync(u => u.Login == user);
            return sUser;
        }
    }
}