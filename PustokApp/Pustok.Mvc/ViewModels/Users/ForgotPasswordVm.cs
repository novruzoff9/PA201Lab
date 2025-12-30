using System.ComponentModel.DataAnnotations;

namespace Pustok.Mvc.ViewModels.Users;

public class ForgotPasswordVm
{
    [Required(ErrorMessage = "Email daxil edin")]
    [EmailAddress(ErrorMessage = "Duzgun email daxil edin")]
    public string Email { get; set; }
}
