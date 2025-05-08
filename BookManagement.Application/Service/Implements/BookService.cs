using BookManagement.Application.Entities;
using BookManagement.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace BookManagement.Application.Service.Implements
{

    public class BookService : IBookService
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            if (books.Any(b => b.Id == book.Id))
                throw new Exception("ID đã tồn tại.");
            books.Add(book);
        }

        public void EditBook(string id, Book updatedBook)
        {
            var book = GetBookById(id);
            if (book == null)
                throw new Exception("Không tìm thấy sách.");
            book.Title = updatedBook.Title;
            book.Category = updatedBook.Category;
            book.Author = updatedBook.Author;
            book.FilePath = updatedBook.FilePath;
        }

        public void DeleteBook(string id)
        {
            var book = GetBookById(id);
            if (book != null)
                books.Remove(book);
        }

        public List<Book> SearchBooks(string keyword)
        {
            return books
                .Where(b => b.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                         || b.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                         || b.Category.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }


        public List<Book> GetBooksSortedByTitle()
        {
            return books.OrderBy(b => b.Title).ToList();
        }

        public void ExportBooksToFile(string filePath)
        {
            try
            {
                File.WriteAllLines(filePath, books.Select(b => b.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi file: " + ex.Message);
            }
        }

        public void ImportBooksFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return;

                var lines = File.ReadAllLines(filePath);
                books = lines.Select(line => Book.FromString(line)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file: " + ex.Message);
            }
        }

        public Book GetBookById(string id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void ReadBookContent(string bookId)
        {
            var book = GetBookById(bookId);
            if (book == null || !File.Exists(book.FilePath))
            {
                Console.WriteLine("Không tìm thấy nội dung sách.");
                return;
            }

            try
            {
                string content = File.ReadAllText(book.FilePath);
                Console.WriteLine("Nội dung sách: ");
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc nội dung sách: " + ex.Message);
            }
        }

        public void WriteBookContent(string bookId, string content)
        {
            var book = GetBookById(bookId);
            if (book == null)
            {
                Console.WriteLine("Không tìm thấy sách.");
                return;
            }

            try
            {
                File.AppendAllText(book.FilePath, content);
                Console.WriteLine("Đã ghi nội dung sách.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi nội dung sách: " + ex.Message);
            }
        }
    }
}