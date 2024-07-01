namespace LibraryManagement;

public interface ISearchable
{
    // void Search(string query);
    Book? FindTitle(string title);
    Book? FindAuthor(string author);
    Book? FindPublicYear(int publicYear);
    Book? FindIsbn(int isbn);
}