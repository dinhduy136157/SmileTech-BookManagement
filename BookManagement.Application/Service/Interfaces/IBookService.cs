using BookManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Application.Service.Interfaces
{
    public interface IBookService
    {
        void AddBook(Book book);
        void EditBook(string id, Book updatedBook);
        void DeleteBook(string id);
        List<Book> SearchBooks(string keyword);
        List<Book> GetBooksSortedByTitle();
        void ExportBooksToFile(string filePath);
        void ImportBooksFromFile(string filePath);
        Book GetBookById(string id);
        void ReadBookContent(string bookId);
        void WriteBookContent(string bookId, string content);
    }


}
