using Microsoft.AspNetCore.Mvc;
using System;

namespace Publico.Models {
    // ����� � �������� ��� ��������� ������
    public class ErrorViewModel {
        // ���� ������ ������ �� ����������
        public static IActionResult Error() { throw new Exception("�������� � �������."); }
        // ���� � ������������ ��� ������
        public static IActionResult ErrorToken() { throw new Exception("������������ �� �������������. � ������� � ������ ��������."); }
        // ���� ������������ ��� � ��
        public static IActionResult NotFoundUser() { throw new Exception("������������ �� ����������."); }
        // ���� ������� ������������ �� ������ � ������
        public static IActionResult IsEmptyUser() { throw new Exception("������� ������������ �� ��������."); }
    }
}
