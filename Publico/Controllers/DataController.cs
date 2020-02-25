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
        /// <param name="modelm"></param>
        /// <returns></returns>
        [HttpPost, Route("addfriend")]
        public async Task<IActionResult> AddFriend(Friends modelf) {
            // Если модель корректна, то добавляет данные в БД
            if (modelf.UserId != null && modelf.FriendLogin != null) {
                // Получение полей из модели 
                Friends fr1 = new Friends {
                    UserId = modelf.UserId,
                    FriendLogin = modelf.FriendLogin
                };
                // Добавляет в БД
                await db.Friends.AddRangeAsync(fr1);
                // Сохраняет изменения
                await db.SaveChangesAsync();
                return Ok();
            }
            return ErrorViewModel.Error();
        }
        /// <summary>
        /// Метод получает список друзей пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("getfriends")]
        public async Task<IActionResult> GetFriendsList(string id) { 
            // Выборка списка друзей
            var friends = await db.Friends.Where(f => f.UserId == id).ToListAsync();
            return Json(friends);
        }
    }
}