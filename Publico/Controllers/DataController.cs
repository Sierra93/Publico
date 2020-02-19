using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Publico.Models;
using Publico.Data;

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
        [HttpPost, Route("gotochat")]
        public IActionResult GoToChat() {
            return View();
        }
    }
}