using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Services
{
    internal class Library
    {
        private List<Book> books = new List<Book>();
        // Create a dictionary with a case-insensitive string comparer for genre keys
        private Dictionary<string, List<Book>> booksByGenre = new Dictionary<string, List<Book>>(StringComparer.OrdinalIgnoreCase);
        // This makes the ID's sorted 
        private int nextId = 1;

        // Time complexity: O(1)
        public void AddBook(string title, string author, int publicationYear, string genre)
        {
            Book book = new Book(nextId, title, author, publicationYear, genre);
            books.Add(book);
            if (!booksByGenre.ContainsKey(genre))
            {
                booksByGenre[genre] = new List<Book>();
            }
            booksByGenre[genre].Add(book);
            nextId++;
        }

        // Time complexity: O(1)
        public List<Book> GetAllBooks()
        {
            return books;
        }

        // Time complexity: O(1) 
        public List<Book> GetBooksByGenre(string genre)
        {
            List<Book> matchingBooks;
            if (booksByGenre.TryGetValue(genre, out matchingBooks))
            {
                return matchingBooks;
            }
            return new List<Book>(); // Return an empty list if the genre doesn't exist
        }
        

        // Binary search. Time complexity: O(logN), where N is the number of books in the list.
        // '?' makes the return value nullable
        public Book? GetBookById(int id)
        {
            int left = 0;
            int right = books.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                Book midBook = books[mid];

                if (midBook.Id == id)
                {
                    return midBook; // Found the book with the specified ID, returned the Book object
                }
                else if (midBook.Id < id)
                {
                    left = mid + 1; // Search in the right half
                }
                else
                {
                    right = mid - 1; // Search in the left half
                }
            }

            return null; // Book with the specified ID not found
        }

        // Linear search. Time complexity: O(N), where N is the number of items in the book list.
        public List<Book> GetBooksByTitle(string title)
        {
            List<Book> matchingBooks = new List<Book>();

            foreach (Book book in books)
            {
                // StringComparison.OrdinalIgnoreCase makes the comparison case-insensitive
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    matchingBooks.Add(book);
                }
            }

            return matchingBooks;
        }

        // Linear search. Time complexity: O(N), where N is the number of items in the book list.
        public List<Book> GetBooksByAuthor(string author)
        {
            List<Book> matchingBooks = new List<Book>();

            foreach (Book book in books)
            {
                if (book.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
                {
                    matchingBooks.Add(book);
                }
            }

            return matchingBooks;
        }

        // Time complexity: O(logN), due to the binary search in GetBookById
        public void BorrowBook(int id)
        {
            Book? book = GetBookById(id);

            if (book == null)
            {
                Console.WriteLine("No book exists with that ID");
            }
            else
            {
                if (!book.IsBorrowed)
                {
                    book.IsBorrowed = true;
                    Console.WriteLine("Book borrowed successfully.");
                }
                else
                {
                    Console.WriteLine("Book is already borrowed.");
                }
            }
        }

        // Time complexity: O(logN), due to the binary search in GetBookById
        public void ReturnBook(int id)
        {
            Book? book = GetBookById(id);
            if (book == null)
            {
                Console.WriteLine("No book exists with that ID.");
            }
            else
            {
                if (book.IsBorrowed)
                {
                    book.IsBorrowed = false;
                    Console.WriteLine("Book returned successfully.");
                }
                else
                {
                    Console.WriteLine("Book is not borrowed.");
                }
            }
        }
    }
}