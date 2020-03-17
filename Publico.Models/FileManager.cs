using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Publico.Models {
    /// <summary>
    /// Сервис сохраняет файл в папку files
    /// </summary>
    public class FileManager {
        public async Task<bool> UploadFile(IFormFile file) {
            try {
                bool isCopied = false;
                if (file.Length > 0) {
                    string fileName = file.FileName;
                    string extension = Path.GetExtension(fileName);
                    // Задаем расширение файлов
                    if (extension == ".png" || extension == ".jpg" || extension == "docx") {
                        // Записываем путь к папке, в которую будем загружать файлы
                        string filePath = Path.GetFullPath(
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files"));
                        using (var fileStream = new FileStream(
                            Path.Combine(filePath, fileName),
                                           FileMode.Create)) {
                            await file.CopyToAsync(fileStream);
                            isCopied = true;
                        }
                    }
                    else {
                        throw new Exception("Файл должен быть формата .png, .JPG, .docx, .csv, .xlsx");
                    }
                }
                return isCopied;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
