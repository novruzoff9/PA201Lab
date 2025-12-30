using Pustok.Mvc.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pustok.Mvc.Models;

public class Author : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string FullName { get; set; }
    public List<Book>? Books { get; set; }
}
