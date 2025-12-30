using Pustok.Mvc.Attributes;
using Pustok.Mvc.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok.Mvc.Models;

public class Slider : BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string ButtonText { get; set; }
    public string ButtonUrl { get; set; }
    public int Order { get; set; }
    [NotMapped]
    [FileLengthAttribute(1*1024*1024)]
    [FileType("image/jpeg","image/png","image/gif")]
    public IFormFile? File { get; set; }
}
