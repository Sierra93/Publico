using Microsoft.AspNetCore.Mvc;
using System;

namespace Publico.Models {
    // ����� � �������� ��� ��������� ������
    public class ErrorViewModel {
        public static IActionResult Error() {
            throw new Exception("�������� � �������");
        }
    }
}
