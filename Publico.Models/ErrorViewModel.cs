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
        // ���� ������� ����� �� ���������
        public static IActionResult IsEmptyEmail() { throw new Exception("������� ����� �� ���������."); }
        // ���� ����� ��� ����������
        public static IActionResult LoginNotEmpty() { throw new Exception("��������� ����� ��� ����������."); }
        // ���� ����� ��� ����������
        public static IActionResult EmailNotEmpty() { throw new Exception("��������� email ��� ����������."); }
        // ���� �� ������� �������
        public static IActionResult RemoveError() { throw new Exception("������ ��������."); }
        // ���� �� ������� �������� ������
        public static IActionResult ErrorChangePassword() { throw new Exception("������ ��������� ������"); }
    }
}
