using Spectre.Console;
using LibraryManagement;
AnsiConsole.Status()
    .Start("Loading...", ctx =>
    {
        AnsiConsole.MarkupLine("Getting ready...");
        Thread.Sleep(1000);
        ctx.Status("Getting ready list book");
        ctx.Spinner(Spinner.Known.Star);
        ctx.SpinnerStyle(Style.Parse("green"));
        AnsiConsole.MarkupLine("Loading...");
        Thread.Sleep(2000);
    });
var b1 = new Book("I Feel Bad About My Neck", "Nora Ephron", 2006, 101);
var b2 = new Book("Broken Glass", "Alain Mabanckou", 2005, 102);
var b3 = new Book("The Girl With the Dragon Tattoo", "Stieg Larsson", 2005, 103);
var b4 = new Book("Harry Potter and the Goblet of Fire", "JK Rowling", 2000, 104);
var b5 = new Book("A Little Life", "Hanya Yanagihara", 2015, 105);
var b6 = new Book("Chronicles: Volume One", "Bob Dylan", 2004, 106);
var b7 = new Book("The Tipping Point", "Malcolm Gladwell", 2000, 107);
var b8 = new Book("Darkmans", "Nicola Barker", 2007, 108);
var b9 = new Book("The Siege", "Helen Dunmore", 2001, 109);
var b10 = new Book("Light", "John Harrison", 2002, 110);
BookList listBook = new BookList();
listBook.AddBook(b1);
listBook.AddBook(b2);
listBook.AddBook(b3);
listBook.AddBook(b4);
listBook.AddBook(b5);
listBook.AddBook(b6);
listBook.AddBook(b7);
listBook.AddBook(b8);
listBook.AddBook(b9);
listBook.AddBook(b10);
BorrowBookList borrowBookList = new BorrowBookList();
MagazineList listMagazine = new MagazineList();

var rule = new Rule();
AnsiConsole.Write(rule);
AnsiConsole.Write(
    new FigletText("\nWelcome to LIBRARY\n")
        .LeftJustified()
        .Color(Color.Red));
var title = new Rule("Main Menu\n");
title.Justification = Justify.Left;
AnsiConsole.Write(title);
bool exit = false;

while (!exit)
{
    var libMenu = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .PageSize(10)
            .MoreChoicesText("[red](Move up and down to reveal more items)[/]")
            .AddChoices([
                "show list of books",
                "show list of magazines",
                "show list of borrowed books",
                "borrow book",
                "return borrowed book",
                "search book",
                "add book",
                "remove book",
                "add magazine",
                "remove magazine",
                "exit",
            ]));

    switch (libMenu)
    {

        case "show list of books":
            listBook.Show();

            break;

        case "show list of magazines":
            listMagazine.Show();
            break;

        case "show list of borrowed books":
            borrowBookList.Show();
            break;

        case "borrow book":
            var bookTitles = listBook.GetBookTitles();
            if (bookTitles.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No books available to borrow.[/]");
                break;
            }

            var borrowBookTitle = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nSelect book to borrow: ")
                    .PageSize(10)
                    .MoreChoicesText("[red](Move up and down to reveal more items)[/]")
                    .AddChoices(bookTitles.Concat(["Back to menu"])));

            if (borrowBookTitle == "back to menu")
            {
                break;
            }
            var bookToBorrow = listBook.FindTitle(borrowBookTitle);
            if (bookToBorrow != null)
            {
                var borrowData = AnsiConsole.Ask<DateTime>("Enter today's data: (mm-dd-yyyy)");
                var returnData = AnsiConsole.Ask<DateTime>("Enter return's data: (mm-dd-yyyy)");
                BorrowBook newBorrowBook = new BorrowBook(bookToBorrow.Title, bookToBorrow.Author, bookToBorrow.PublicationYear, bookToBorrow.ISBN, borrowData, returnData);
                listBook.RemoveBook(bookToBorrow);
                borrowBookList.AddBorrowBook(newBorrowBook);
                AnsiConsole.MarkupLine($"[green]You borrowed Book: '{borrowBookTitle}'  successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Book not borrowed.[/]");
            }
            break;


        case "return borrowed book":
            var borrowedBookTitles = borrowBookList.GetBookTitles();
            if (borrowedBookTitles.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No books available to return.[/]");
                break;
            }

            var returnBookTitle = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nSelect book to return: ")
                    .PageSize(10)
                    .MoreChoicesText("[red](Move up and down to reveal more items)[/]")
                    .AddChoices(borrowedBookTitles.Concat(new[] { "Back to menu" })));

            if (returnBookTitle == "Back to menu")
            {
                break;
            }

            var bookToReturn = borrowBookList.FindTitle(returnBookTitle);
            if (bookToReturn != null)
            {
                listBook.AddBook(new Book(
                    bookToReturn.Title,
                    bookToReturn.Author,
                    bookToReturn.PublicationYear,
                    bookToReturn.ISBN
                ));
                borrowBookList.RemoveBorrowBook(bookToReturn);
                AnsiConsole.MarkupLine($"[green]Book '{returnBookTitle}' returned successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Book not found for return.[/]");
            }
            break;
        case "search book":
            var searchMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nSearch book by:")
                    .PageSize(10)
                    .MoreChoicesText("[red](Move up and down to reveal more items)[/]")
                    .AddChoices([
                        "Title",
                        "Author",
                        "Publication year",
                        "ISBN",
                        "Back to menu"
                    ]));

            Book? foundBook = null;
            switch (searchMenu)
            {
                case "Back to menu":
                    break;
                case "Title":
                    var title1 = AnsiConsole.Ask<string>("Enter book title:");
                    foundBook = listBook.FindTitle(title1);
                    break;
                case "Author":
                    var author = AnsiConsole.Ask<string>("Enter book author:");
                    foundBook = listBook.FindAuthor(author);
                    break;
                case "Publication year":
                    var year = AnsiConsole.Ask<int>("Enter publication year:");
                    foundBook = listBook.FindPublicYear(year);
                    break;
                case "ISBN":
                    var isbn = AnsiConsole.Ask<int>("Enter ISBN:");
                    foundBook = listBook.FindIsbn(isbn);
                    break;
            }

            if (foundBook != null)
            {
                AnsiConsole.MarkupLine($"[green]Found book: {foundBook}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Book not found.[/]");
            }
            break;


        case "add book":
            var newTitle = AnsiConsole.Ask<string>("Enter book Title: ");
            var newAuthor = AnsiConsole.Ask<string>("Enter book Author: ");
            var newPublicYear = AnsiConsole.Ask<int>("Enter book Publication year:");
            var newIsbn = AnsiConsole.Ask<int>("Enter book ISBN:");
            Book newBook = new Book(newTitle, newAuthor, newPublicYear, newIsbn);
            listBook.AddBook(newBook);
            AnsiConsole.MarkupLine($"[green]New book added: {newBook}[/]");
            break;
        case "remove book":
            var bookTitless = listBook.GetBookTitles();
            if (bookTitless.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No books available to remove.[/]");
                break;
            }

            var booksToRemove = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("\nSelect books to remove: ")
                    .PageSize(10)
                    .MoreChoicesText("[red](Move up and down to reveal more items)[/]")
                    .AddChoices(bookTitless)
            );

            if (booksToRemove.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No books selected for removal.[/]");
                break;
            }

            foreach (var titlee in booksToRemove)
            {
                var bookToRemove = listBook.FindTitle(titlee);
                if (bookToRemove != null)
                {
                    listBook.RemoveBook(bookToRemove);
                    AnsiConsole.MarkupLine($"[green]Book '{titlee}' removed successfully![/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]Book '{titlee}' not found for removal.[/]");
                }
            }
            break;


        case "add magazine":

            var newTitlem = AnsiConsole.Ask<string>("Enter book Title: ");
            var newAuthorm = AnsiConsole.Ask<string>("Enter book Author: ");
            var newPublicYearm = AnsiConsole.Ask<int>("Enter book Publication year:");
            var newIsbnm = AnsiConsole.Ask<int>("Enter book ISBN:");
            Magazine newMagazine = new Magazine(newTitlem, newAuthorm, newPublicYearm, newIsbnm);
            listMagazine.AddMagazine(newMagazine);
            AnsiConsole.MarkupLine($"[green]New magazine added: {newMagazine}[/]");
            break;
        case "remove magazine":

            var magazineTitles = listMagazine.GetMagazineTitles();
            if (magazineTitles.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No magazines available to remove.[/]");
                break;
            }

            var removeMagazineTitle = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nSelect magazine to remove: ")
                    .PageSize(10)
                    .MoreChoicesText("[red](Move up and down to reveal more items)[/]")
                    .AddChoices(magazineTitles));

            var magazineToRemove = listMagazine.FindTitle(removeMagazineTitle);
            if (magazineToRemove != null)
            {
                listMagazine.RemoveMagazine(magazineToRemove);
                AnsiConsole.MarkupLine($"[green]Magazine '{removeMagazineTitle}' removed successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]MAgazine not found for remove.[/]");
            }
            break;
        case "exit":
            exit = true;
            break;
    }
}
