namespace LibraryManagement;

public abstract class LibraryItem
{
    public abstract string Title { get; set; }
    public abstract string Author { get; set; }

    public abstract int ISBN { get; set; }
    public abstract int PublicationYear { get; set; }
}
