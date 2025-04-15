using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

record Book(string ISBN, string Title, string Author, string Genre, bool CheckedOut, DateTime? DueDate);

class Library
{
    private List<Book> books = new();

    public void AddBookFromInput()
    {
        Console.WriteLine("\n Enter Book Details:");

        string isbn;
        while (true)
        {
            Console.Write("ISBN (numbers only): ");
            isbn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isbn) || !Regex.IsMatch(isbn, @"^\d+$"))
            {
                Console.WriteLine(" Invalid ISBN. Only numbers allowed.");
                continue;
            }

            if (books.Exists(b => b.ISBN == isbn))
            {
                Console.WriteLine(" A book with this ISBN already exists.");
                return;
            }

            break;
        }

        string title = GetValidInput("Title", @"^[A-Za-z0-9\s,.'-]+$");
        string author = GetValidInput("Author", @"^[A-Za-z\s,.'-]+$"); 
        string genre = GetValidInput("Genre", @"^[A-Za-z\s,.'-]+$"); 

        Book newBook = new(isbn, title, author, genre, false, null);
        books.Add(newBook);

        Console.WriteLine(" Book added successfully!");
    }

    private string GetValidInput(string fieldName, string pattern)
    {
        string input;
        do
        {
            Console.Write($"{fieldName}: ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, pattern))
                Console.WriteLine($" {fieldName} cannot be empty and must contain valid characters (letters, spaces, commas, periods, and hyphens).");
        } while (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, pattern));

        return input.Trim();
    }

    public void DisplayBooks()
    {
        Console.WriteLine("\n Library Book List:");
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine($"ISBN: {book.ISBN}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Checked Out: {book.CheckedOut}, Due Date: {(book.DueDate.HasValue ? book.DueDate.Value.ToShortDateString() : "N/A")}");
        }
    }

    public void CheckoutBook(string isbn)
    {
        try
        {
            int index = books.FindIndex(b => b.ISBN == isbn);
            if (index == -1)
                throw new Exception("Book not found.");

            var book = books[index];

            if (book.CheckedOut)
                throw new Exception("Book is already checked out.");

            DateTime dueDate = DateTime.Now.AddDays(14);
            books[index] = book with { CheckedOut = true, DueDate = dueDate };

            Console.WriteLine($" '{book.Title}' checked out. Due on {dueDate.ToShortDateString()}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(" " + ex.Message);
        }
    }

    public void ReturnBook(string isbn)
    {
        try
        {
            int index = books.FindIndex(b => b.ISBN == isbn);
            if (index == -1)
                throw new Exception("Book not found.");

            var book = books[index];

            if (!book.CheckedOut)
                throw new Exception("Book was not checked out.");

            books[index] = book with { CheckedOut = false, DueDate = null };
            Console.WriteLine($" '{book.Title}' returned successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(" " + ex.Message);
        }
    }
}

class Program
{
    static void Main()
    {

        Library library = new();

        while (true)
        {
            Console.WriteLine("\n===  Library Menu ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View Books");
            Console.WriteLine("3. Checkout Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    library.AddBookFromInput();
                    break;
                case "2":
                    library.DisplayBooks();
                    break;
                case "3":
                    Console.Write("Enter ISBN to checkout: ");
                    string checkoutIsbn = Console.ReadLine();
                    library.CheckoutBook(checkoutIsbn);
                    break;
                case "4":
                    Console.Write("Enter ISBN to return: ");
                    string returnIsbn = Console.ReadLine();
                    library.ReturnBook(returnIsbn);
                    break;
                case "5":
                    Console.WriteLine(" Exiting... Thank you!");
                    return;
                default:
                    Console.WriteLine(" Invalid option. Try again.");
                    break;
            }
        }
    }
}
