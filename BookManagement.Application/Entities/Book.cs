using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Application.Entities
{

    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string FilePath { get; set; }

        public override string ToString()
        {
            return $"{Id}|{Title}|{Category}|{Author}|{FilePath}";
        }

        public static Book FromString(string line)
        {
            var parts = line.Split('|');
            if (parts.Length != 5)
                throw new FormatException("Sai định dạng dòng dữ liệu.");
            return new Book
            {
                Id = parts[0],
                Title = parts[1],
                Category = parts[2],
                Author = parts[3],
                FilePath = parts[4]
            };
        }
    }

}
