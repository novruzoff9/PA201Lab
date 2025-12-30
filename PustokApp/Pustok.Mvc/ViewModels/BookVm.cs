using Pustok.Mvc.Models;

namespace Pustok.Mvc.ViewModels;

public class BookVm
{
    public Book Book { get; set; }
    public List<Book> RelatedBooks { get; set; }
}
