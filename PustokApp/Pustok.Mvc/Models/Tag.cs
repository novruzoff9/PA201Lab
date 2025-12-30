using Pustok.Mvc.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pustok.Mvc.Models;

public class Tag:BaseEntity
{
    [Required]
    public string Name { get; set; }
    public List<BookTag> BookTags { get; set; }
}
