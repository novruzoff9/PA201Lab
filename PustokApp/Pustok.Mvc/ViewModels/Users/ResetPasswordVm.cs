using System.ComponentModel.DataAnnotations;

namespace Pustok.Mvc.ViewModels.Users;

public class ResetPasswordVm
{
    public string Email { get; set; }
    public string Token { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}
