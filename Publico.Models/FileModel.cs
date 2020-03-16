using System;
using System.Collections.Generic;
using System.Text;
using Publico.Models;

namespace Publico.Models {
    /// <summary>
    /// Класс загрузки файла
    /// </summary>
    public class FileModel {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
