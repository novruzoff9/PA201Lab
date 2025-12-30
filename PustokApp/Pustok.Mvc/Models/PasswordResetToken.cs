using System.ComponentModel.DataAnnotations;

namespace Pustok.Mvc.Models;

public class PasswordResetToken
{
    [Key]
    public string UserEmail { get; set; }
    public string Key { get; set; }
    public DateTime ExpireDate { get; set; }
}
