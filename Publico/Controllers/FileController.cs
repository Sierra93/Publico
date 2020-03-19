using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Publico.Models;
using Publico.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using System.Threading;

namespace Publico.Controllers {
    /// <summary>
    /// Контроллер для работы с файлами
    /// </summary>
    [Route("api/odata/file")]
    [ApiController]
    public class FileController : Controller {
        ApplicationDbContext db;
        IWebHostEnvironment _appEnvironment;
        public FileController(ApplicationDbContext _context, IWebHostEnvironment appEnvironment) {
            db = _context;
            _appEnvironment = appEnvironment;
        }
        /// <summary>
        /// Метод прикрепляет файл к сообщению
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost, Route("attach")]
        public async Task<IActionResult> AttachFile(IFormCollection file) {
            string storePath = "wwwroot/files/";   // Путь к папке с изображениями
            if (file.Files == null || file.Files[0].Length == 0) {
                throw new ArgumentNullException();
            }
            // Полный локальный путь к файлу включая папку проекта wwwroot
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), storePath,
                        file.Files[0].FileName);
            using (var stream = new FileStream(path, FileMode.Create)) {
                await file.Files[0].CopyToAsync(stream);
            }
            return Ok();
        }
        /// <summary>
        /// Метод скачивает файл из папки files и сохраняет в папку C:\downloads\
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        [HttpGet, Route("downloadfile")]
        public async Task<IActionResult> DownloadFile(string fileName) {
            string sPathTo = @"C:\downloads\";  // Куда будет загружать файл
            string path = Path.GetFullPath(fileName);
            // Откуда будет скачивать файл
            var sPathFrom = @"\\Mac\Home\Documents\MyPortfolio\Publico\Publico\wwwroot\files\" + fileName;
            if (!Directory.Exists(sPathTo)) {
                Directory.CreateDirectory(sPathTo);
            }
            WebClient client = new WebClient();            
            // Загружает файл
            client.DownloadFile(path, sPathTo + fileName);
            return Ok();
        }
    }
}