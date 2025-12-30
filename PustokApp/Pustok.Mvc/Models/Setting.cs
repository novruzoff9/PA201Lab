using Pustok.Mvc.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pustok.Mvc.Models;

public class Setting
{
    [Key]
    public string Key { get; set; }
    public string Value { get; set; }
}
