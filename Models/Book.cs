using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        // Field declarations
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
        public bool IsBorrowed { get; set; }

        // Constructor
        public Book(int id, string title, string author, int publicationYear, string genre)
        {
            Id = id;
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            Genre = genre;
            IsBorrowed = false;
        }

        // Overriding the ToString() method to return a formatted string containing the book's details.
        public override string ToString()
        {
            return $"ID: {Id}; {Title} by {Author}, {PublicationYear}, Genre: {Genre}, Borrowed: {IsBorrowed}";
        }
    }
}
