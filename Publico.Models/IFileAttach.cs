using System;
using System.Collections.Generic;
using System.Text;

namespace Publico.Core.Interfaces {
    interface IFileAttach {
        int Id { get; set; }
        string FileName { get; set; }
        string FilePath { get; set; }
    }
}
