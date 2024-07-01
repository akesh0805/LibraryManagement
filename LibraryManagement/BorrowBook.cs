
namespace LibraryManagement;

public class BorrowBook : Book
{
    public DateTime BorrowData { get; set; }
    public DateTime ReturnData { get; set; }

    public BorrowBook(string title, string author, int publicationYear, int isbn, DateTime borrowData, DateTime returnData)
        : base(title, author, publicationYear, isbn)
    {
        BorrowData = borrowData;
        ReturnData = returnData;
    }

    public override string ToString()
    {
        return base.ToString() + $", Borrow Data: {BorrowData.ToShortDateString()}, Return Data: {ReturnData.ToShortDateString()}";
    }

}
public class BorrowBookList
{
    public List<BorrowBook> borrowBookList { get; set; }
    public BorrowBookList()
    {
        borrowBookList = new List<BorrowBook>();
    }
    public void AddBorrowBook(BorrowBook bookToBorrow)
    {
        borrowBookList.Add(bookToBorrow);
    }
    public void RemoveBorrowBook(BorrowBook borrowBook)
    {
        borrowBookList.Remove(borrowBook);
    }
    public void Show()
    {
        foreach (var book in borrowBookList)
        {
            Console.WriteLine(book);
        }
    }

    public List<string> GetBookTitles()
    {
        return borrowBookList.Select(book => book.Title).ToList();
    }
    public BorrowBook? FindTitle(string title)
    {
        return borrowBookList.FirstOrDefault(book => book.Title == title);
    }


}