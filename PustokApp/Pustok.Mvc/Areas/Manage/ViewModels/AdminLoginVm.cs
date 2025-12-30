using System.ComponentModel.DataAnnotations;

namespace Pustok.Mvc.Areas.Manage.ViewModels;

public class AdminLoginVm
{
    [Required]
    [MinLength(3)]
    public string UserName { get; set; }
    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } 
}
