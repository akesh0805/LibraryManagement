
namespace LibraryManagement;
using Spectre.Console;
public class Book : LibraryItem, IBorrowable
{
    public override string Title { get; set; }
    public override string Author { get; set; }
    public override int PublicationYear { get; set; }
    public override int ISBN { get; set; }

    public Book(string title, string author, int publicationYear, int isbn)
    {
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
        ISBN = isbn;

    }



    public override string ToString()
    {
        return $"Title book: {Title}, Author: {Author}, Publication year: {PublicationYear}, ISBN number: {ISBN}";
    }

    public void Borrow()
    {
        throw new NotImplementedException();
    }

    public void Return()
    {
        throw new NotImplementedException();
    }

}




public class BookList : ISearchable
{
    public List<Book> books;
    public BookList() => books = [];
    public void AddBook(Book book)
    {
        books.Add(book);
    }
    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }
    public void Show()
    {
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public Book? FindTitle(string title)
    {
        return books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

    }

    public Book? FindAuthor(string author)
    {
        return books.Find(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase));

    }

    public Book? FindPublicYear(int publicYear)
    {
        return books.Find(book => book.PublicationYear == publicYear);

    }

    public Book? FindIsbn(int isbn)
    {
        return books.Find(book => book.ISBN == isbn);

    }
    public List<string> GetBookTitles()
    {
        return books.Select(book => book.Title).ToList();
    }


}



