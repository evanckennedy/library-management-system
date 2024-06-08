using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    internal class Program
    {
        // Make library a static field so it can be accessed from all methods in this class
        static Library library = new Library();

        static void Main(string[] args)
        {
            // Initial book collection
            library.AddBook("To Kill a Mockingbird", "Harper Lee", 1960, "Classic");
            library.AddBook("The Great Gatsby", "F Scott Fitzgerald", 1925, "Classic");
            library.AddBook("1984", "George Orwell", 1949, "Dystopian");
            library.AddBook("The Catcher in the Rye", "JD Salinger", 1951, "Coming-of-Age");
            library.AddBook("Pride and Prejudice", "Jane Austen", 1813, "Romance");
            library.AddBook("The Lord of the Rings", "JRR Tolkien", 1954, "Fantasy");
            library.AddBook("The Picture of Dorian Gray", "Oscar Wilde", 1890, "Gothic");
            library.AddBook("Moby-Dick", "Herman Melville", 1851, "Adventure");
            library.AddBook("The Adventures of Sherlock Holmes", "Arthur Conan Doyle", 1892, "Mystery");
            library.AddBook("The Scarlet Letter", "Nathaniel Hawthorne", 1850, "Classic");
            library.AddBook("Treasure Island", "Robert Louis Stevenson", 1883, "Adventure");

            while (true)
            {
                ShowMenu();
                // Console.ReadKey() reads the next key press from the user. 
                // The KeyChar property gets the character corresponding to the key pressed.
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        AddBook();
                        break;
                    case '2':
                        ViewBooks();
                        break;
                    case '3':
                        SearchBooks();
                        break;
                    case '4':
                        BorrowBook();
                        break;
                    case '5':
                        ReturnBook();
                        break;
                    case '6':
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View Books");
            Console.WriteLine("3. Search Books");
            Console.WriteLine("4. Borrow Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
        }

        static void AddBook()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            int year;
            while (true)
            {
                Console.Write("Enter publication year: ");
                if (int.TryParse(Console.ReadLine(), out year))
                {
                    // The user entered a valid integer, so break out of the loop
                    break;
                }
                else
                {
                    // The user didn't enter a valid integer, so show an error message and continue the loop
                    Console.WriteLine("Invalid publication year. Please enter a valid integer.");
                }
            }

            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            library.AddBook(title, author, year, genre);
            Console.WriteLine("Book added successfully.");
        }

        static void ViewBooks()
        {
            while (true)
            {
                Console.WriteLine("View Books");
                Console.WriteLine("1. All books");
                Console.WriteLine("2. By genre");
                Console.Write("Enter your choice: ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (choice == '1')
                {
                    List<Book> books = library.GetAllBooks();
                    DisplayBooks(books);
                    break;
                }
                else if (choice == '2')
                {
                    Console.Write("Enter Genre: ");
                    string genre = Console.ReadLine();
                    List<Book> books = library.GetBooksByGenre(genre);
                    DisplayBooks(books);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }      
        }

        static void DisplayBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
            }
            else
            {
                foreach (Book book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        static void SearchBooks()
        {
            while (true)
            {
                Console.WriteLine("Search Books");
                Console.WriteLine("1. By title");
                Console.WriteLine("2. By author");
                Console.WriteLine("3. By id");
                Console.WriteLine("4. Back to main menu");
                Console.Write("Enter your choice: ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        List<Book> booksByTitle = library.GetBooksByTitle(title);
                        if (booksByTitle.Count > 0)
                        {
                            DisplayBooks(booksByTitle);
                        }
                        else
                        {
                            Console.WriteLine("No books found with that title.");
                        }
                        break;
                    case '2':
                        Console.Write("Enter author: ");
                        string author = Console.ReadLine();
                        List<Book> booksByAuthor = library.GetBooksByAuthor(author);
                        if (booksByAuthor.Count > 0)
                        {
                            DisplayBooks(booksByAuthor);
                        }
                        else
                        {
                            Console.WriteLine("No books found by that author.");
                        }
                        break;
                    case '3':
                        int id;
                        while (true)
                        {
                            Console.Write("Enter id: ");
                            if (int.TryParse(Console.ReadLine(), out id))
                            {
                                Book book = library.GetBookById(id);
                                if (book != null)
                                {
                                    DisplayBooks(new List<Book> { book });
                                }
                                else
                                {
                                    Console.WriteLine("No book found with that ID.");
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input.");
                            }
                        }
                        break;
                    case '4':
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void BorrowBook()
        {
            int id;
            while (true)
            {
                Console.Write("Enter the ID of the book you'd like to borrow: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    library.BorrowBook(id);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter an integer.");
                }
            }
        }

        static void ReturnBook()
        {
            int id;
            while (true)
            {
                Console.Write("Enter the ID of the book you'd like to return: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    library.ReturnBook(id);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter an integer");
                }
            }
        }
    }
}
