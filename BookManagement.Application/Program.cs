using BookManagement.Application.Entities;
using BookManagement.Application.Service.Implements;
using BookManagement.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Application
{

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            IBookService service = new BookService();
            string dataFile = "books.txt";
            service.ImportBooksFromFile(dataFile);

            while (true)
            {
                Console.WriteLine("\n1. Thêm sách");
                Console.WriteLine("2. Sửa sách");
                Console.WriteLine("3. Xóa sách");
                Console.WriteLine("4. Tìm kiếm sách");
                Console.WriteLine("5. Xem danh sách sách");
                Console.WriteLine("6. Đọc nội dung sách");
                Console.WriteLine("7. Ghi nội dung sách");
                Console.WriteLine("8. Lưu danh sách Sách");
                Console.WriteLine("9. Thoát");

                Console.Write("Chọn: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("ID: ");
                        string id = Console.ReadLine();
                        Console.Write("Tiêu đề: ");
                        string title = Console.ReadLine();
                        Console.Write("Danh mục: ");
                        string category = Console.ReadLine();
                        Console.Write("Tác giả: ");
                        string author = Console.ReadLine();
                        Console.Write("Đường dẫn file: ");
                        string path = Console.ReadLine();
                        service.AddBook(new Book { Id = id, Title = title, Category = category, Author = author, FilePath = path });
                        break;

                    case "2":
                        Console.Write("ID sách cần sửa: ");
                        id = Console.ReadLine();
                        Console.Write("Tiêu đề mới: ");
                        title = Console.ReadLine();
                        Console.Write("Danh mục mới: ");
                        category = Console.ReadLine();
                        Console.Write("Tác giả mới: ");
                        author = Console.ReadLine();
                        Console.Write("Đường dẫn file mới: ");
                        path = Console.ReadLine();
                        service.EditBook(id, new Book { Id = id, Title = title, Category = category, Author = author, FilePath = path });
                        break;

                    case "3":
                        Console.Write("ID sách cần xóa: ");
                        service.DeleteBook(Console.ReadLine());
                        break;

                    case "4":
                        Console.Write("Nhập từ khóa: ");
                        var result = service.SearchBooks(Console.ReadLine());
                        foreach (var book in result)
                        {
                            Console.WriteLine($"{book.Id}: {book.Title} - {book.Author}");
                        }
                        break;
                    case "5":
                        var sorted = service.GetBooksSortedByTitle();
                        foreach (var book in sorted)
                            Console.WriteLine($"{book.Id}: {book.Title} - {book.Author}");
                        break;

                    case "6":
                        Console.Write("Nhập ID sách để đọc nội dung: ");
                        service.ReadBookContent(Console.ReadLine());
                        break;
                    case "7":
                        Console.Write("Nhập ID sách để ghi nội dung: ");
                        id = Console.ReadLine();
                        Console.WriteLine("Nhập nội dung:");
                        string content = Console.ReadLine();
                        service.WriteBookContent(id, content);
                        break;

                    case "8":
                        service.ExportBooksToFile(dataFile);
                        Console.WriteLine("Đã lưu.");
                        break;
                    case "9":
                        service.ExportBooksToFile(dataFile);
                        Console.WriteLine("Thoát...");
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }

        }
    }
}


