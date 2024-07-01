namespace LibraryManagement;

public class Magazine : LibraryItem
{
    public override string Title { get; set; }
    public override string Author { get; set; }
    public override int ISBN { get; set; }
    public override int PublicationYear { get; set; }

    public Magazine(string title, string author, int isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }
    public override string ToString()
    {
        return $"Title magazine: {Title}, Author: {Author}, Publication year: {PublicationYear}, ISBN number: {ISBN}";
    }
}

public class MagazineList
{
    public List<Magazine> magazines;

    public MagazineList() => magazines = [];
    public void AddMagazine(Magazine magazine)
    {
        magazines.Add(magazine);
    }
    public void RemoveMagazine(Magazine magazine)
    {
        magazines.Remove(magazine);
    }
    public void Show()
    {
        foreach (var magazine in magazines)
        {
            Console.WriteLine(magazine);
        }
        if (magazines.Count == 0)
            Console.WriteLine("Magazines list is empty");

    }
    public Magazine? FindTitle(string title)
    {
        return magazines.Find(magazine => magazine.Title == title);

    }
    public List<string> GetMagazineTitles()
    {
        return magazines.Select(magazine => magazine.Title).ToList();
    }
}
