using Pustok.Mvc.Models.Common;

namespace Pustok.Mvc.Models;

public class BookImage : BaseEntity
{
    public string ImageUrl { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }
}
